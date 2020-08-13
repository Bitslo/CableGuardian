using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valve.VR;
using System.ComponentModel;
using System.Threading;

namespace CableGuardian
{
    public enum OpenVRConnectionStatus { AllOK, NoHMD, Initializing, NoSteamVR, SteamVRQuit, InitLimitReached, UnexpectedError, Stopped }

    class OpenVRConnection : VRConnection
    {
        public double Yaw = 0;
        BackgroundWorker Worker = new BackgroundWorker();
        BackgroundWorker WorkerManager = new BackgroundWorker();
        bool WorkerFailed = false;
        bool StopFlag = false;
        const int PollInterval = 11; //100; // needs to be quick enough to reliably catch the quit message from steamvr. 90 fps = 11,1 so I'd imagine that should do it...        
                                     // 18.4.2020 TIL: Thread.Sleep() is very inaccurate. 
                                     // Also, intervals below 15ms are never going to happen.
                                     // https://docs.microsoft.com/en-us/windows/win32/api/synchapi/nf-synchapi-sleep#remarks
                                     // But luckily I don't need accurate timing in this case, and the interval seems to be quick enough (whatever it actually is).
        const int InitializationInterval = 2000;        
        int InitAttemptCount = 0;        
        const int InitAttemptLimit = 300; // (failed) OpenVR initialization attempt seems to leak about 0.1MB memory. Set a global limit (and request app restart)
        const int SleepTimeAfterQuit = 15000;
        int HMDUserInteractionBufferTime = 2000;
        EVRInitError LastOpenVRError = EVRInitError.None;
        CVRSystem VRSys = null;
        string LastExceptionMessage { get; set; }
        string LastStopMessage { get; set; }       
        VREvent_t NextVREvent = new VREvent_t();
        TrackedDevicePose_t[] PoseArray;        
        bool StopRequested = false;       
        uint HmdIndex = 0;
        int _KeepAliveCounter = 0;
        int KeepAliveCounter { get { return _KeepAliveCounter; } set { _KeepAliveCounter = (_KeepAliveCounter >= 10000) ? 1 : value; } }
        // Unlike Oculus, there doesn't seem to be a way to read HMD mounted (proximity sensor) for OpenVR.
        // For OpenVR User interaction is true when HMD is mounted OR HMD is moving.
        // Note that there is also a 10s delay before User interaction returns to false. (e.g. User sets the HMD down on a desk)
        // (Side note: in Unity SteamVR plugin there's a way to read the prox sensor, but it seemed like a huge hassle to implement here)
        bool HMDUserInteraction_buffered = false; 
        bool HMDUserInteraction_previousReading = false;
        int HMDUserInteractionCounter = 0; // doesn't really make a difference for OpenVR, since we can't check the proximity sensor        
        int InitializationDivider = 0;
        int HMDUserInteractionDivider = 0;
        bool SteamVRWasOffBefore = false;


        /// <summary>
        /// ONLY UPDATE THIS FROM OpenVRStatus SETTER
        /// </summary>
        public override VRConnectionStatus Status { get; protected set; }

