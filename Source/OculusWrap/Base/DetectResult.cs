// Copyright (c) 2017 AB4D d.o.o.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
// Based on OculusWrap project created by MortInfinite and licensed as Ms-PL (https://oculuswrap.codeplex.com/)

using System;
using System.Runtime.InteropServices;

namespace CableGuardian
{
    /// <summary>
    /// Return values for ovr_Detect.
    /// </summary>
    /// <see cref="OvrWrap.Detect"/>
    [StructLayout(LayoutKind.Sequential, Pack=8, Size=8)]
    public struct DetectResult
    {
        // Using MarshalAs(UnmanagedType.U1) with this struct gets a "Method's type signature is not PInvoke compatible" exception (probably because of Size=8)
        //[MarshalAs(UnmanagedType.U1)] // Marshal byte to bool (0 = false, all other = true)
        //public bool IsOculusServiceRunning;

        //[MarshalAs(UnmanagedType.U1)] // Marshal byte to bool (0 = false, all other = true)
        //public bool IsOculusHMDConnected;


        private byte _isOculusServiceRunning;
        private byte _isOculusHMDConnected;

        /// <summary>
        /// Is False when the Oculus Service is not running.
        ///   This means that the Oculus Service is either uninstalled or stopped.
        ///   IsOculusHMDConnected will be ovrFalse in this case.
        /// Is True when the Oculus Service is running.
        ///   This means that the Oculus Service is installed and running.
        ///   IsOculusHMDConnected will reflect the state of the HMD.
        /// </summary>
        public bool IsOculusServiceRunning
        {
            get
            {
                return (int)this._isOculusServiceRunning > 0;
            }
        }

        /// <summary>
        /// Is False when an Oculus HMD is not detected.
        ///   If the Oculus Service is not running, this will be ovrFalse.
        /// Is True when an Oculus HMD is detected.
        ///   This implies that the Oculus Service is also installed and running.
        /// </summary>
        public bool IsOculusHMDConnected
        {
            get
            {
                return (int)this._isOculusHMDConnected > 0;
            }
        }
    }
}