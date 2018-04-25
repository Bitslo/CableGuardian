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

namespace CableGuardian
{
    /// <summary>
    /// Tracking capability bits reported by the device.
    /// Used with ovr_GetTrackingCaps.
    /// </summary>
    [Flags]
    public enum TrackingCaps
    {
        /// <summary>
        /// No flags.
        /// </summary>
        None				= 0x0000,

        /// <summary>
        /// Supports orientation tracking (IMU).
        /// </summary>
        Orientation			= 0x0010,

        /// <summary>
        /// Supports yaw drift correction via a magnetometer or other means.
        /// </summary>
        MagYawCorrection	= 0x0020,

        /// <summary>
        /// Supports positional tracking.
        /// </summary>
        Position			= 0x0040,
    }
}