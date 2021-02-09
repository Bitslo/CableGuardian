using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CableGuardian
{
    public enum VRConnectionStatus { AllOK = 1, Closed = 0, Waiting = -1, Opening = -2, APIError = -3, UnknownError = -4, InitLimitReached = -5 }
        
    abstract class VRConnection : IDisposable
    {
        public event EventHandler<EventArgs> StatusChanged;
        public event EventHandler<EventArgs> StatusChangedToAllOK;
        public event EventHandler<EventArgs> StatusChangedToNotOK;
        public event EventHandler<EventArgs> HMDUserInteractionStarted;
        public event EventHandler<EventArgs> HMDUserInteractionStopped;
        public abstract VRConnectionStatus Status { get; protected set; }
        private VRConnectionStatus PreviousStatus { get; set; } = VRConnectionStatus.Closed;
        public string StatusMessage { get; protected set; } = "";
        /// <summary>
        /// Returns the wave out device index used by the VR API
        /// </summary>
        public abstract int WaveOutDeviceNumber { get; }
        /// <summary>
        /// returns true when yaw contains a valid value
        /// </summary>
        /// <param name="yaw"></param>
        /// <returns></returns>
        public abstract bool GetHmdYaw(ref double yaw);        
        /// <summary>
        /// Returns true if connection was opened for listening to VR API.
        /// NOTE that this does not mean that the API connection is ok. Check Status for that.
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            return OpenImplementation();            
        }
        protected abstract bool OpenImplementation();
        public void Close()
        {
            CloseImplementation();     
        }
        protected abstract void CloseImplementation();
        public abstract void Dispose();

        protected void OnStateChange(int changeType)
        {
            if (changeType == 0) // a gimmick to recognize what to report
            {
                InvokeStatusChanged();
            }
            // later additions... wanted to keep user interaction separate from the connection "status":
            else if (changeType == 1)
            {
                InvokeHMDUserInteractionStarted();
            }
            else if (changeType == 2)
            {
                InvokeHMDUserInteractionStopped();
            }
        }

        protected void InvokeStatusChanged()
        {
            StatusChanged?.Invoke(this, EventArgs.Empty);

            if (Status == VRConnectionStatus.AllOK && StatusChangedToAllOK != null)
            {
                StatusChangedToAllOK(this, EventArgs.Empty);
            }

            if (PreviousStatus == VRConnectionStatus.AllOK && Status != VRConnectionStatus.AllOK && StatusChangedToNotOK != null)
            {
                StatusChangedToNotOK(this, EventArgs.Empty);
            }

            PreviousStatus = Status;
        }

        protected void InvokeHMDUserInteractionStarted()
        {
            HMDUserInteractionStarted?.Invoke(this, EventArgs.Empty);
        }

        protected void InvokeHMDUserInteractionStopped()
        {
            HMDUserInteractionStopped?.Invoke(this, EventArgs.Empty);
        }

    }
}