        OpenVRConnectionStatus _OpenVRConnStatus = OpenVRConnectionStatus.Stopped;
        public OpenVRConnectionStatus OpenVRConnStatus
        {
            get { return _OpenVRConnStatus; }
            private set
            {   
                if (_OpenVRConnStatus != value || value == OpenVRConnectionStatus.Initializing) // to get refresh on initialization attempts
                {
                    _OpenVRConnStatus = value;

                    if (_OpenVRConnStatus == OpenVRConnectionStatus.Initializing)
                    {
                        if (InitAttemptCount == 0)
                            StatusMessage = $"Initializing...";
                        else
                            StatusMessage = $"Trying to establish OpenVR headset connection... {Environment.NewLine}(Attempt #{InitAttemptCount} since startup)" +
                                        $"{Environment.NewLine}Last error: {OpenVR.GetStringForHmdError(LastOpenVRError)}";

                        Status = VRConnectionStatus.Opening;
                    }
                    else if (_OpenVRConnStatus == OpenVRConnectionStatus.NoHMD)
                    {                        
                        StatusMessage = $"OpenVR headset not found. Waiting {SleepTimeAfterQuit/1000}s before trying again." +
                                        $"{Environment.NewLine}Last error: {OpenVR.GetStringForHmdError(LastOpenVRError)}";
                        Status = VRConnectionStatus.Waiting;
                    }
                    else if (_OpenVRConnStatus == OpenVRConnectionStatus.NoSteamVR)
                    {
                        StatusMessage = "Waiting for SteamVR.";
                        Status = VRConnectionStatus.Waiting;
                    }
                    else if (_OpenVRConnStatus == OpenVRConnectionStatus.SteamVRQuit)
                    {
                        StatusMessage = $"SteamVR requested quit. Waiting {SleepTimeAfterQuit / 1000}s before trying again.";
                        Status = VRConnectionStatus.Waiting;
                    }
                    else if (_OpenVRConnStatus == OpenVRConnectionStatus.Stopped)
                    {
                        StatusMessage = $"Stopped. {LastStopMessage ?? ""}";
                        Status = VRConnectionStatus.Closed;
                    }
                    else if (_OpenVRConnStatus == OpenVRConnectionStatus.InitLimitReached)
                    {
                        StatusMessage = $"OpenVR initialization limit ({InitAttemptLimit}) reached. " +
                                        $" Restart application. {OpenVR.GetStringForHmdError(LastOpenVRError)}";
                        Status = VRConnectionStatus.InitLimitReached;
                    }
                    else if (_OpenVRConnStatus == OpenVRConnectionStatus.UnexpectedError)
                    {
                        StatusMessage = $"Unexpected error when connecting to OpenVR headset. {LastExceptionMessage ?? ""}";
                        Status = VRConnectionStatus.UnknownError;
                    }
                    else if (_OpenVRConnStatus == OpenVRConnectionStatus.AllOK)
                    {
                        StatusMessage = "OpenVR headset connection OK.";
                        Status = VRConnectionStatus.AllOK;
                    }

                    if (Worker != null && !Worker.CancellationPending)
                        Worker.ReportProgress(0, null);
                }
            }
        }


        /// <summary>
        /// Wave output device for OpenVR will always be the Windows setting. 
        /// AFAIK SteamVR sets the windows default output to match the selection in SteamVR settings (when SteamVR is launched). 
        /// At the moment I'm not going to do any further research to possibly correct this assumption.
        /// </summary>
        public override int WaveOutDeviceNumber
        {
            get
            {                
                return -1; // windows default              
            }
        }

        public OpenVRConnection()
        {
            // link all intervals to pollrate to be able to change them independently           
            InitializationDivider = InitializationInterval / PollInterval;
            HMDUserInteractionDivider = HMDUserInteractionBufferTime / PollInterval;

            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.WorkerReportsProgress = true;
            Worker.DoWork += DoWork;
            LastStopMessage = "Uninitialized.";
            OpenVRConnStatus = OpenVRConnectionStatus.Stopped;

            WorkerManager.WorkerReportsProgress = true;
            WorkerManager.DoWork += DoManagementWork;
            WorkerManager.ProgressChanged += WorkerManager_ProgressChanged;
        }

