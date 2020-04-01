using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

namespace CableGuardian
{
    class VRObserverEventArgs : EventArgs
    {
        /// <summary>
        /// Current rotation around the y-axis (= Yaw) in radians.
        /// (range from -PI to +PI) (sign denotes direction from center - left or right depending on coordinate system)
        /// </summary>
        public double HmdYaw { get; }        
        public VRObserverEventArgs(double hmdYaw)
        {
            HmdYaw = hmdYaw;            
        }
    }

    class VRObserver
    {
        public event EventHandler<VRObserverEventArgs> StateRefreshed;
                
        VRConnection VR;
        double HmdYaw;        

        BackgroundWorker Worker = new BackgroundWorker();
        bool StopFlag = false;

        /// <summary>
        /// Interval (ms) to read statistics from the VR API connection
        /// </summary>
        public int PollInterval { get; set; }

        /// <summary>
        /// Periodically reports statistics from an active VR API connection.
        /// </summary>
        /// <param name="vr"></param>
        /// <param name="pollInterval"></param>
        public VRObserver(VRConnection vr, int pollInterval = 150)
        {
            VR = vr ?? throw new Exception("null VR connection.");
            PollInterval = pollInterval;
            
            Worker.DoWork += DoWork;
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.WorkerReportsProgress = true;                        
        }

        public void SetVRConnection(VRConnection vr)
        {
            VR = vr ?? throw new Exception("null VR connection.");
        }

        public void Start()
        {
            if (!Worker.IsBusy)
            {
                StopFlag = false;
                Worker.RunWorkerAsync();
            }
        }

        public void Stop()
        {
            StopFlag = true;
        }
        

        /// <summary>
        /// NEVER MAKE CHANGES TO VR-CONNECTION FROM THIS THREAD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DoWork(object sender, DoWorkEventArgs e)
        {
            while (StopFlag == false)
            {
                if(VR.GetHmdYaw(ref HmdYaw))
                    Worker.ReportProgress(0, null);
               
                Thread.Sleep(PollInterval);
            }            
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InvokeStateRefreshed();
        }

        void InvokeStateRefreshed()
        {
            StateRefreshed?.Invoke(this, new VRObserverEventArgs(HmdYaw));
        }
    }
}
