using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CableGuardian
{
    public enum VRConnectionStatus { AllOK = 1, Closed = 0, Waiting = -1, Opening = -2, APIError = -3, UnknownError = -4 }
        
    abstract class VRConnection : IDisposable
    {
        public EventHandler<EventArgs> StatusChanged;
        public EventHandler<EventArgs> StatusChangedToAllOK;
        public abstract VRConnectionStatus Status { get; protected set; }
        public string StatusMessage { get; protected set; }
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

        protected void InvokeStatusChanged()
        {
            if (StatusChanged != null)
            {
                StatusChanged(this, new EventArgs());
            }

            if (Status == VRConnectionStatus.AllOK && StatusChangedToAllOK != null)
            {
                StatusChangedToAllOK(this, new EventArgs());
            }
        }

    }
}