        protected override bool OpenImplementation()
        {
            if (!Worker.IsBusy && Status != VRConnectionStatus.InitLimitReached)
            {
                StopFlag = false;
                StopRequested = false;
                WorkerFailed = false;
                Worker.RunWorkerAsync();
                
                if (!WorkerManager.IsBusy)                
                    WorkerManager.RunWorkerAsync();                
                
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void CloseImplementation()
        {
            LastStopMessage = "Close was requested.";
            StopRequested = true;            
        }

        void Stop(bool initLimitReached = false)
        {
            if (!StopFlag)
            {
                if (initLimitReached)
                {
                    OpenVRConnStatus = OpenVRConnectionStatus.InitLimitReached;
                }
                else
                {
                    OpenVRConnStatus = OpenVRConnectionStatus.Stopped;
                }
                StopFlag = true;
            }
        }

        void DoManagementWork(object sender, DoWorkEventArgs e)
        {           
            while (!StopFlag)
            {   
                if (WorkerFailed)
                {
                    // wait until the worker method has completed
                    int safety = 0;
                    while (Worker.IsBusy && safety < 50) // safety in case the worker was restarted elsewhere (most likely won't happen)
                    {
                        safety++;
                        Thread.Sleep(100);
                    }
                                                
                    WorkerManager.ReportProgress(0);
                }

                Thread.Sleep(2000);
            }           
        }
        private void WorkerManager_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // currently the managers only task is to report when the worker has unexpectedly stopped --> restart            
            OpenImplementation();
        }


        void DoWork(object sender, DoWorkEventArgs e)
        {
            if (!StopFlag)            
                OpenVRConnStatus = OpenVRConnectionStatus.Initializing;

            try
            {
                while (!StopFlag)
                {
                    KeepAlive();
                    Thread.Sleep(PollInterval);
                }
            }
            catch (Exception ex)
            {
                WorkerFailed = true;
                LastExceptionMessage = ex.Message;
                OpenVRConnStatus = OpenVRConnectionStatus.UnexpectedError;
            }

            EndCurrentSession();
        }

        void KeepAlive()
        {
            KeepAliveCounter++;

            if (StopRequested)
            {
                Stop();
                return;
            }
            
            if (VRSys == null) 
            {
                if (KeepAliveCounter % InitializationDivider != 0) // do not attempt initialization on every loop.
                    return; 

                // ***** INITIALIZATION ******

                //if (!OpenVR.IsHmdPresent()) // Note that this also leaks memory

                // To avoid a memory leak, check that SteamVR is running before trying to Initialize                
                if (System.Diagnostics.Process.GetProcessesByName(Config.SteamVRProcessName).Any())
                {
                    if (InitAttemptCount >= InitAttemptLimit)
                    {
                        Stop(true); // no point to keep looping and eating memory forever                                                                    
                    }

                    InitAttemptCount++;
                    OpenVRConnStatus = OpenVRConnectionStatus.Initializing;
                    // do not carelessly call OpenVR.Init(), it will leak memory
                    VRSys = OpenVR.Init(ref LastOpenVRError, EVRApplicationType.VRApplication_Background);
                    if (LastOpenVRError != EVRInitError.None || VRSys == null)
                    {
                        if (LastOpenVRError == EVRInitError.Init_HmdNotFound || LastOpenVRError == EVRInitError.Init_HmdNotFoundPresenceFailed)
                        {
                            OpenVRConnStatus = OpenVRConnectionStatus.NoHMD;                                                             
                            Thread.Sleep(SleepTimeAfterQuit);
                        }
                        return;
                    }

                    bool hmdFound = false;
                    // check devices and find HMD index (but I suppose it's always 0) - Documentation is vague.
                    // For example, what's the purpose of OpenVR.k_unTrackedDeviceIndex_Hmd? What about multiple HMDs...
                    for (uint i = 0; i < OpenVR.k_unMaxTrackedDeviceCount; i++)
                    {
                        if (VRSys.IsTrackedDeviceConnected(i))
                        {                            
                            ETrackedDeviceClass c = VRSys.GetTrackedDeviceClass(i);
                            if (c == ETrackedDeviceClass.HMD)
                            {
                                HmdIndex = i;
                                hmdFound = true;
                                break;
                            }
                        }
                    }

                    if (!hmdFound‬)
                    {                        
                        EndCurrentSession();
                        OpenVRConnStatus = OpenVRConnectionStatus.NoHMD;                         
                        Thread.Sleep(SleepTimeAfterQuit);

                        return;
                    }

                    PoseArray = new TrackedDevicePose_t[HmdIndex + 1]; 
                    for (int i = 0; i < PoseArray.Length; i++)
                    {
                        PoseArray[i] = new TrackedDevicePose_t();
                    }

                    if (SteamVRWasOffBefore)
                    {
                        // Wait a bit more before reporting OK and allowing hmd queries when SteamVR is started AFTER CG. 
                        // The initial yaw values from the API sometimes threw the counter off by one half-turn.
                        // Maybe there's a small window at the beginning when the headset readings are not stable...
                        // This is very hard to reproduce/test as it happens so rarely. Shooting in the dark here.
                        Thread.Sleep(3000);
                        SteamVRWasOffBefore = false;
                    }
                    else
                    {
                        Thread.Sleep(500);
                    }

                    OpenVRConnStatus = OpenVRConnectionStatus.AllOK;
                }
                else
                {
                    SteamVRWasOffBefore = true;
                    OpenVRConnStatus = OpenVRConnectionStatus.NoSteamVR;
                }
            }
            else // VRSys != null (connection has been initialized)
            {
                // Check quit request
                VRSys.PollNextEvent(ref NextVREvent, (uint)System.Runtime.InteropServices.Marshal.SizeOf(NextVREvent));
                // this doesn't always work... I suppose the quit event can fly by when the poll rate is relatively low
                // It seems that SteamVR kills the VR processes that don't get Quit event with PollNextEvent().                 
                if (NextVREvent.eventType == (uint)EVREventType.VREvent_Quit)
                {
                    OpenVRConnStatus = OpenVRConnectionStatus.SteamVRQuit; // to immediately prevent native methods from being called
                    EndCurrentSession();
                    OpenVRConnStatus = OpenVRConnectionStatus.SteamVRQuit; // again to get correct status (changed in EndCurrentSession())
                    // a good sleep before starting to poll steamvr -process again
                    Thread.Sleep(SleepTimeAfterQuit);                     
                }
                else
                {   
                    bool interaction = (VRSys.GetTrackedDeviceActivityLevel(HmdIndex) == EDeviceActivityLevel.k_EDeviceActivityLevel_UserInteraction);
                    if (interaction == HMDUserInteraction_previousReading)
                        HMDUserInteractionCounter++;
                    else
                        HMDUserInteractionCounter = 0;

                    HMDUserInteraction_previousReading = interaction;

                    // some buffer to filter out false flags (and conveniently some delay for notifications)                                                                                
                    // ... although false flags are not really possible the way OpenVR currently works
                    if (HMDUserInteractionCounter > HMDUserInteractionDivider) 
                    {
                        if (HMDUserInteraction_buffered != interaction)
                        {
                            HMDUserInteraction_buffered = interaction;

                            if (interaction)
                                Worker.ReportProgress(1, null);
                            else
                                Worker.ReportProgress(2, null);
                        }
                        HMDUserInteractionCounter = 0;
                    }
                }
            }    
        }

        void EndCurrentSession()
        {
            try
            {
                if (VRSys != null)
                {
                    // Note that there seems to be a memory leak in openvr_api.
                    // Calling shutdown apparently does not release all resources...
                    // ... that were loaded with OpenVR.Init()
                    OpenVR.Shutdown();                    
                }                
            }
            catch (Exception e)
            {
                LastExceptionMessage = e.Message;
                if (!StopFlag)
                {
                    OpenVRConnStatus = OpenVRConnectionStatus.UnexpectedError;
                }
            }

            if (!StopFlag)
            {
                OpenVRConnStatus = OpenVRConnectionStatus.Initializing;
            }

            VRSys = null;
        }

        public override void Dispose()
        {
            LastStopMessage = "Disposed.";
            StopRequested = true;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnStateChange(e.ProgressPercentage);
        }

        public override bool GetHmdYaw(ref double yaw)
        {
            if (OpenVRConnStatus != OpenVRConnectionStatus.AllOK)                            
                return false;            

            VRSys.GetDeviceToAbsoluteTrackingPose(ETrackingUniverseOrigin.TrackingUniverseStanding, 0, PoseArray);
            if (!PoseArray[HmdIndex].bPoseIsValid)            
                return false;            
            
            yaw = GetYawFromOrientation(GetOrientation(PoseArray[HmdIndex].mDeviceToAbsoluteTracking));
            return true;            
        }

        public void SetSteamVRAutoStart(bool autoStart)
        {
            if (autoStart)
            {
                OpenVR.Applications.AddApplicationManifest(Config.ManifestPath, false);
                Thread.Sleep(100);
                OpenVR.Applications.SetApplicationAutoLaunch(Config.GetManifestAppKey(), true);
                Thread.Sleep(100);
            }
            else
            {
                OpenVR.Applications.SetApplicationAutoLaunch(Config.GetManifestAppKey(), false);
                Thread.Sleep(100);
                OpenVR.Applications.RemoveApplicationManifest(Config.ManifestPath);
                Thread.Sleep(100);
            }
        }

        public bool IsSteamAutoStartEnabled()
        {
            return OpenVR.Applications.GetApplicationAutoLaunch(Config.GetManifestAppKey());
        }

        public bool IsSteamAutoStartEnabled_Legacy()
        {
            return OpenVR.Applications.GetApplicationAutoLaunch("cableguardian");
        }
        public void SetLegacyAutoStartOff()
        {
            OpenVR.Applications.SetApplicationAutoLaunch("cableguardian", false);
            Thread.Sleep(100);
        }

        HmdQuaternion_t GetOrientation(HmdMatrix34_t matrix)
        {
            // conversion formula from: 
            // https://www.codeproject.com/Articles/1171122/How-to-Get-Raw-Positional-Data-from-HTC-Vive

            HmdQuaternion_t q = new HmdQuaternion_t();

            q.w = Math.Sqrt(Math.Max(0, 1 + matrix.m0 + matrix.m5 + matrix.m10)) / 2;
            q.x = Math.Sqrt(Math.Max(0, 1 + matrix.m0 - matrix.m5 - matrix.m10)) / 2;
            q.y = Math.Sqrt(Math.Max(0, 1 - matrix.m0 + matrix.m5 - matrix.m10)) / 2;
            q.z = Math.Sqrt(Math.Max(0, 1 - matrix.m0 - matrix.m5 + matrix.m10)) / 2;
            q.x = CopySign(q.x, matrix.m9 - matrix.m6);
            q.y = CopySign(q.y, matrix.m2 - matrix.m8);
            q.z = CopySign(q.z, matrix.m4 - matrix.m1);

            return q; 
        }

        double GetYawFromOrientation(HmdQuaternion_t orientation)
        {
            // don't really understand math well enough to know whether the quaternion from GetOrientation(HmdMatrix34_t) is normalized or not.
            // --> let's assume it's not. --> some additional calculation (although, in case of Yaw, only seems to affect the singularities (rare, if ever))

            // Conversion formula from:
            // http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToEuler/index.htm

            double x = orientation.x;
            double y = orientation.y;
            double z = orientation.z;
            double w = orientation.w;

            double sqx = x*x;
            double sqy = y*y;
            double sqz = z*z;
            double sqw = w*w;
            double unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor            

            // check singularity:
            double test = x * y + z * w;
            if (test > 0.499 * unit)
            { // singularity at north pole
                return 2 * Math.Atan2(x, w);
            }
            if (test < -0.499 * unit)
            { // singularity at south pole
                return -2 * Math.Atan2(x, w);
            }

            return Math.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
        }

        double CopySign(double a, double b)
        {
            if (a >= 0 && b < 0)
                return a * -1;
            else if (a >= 0)
                return a;
            else if (b < 0)
                return a;
            else
                return a * -1;            
        }
    }
}
