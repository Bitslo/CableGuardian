
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CableGuardian
{
    public enum OculusConnectionStatus { AllOk, Initialized, NoHMD, NoService, InitFail, CreateFail, UnexpectedError, Stopped, Initializing, Resurrecting, WaitingHome, OculusVRQuit }

    /// <summary>
    /// Provides an Oculus HMD connection that is automatically polled and kept alive.
    /// Internally, "keeping alive" is actually more like resurrection:
    /// If the HMD is lost, the existing Oculus session is destroyed and a new session is created (when possible).
    /// Status == AllOk when connection is up and running (and public methods are usable).
    /// </summary>
    class OculusConnection : VRConnection
    {
        bool SessionInitialized = false;
        bool SessionCreated = false;
        bool SuppressStatusEvents = false;
        bool StopFlag = false;
        int PollInterval = 50;
        IntPtr SessionPtr;
        GraphicsLuid pLuid;
        BackgroundWorker Worker = new BackgroundWorker();
        public bool RequireHome { get; set; }
        bool StopRequested = false;
        SessionStatus SesStatus = new SessionStatus();       
        bool HomePresent = false;
        int _KeepAliveCounter = 0;
        int KeepAliveCounter { get { return _KeepAliveCounter; } set { _KeepAliveCounter = (_KeepAliveCounter >= 10000) ? 1 : value; } }
        bool HMDMounted_buffered = false;
        bool HMDMounted_previousReading = false;
        int HMDMountBuffer = 0;
        
        /// <summary>
        /// ONLY UPDATE THIS FROM OculusStatus SETTER
        /// </summary>
        public override VRConnectionStatus Status { get; protected set; }

        OculusConnectionStatus _OculusStatus;
        public OculusConnectionStatus OculusStatus
        {
            get { return _OculusStatus; }
            private set
            {
                if (_OculusStatus != value)
                {
                    _OculusStatus = value;                    
                    if (_OculusStatus == OculusConnectionStatus.NoHMD)
                    {
                        StatusMessage = "Searching for an Oculus VR headset.";
                        Status = VRConnectionStatus.APIError;
                    }
                    else if (_OculusStatus == OculusConnectionStatus.NoService)
                    {
                        StatusMessage = "Oculus VR Runtime Service is not running.";
                        Status = VRConnectionStatus.APIError;
                    }
                    else if (_OculusStatus == OculusConnectionStatus.Initializing)
                    {
                        StatusMessage = $"Initializing connection.";
                        Status = VRConnectionStatus.Opening;
                    }
                    else if (_OculusStatus == OculusConnectionStatus.InitFail)
                    {
                        StatusMessage = $"Oculus HMD initialization failed. {LastOculusResult.ToString()}";
                        Status = VRConnectionStatus.APIError;
                    }
                    else if (_OculusStatus == OculusConnectionStatus.CreateFail)
                    {
                        StatusMessage = $"Oculus HMD creation failed. {LastOculusResult.ToString()}";
                        Status = VRConnectionStatus.APIError;
                    }
                    else if (_OculusStatus == OculusConnectionStatus.Resurrecting)
                    {
                        StatusMessage = "Oculus HMD connection lost. Reconnecting...";
                        Status = VRConnectionStatus.Opening;
                    }
                    else if (_OculusStatus == OculusConnectionStatus.UnexpectedError)
                    {
                        StatusMessage = $"Unexpected error when connecting to Oculus HMD. {LastExceptionMessage ?? ""}";
                        Status = VRConnectionStatus.UnknownError;
                    }
                    else if (_OculusStatus == OculusConnectionStatus.OculusVRQuit)
                    {
                        StatusMessage = "OculusVR service requested quit. Waiting 15s before attempting to reconnect...";
                        Status = VRConnectionStatus.Waiting;
                    }
                    else if (_OculusStatus == OculusConnectionStatus.Stopped)
                    {
                        StatusMessage = "Stopped.";
                        Status = VRConnectionStatus.Closed;
                    }
                    else if (_OculusStatus == OculusConnectionStatus.WaitingHome)
                    {
                        StatusMessage = "Waiting for Oculus Home.";
                        Status = VRConnectionStatus.Waiting;
                    }
                    else if (_OculusStatus == OculusConnectionStatus.Initialized)
                    {
                        StatusMessage = "Oculus library initialized... creating HMD connection.";
                        Status = VRConnectionStatus.Opening;
                    }
                    else if (_OculusStatus == OculusConnectionStatus.AllOk)
                    {
                        StatusMessage = "Oculus HMD connection OK.";
                        Status = VRConnectionStatus.AllOK;
                    }

                    if (!SuppressStatusEvents)
                        Worker.ReportProgress(0, null);
                }
                
            }
        }

        Result LastOculusResult;
        string LastExceptionMessage { get; set; }
        /// <summary>
        /// Audio output device set in Oculus settings. 
        /// </summary>
        public override int WaveOutDeviceNumber
        {
            get
            {
                if (OculusStatus == OculusConnectionStatus.AllOk)
                {
                    try
                    {
                        // use native call instead of a stored value in case user changes Oculus setting.
                        // shouldn't be too much overhead
                        return GetAudioDeviceOutId();
                    }
                    catch (Exception)
                    {
                        return -1; // windows default
                    }
                }
                else
                {
                    return -1;
                }                
            }
        }

        public bool OculusHMDConnected()
        {
            try
            {
                DetectResult detection = OculusWrap.Detect(1000);
                if (detection.IsOculusHMDConnected)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                // intentionally ignore and just assume not connected    
            }

            return false;
        }
        
        public OculusConnection()
        {
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.WorkerReportsProgress = true;
            Worker.DoWork += DoWork;
            OculusStatus = OculusConnectionStatus.Stopped;
        }

        protected override bool OpenImplementation()
        {
            if (!Worker.IsBusy)
            {
                StopFlag = false;
                StopRequested = false;
                SuppressStatusEvents = true;
                OculusStatus = OculusConnectionStatus.Initializing;
                SuppressStatusEvents = false;
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
            StopRequested = true;            
        }

        void Stop()
        {
            if (!StopFlag)
            {   
                OculusStatus = OculusConnectionStatus.Stopped;                
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
                    OculusStatus = OculusConnectionStatus.UnexpectedError;
                }
                Thread.Sleep(PollInterval);
            }

            EndCurrentSession();
        }
              

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnStateChange(e.ProgressPercentage);
        }
       
        void KeepAlive()
        {
            KeepAliveCounter++;

            if (StopRequested)
            {
                Stop();
                return;
            }

            if (RequireHome)
            {
                if (KeepAliveCounter % 40 == 0) // to save CPU, check only every 2 secs (when pollrate = 50ms)
                {
                    if (System.Diagnostics.Process.GetProcessesByName(Config.OculusHomeProcessName).Any() == false)
                    {
                        if (OculusStatus != OculusConnectionStatus.WaitingHome)
                        {
                            EndCurrentSession();
                            HomePresent = false;
                            OculusStatus = OculusConnectionStatus.WaitingHome;
                        }
                    }
                    else
                    {                        
                        HomePresent = true;                       
                    }
                }
                if (!HomePresent)
                {
                    return;
                }
            }  

            if (OculusStatus == OculusConnectionStatus.AllOk)
            {
                // poll quit message on every lap
                LastOculusResult = OculusWrap.GetSessionStatus(SessionPtr, ref SesStatus);
                if (!OculusWrap.OVR_SUCCESS(LastOculusResult) || (SesStatus.ShouldQuit || SesStatus.DisplayLost || !SesStatus.HmdPresent))
                {
                    //StopRequested = true;
                    OculusStatus = OculusConnectionStatus.OculusVRQuit; // to immediately prevent native methods from being called
                    EndCurrentSession();
                    OculusStatus = OculusConnectionStatus.OculusVRQuit;  // again to get correct status (changed in EndCurrentSession())
                    // a good sleep before starting to poll OculusVR again
                    Thread.Sleep(15000);                     
                }
                else if (OculusWrap.OVR_SUCCESS(LastOculusResult))
                {
                    bool mounted = SesStatus.HmdMounted;
                    if (mounted == HMDMounted_previousReading)                    
                        HMDMountBuffer++;                    
                    else                    
                        HMDMountBuffer = 0;

                    HMDMounted_previousReading = mounted;

                    if (HMDMountBuffer > 20) // some buffer to filter out false flags (and conveniently some delay for notifications)
                    {
                        if (HMDMounted_buffered != mounted)
                        {
                            HMDMounted_buffered = mounted;

                            if (mounted)
                                Worker.ReportProgress(1, null);
                            else
                                Worker.ReportProgress(2, null);
                        }                        
                        HMDMountBuffer = 0;
                    }
                }
                return;
            }


            // This point is reached only when connection not yet created 
            // ******** INITIALIZATION & CREATION ***********

            if (KeepAliveCounter % 20 != 0) // try initialization one per second (when pollrate = 50 ms)
                return;

            if (OculusStatus == OculusConnectionStatus.Initialized)
            {
                LastOculusResult = OculusWrap.Create(ref SessionPtr, ref pLuid);
                if (OculusWrap.OVR_SUCCESS(LastOculusResult))
                {
                    OculusStatus = OculusConnectionStatus.AllOk;
                    SessionCreated = true;                        
                }              
            }
            else
            {                
                DetectResult detection = OculusWrap.Detect(1000);
                if (detection.IsOculusHMDConnected)
                {
                    uint libMinorVersion = 0;
                    LastOculusResult = Result.LibVersion;
                    // To initialize directly from LibOVRRT-dll, the requested version is required.
                    // --> Try initialization until reaching the first valid version. (Future proofing.)
                    // (Oculus library defines a lowest requestable version, but it can change in the future.)
                    while (!OculusWrap.OVR_SUCCESS(LastOculusResult) && libMinorVersion < 100)
                    {
                        InitParams par = new InitParams(InitFlags.RequestVersion | InitFlags.Invisible, libMinorVersion);
                        LastOculusResult = OculusWrap.Initialize(par);
                        libMinorVersion++;
                    }                    
                    if (OculusWrap.OVR_SUCCESS(LastOculusResult))
                    {
                        OculusStatus = OculusConnectionStatus.Initialized;
                        SessionInitialized = true;
                    }
                    else
                    {
                        OculusStatus = OculusConnectionStatus.InitFail;                        
                    }
                }
                else if(detection.IsOculusServiceRunning)
                {
                    OculusStatus = OculusConnectionStatus.NoHMD;
                }
                else
                {
                    OculusStatus = OculusConnectionStatus.NoService;
                }
            }
        }

        void EndCurrentSession()
        {
            try
            {
                if (SessionCreated)
                {
                    OculusWrap.Destroy(SessionPtr);
                    SessionCreated = false;
                }
                if (SessionInitialized)
                {
                    OculusWrap.Shutdown();
                    SessionInitialized = false;
                }
            }
            catch (Exception e)
            {                
                LastExceptionMessage = e.Message;
                if (!StopFlag)
                {
                    OculusStatus = OculusConnectionStatus.UnexpectedError;
                }
            }

            if (!StopFlag)
            {
                OculusStatus = OculusConnectionStatus.Resurrecting;                
            }

            SessionPtr = IntPtr.Zero;            
        }
                      
        public override void Dispose()
        {
            StopRequested = true;       
        }
        
        public override bool GetHmdYaw(ref double yaw)
        {
            if (StopRequested)
                return false;

            if (OculusStatus != OculusConnectionStatus.AllOk)            
                return false;        

            TrackingState ts = OculusWrap.GetTrackingState(SessionPtr, 0, false);            
            // I suppose for Oculus there is no need to check if the received orientation is valid... (compare to OpenVR)
            yaw = GetYawFromOrientation(ts.HeadPose.ThePose.Orientation);
            return true;
        }

        double GetYawFromOrientation(Quaternionf orientation)
        {
            // From Oculus SDK documentation: "Rotation is maintained as a unit quaternion"  (= normalized quat)    
            float x = orientation.X;
            float y = orientation.Y;
            float z = orientation.Z;
            float w = orientation.W;

            // Conversion formula from:
            // http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToEuler/index.htm

            // check singularity:
            double test = x * y + z * w;
            if (test > 0.499)
            { // singularity at north pole
                return 2 * Math.Atan2(x, w);
            }
            if (test < -0.499)
            { // singularity at south pole
                return -2 * Math.Atan2(x, w);
            }

            return Math.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
        }

        int GetAudioDeviceOutId()
        {
            if (OculusStatus != OculusConnectionStatus.AllOk)
                return -1; // windows default 

            uint id = 0;
            Result r = OculusWrap.GetAudioDeviceOutWaveId(ref id);
            return (int)id;
        }


    }
}
