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
    public enum OpenVRConnectionStatus { AllOK, NoHMD, Initializing, NoSteamVR, SteamVRQuit, StoppedOnInitError, UnexpectedError, Stopped }

    class OpenVRConnection : VRConnection
    {        
        public double Yaw = 0;
        BackgroundWorker Worker = new BackgroundWorker();
        bool StopFlag = false;
        int PollInterval = 100; // needs to be quick enough to catch the quit message from steamvr
        EVRInitError LastOpenVRError = EVRInitError.None;
        CVRSystem VRSys = null;
        string LastExceptionMessage { get; set; }
        string LastStopMessage { get; set; }       
        VREvent_t NextVREvent = new VREvent_t();
        TrackedDevicePose_t[] PoseArray;        
        bool StopRequested = false;       
        uint HmdIndex = 0;
        int _KeepAliveCounter = 0;
        int KeepAliveCounter { get { return _KeepAliveCounter; } set { _KeepAliveCounter = (_KeepAliveCounter >= 100) ? 1 : value; } }


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
                if (_OpenVRConnStatus != value)
                {
                    _OpenVRConnStatus = value;

                    if (_OpenVRConnStatus == OpenVRConnectionStatus.Initializing)
                    {
                        StatusMessage = "Preparing OpenVR HMD connection.";
                        Status = VRConnectionStatus.Opening;
                    }
                    else if (_OpenVRConnStatus == OpenVRConnectionStatus.NoHMD)
                    {
                        StatusMessage = "OpenVR HMD not found.";
                        Status = VRConnectionStatus.APIError;
                    }
                    else if (_OpenVRConnStatus == OpenVRConnectionStatus.NoSteamVR)
                    {
                        StatusMessage = "Waiting for SteamVR.";
                        Status = VRConnectionStatus.Waiting;
                    }
                    else if (_OpenVRConnStatus == OpenVRConnectionStatus.SteamVRQuit)
                    {
                        StatusMessage = "SteamVR requested quit. Waiting to reconnect...";
                        Status = VRConnectionStatus.Waiting;
                    }
                    else if (_OpenVRConnStatus == OpenVRConnectionStatus.Stopped)
                    {
                        StatusMessage = $"Stopped. {LastStopMessage ?? ""}";
                        Status = VRConnectionStatus.Closed;
                    }
                    else if (_OpenVRConnStatus == OpenVRConnectionStatus.StoppedOnInitError)
                    {
                        StatusMessage = $"OpenVR HMD initialization failed. Make sure SteamVR is running." +
                                        $" Manual retry is required. {OpenVR.GetStringForHmdError(LastOpenVRError)}";
                        Status = VRConnectionStatus.Closed;
                    }
                    else if (_OpenVRConnStatus == OpenVRConnectionStatus.UnexpectedError)
                    {
                        StatusMessage = $"Unexpected error when connecting to OpenVR HMD. {LastExceptionMessage ?? ""}";
                        Status = VRConnectionStatus.UnknownError;
                    }
                    else if (_OpenVRConnStatus == OpenVRConnectionStatus.AllOK)
                    {
                        StatusMessage = "OpenVR HMD connection OK.";
                        Status = VRConnectionStatus.AllOK;
                    }
                    
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
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.WorkerReportsProgress = true;
            Worker.DoWork += DoWork;
            LastStopMessage = "Uninitialized.";
            OpenVRConnStatus = OpenVRConnectionStatus.Stopped;
            
        }

        protected override bool OpenImplementation()
        {
            if (!Worker.IsBusy)
            {
                StopFlag = false;
                StopRequested = false;
                Worker.RunWorkerAsync();
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

        void Stop(bool initError = false)
        {
            if (!StopFlag)
            {
                if (initError)
                {
                    OpenVRConnStatus = OpenVRConnectionStatus.StoppedOnInitError;
                }
                else
                {
                    OpenVRConnStatus = OpenVRConnectionStatus.Stopped;
                }
                StopFlag = true;
            }
        }

        void DoWork(object sender, DoWorkEventArgs e)
        {
            while (!StopFlag)
            {                
                try
                {
                    KeepAlive();
                }
                catch (Exception ex)
                {
                    LastExceptionMessage = ex.Message;
                    OpenVRConnStatus = OpenVRConnectionStatus.UnexpectedError;
                }
                
                Thread.Sleep(PollInterval);
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
                if (KeepAliveCounter % 10 != 0) // initialization attempt on only every tenth lap
                    return; 

                // ***** INITIALIZATION ******

                //if (!OpenVR.IsHmdPresent()) // Note that this also leaks memory
                
                // To avoid a memory leak, check that SteamVR is running before trying to Initialize                
                if (System.Diagnostics.Process.GetProcessesByName(Config.SteamVRProcessName).Any())
                {
                    // do not carelessly call OpenVR.Init(), it will leak memory
                    VRSys = OpenVR.Init(ref LastOpenVRError, EVRApplicationType.VRApplication_Background);
                    if (LastOpenVRError != EVRInitError.None || VRSys == null)
                    {                        
                        Stop(true); // no point to keep looping and eating memory
                                    // let user try again manually
                        return;
                    }

                    int deviceCount = 0;
                    // check devices and find HMD index (but I suppose it's always 0) - Documentation is vague.
                    // For example, what's the purpose of OpenVR.k_unTrackedDeviceIndex_Hmd? What about multiple HMDs...
                    for (uint i = 0; i < OpenVR.k_unMaxTrackedDeviceCount; i++)
                    {
                        if (VRSys.IsTrackedDeviceConnected(i))
                        {
                            deviceCount++;
                            ETrackedDeviceClass c = VRSys.GetTrackedDeviceClass(i);
                            if (c == ETrackedDeviceClass.HMD)
                            {
                                HmdIndex = i;
                                break;
                            }
                        }
                    }

                    if (deviceCount == 0)
                    {
                        LastStopMessage = "No VR devices connected.";
                        Stop();
                        return;
                    }

                    PoseArray = new TrackedDevicePose_t[HmdIndex + 1]; 
                    for (int i = 0; i < PoseArray.Length; i++)
                    {
                        PoseArray[i] = new TrackedDevicePose_t();
                    }

                    OpenVRConnStatus = OpenVRConnectionStatus.AllOK;
                }
                else
                {
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
                    Thread.Sleep(15000);
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
            InvokeStatusChanged();
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
