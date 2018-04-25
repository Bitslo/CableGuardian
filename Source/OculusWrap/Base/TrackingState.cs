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

using System.Runtime.InteropServices;

namespace CableGuardian
{
    /// <summary>
    /// Tracking state at a given absolute time (describes predicted HMD pose etc).
    /// Returned by ovr_GetTrackingState.
    /// <see cref="OvrWrap.GetTrackingState"/>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TrackingState
    {
        /// <summary>
        /// Predicted head pose (and derivatives) at the requested absolute time.
        /// </summary>
        public PoseStatef	HeadPose;

        /// <summary>
        /// HeadPose tracking status described by StatusBits.
        /// </summary>
        public StatusBits	StatusFlags;

        /// <summary>
        /// The most recent calculated pose for each hand when hand controller tracking is present.
        /// HandPoses[ovrHand_Left] refers to the left hand and HandPoses[ovrHand_Right] to the right hand.
        /// These values can be combined with ovrInputState for complete hand controller information.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
        public PoseStatef[]	HandPoses;

        /// <summary>
        /// HandPoses status flags described by StatusBits.
        /// Only OrientationTracked and PositionTracked are reported.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
        public StatusBits[] HandStatusFlags;

        /// <summary>
        /// The pose of the origin captured during calibration.
        /// Like all other poses here, this is expressed in the space set by ovr_RecenterTrackingOrigin,
        /// or ovr_SpecifyTrackingOrigin and so will change every time either of those functions are
        /// called. This pose can be used to calculate where the calibrated origin lands in the new
        /// recentered space. If an application never calls ovr_RecenterTrackingOrigin or
        /// ovr_SpecifyTrackingOrigin, expect this value to be the identity pose and as such will point
        /// respective origin based on ovrTrackingOrigin requested when calling ovr_GetTrackingState.
        /// </summary>
        public Posef CalibratedOrigin;
    }
}