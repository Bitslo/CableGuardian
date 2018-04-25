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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CableGuardian
{
    static class OculusWrap
    {
        /// <summary>
        /// Detects Oculus Runtime and Device Status
        ///
        /// Checks for Oculus Runtime and Oculus HMD device status without loading the LibOVRRT
        /// shared library.  This may be called before Initialize() to help decide whether or
        /// not to initialize LibOVR.
        /// </summary>
        /// <param name="timeoutMilliseconds">Specifies a timeout to wait for HMD to be attached or 0 to poll.</param>
        /// <returns>Returns a DetectResult object indicating the result of detection.</returns>
        /// <see cref="DetectResult"/>
        public static DetectResult Detect(int timeoutMilliseconds)
        {
            if (Environment.Is64BitProcess)
                return OculusNative64.ovr_Detect(timeoutMilliseconds);
            else
                return OculusNative32.ovr_Detect(timeoutMilliseconds);
        }

        /// <summary>
        /// Initializes all Oculus functionality.
        /// </summary>
        /// <param name="parameters">
        /// Initialize with extra parameters.
        /// Pass 0 to initialize with default parameters, suitable for released games.
        /// </param>
        /// <remarks>
        /// Library init/shutdown, must be called around all other OVR code.
        /// No other functions calls besides InitializeRenderingShim are allowed
        /// before Initialize succeeds or after Shutdown.
        /// 
        /// LibOVRRT shared library search order:
        ///      1) Current working directory (often the same as the application directory).
        ///      2) Module directory (usually the same as the application directory, but not if the module is a separate shared library).
        ///      3) Application directory
        ///      4) Development directory (only if OVR_ENABLE_DEVELOPER_SEARCH is enabled, which is off by default).
        ///      5) Standard OS shared library search location(s) (OS-specific).
        /// </remarks>
		public static  Result Initialize(InitParams parameters = null)
        {
            if (Environment.Is64BitProcess)
                return OculusNative64.ovr_Initialize(parameters);
            else
                return OculusNative32.ovr_Initialize(parameters);
        }

        /// <summary>
        /// Creates a handle to a VR session.
        /// 
        /// Upon success the returned IntPtr must be eventually freed with Destroy when it is no longer needed.
        /// A second call to Create will result in an error return value if the previous Hmd has not been destroyed.
        /// </summary>
        /// <param name="sessionPtr">
        /// Provides a pointer to an IntPtr which will be written to upon success.
        /// </param>
        /// <param name="pLuid">
        /// Provides a system specific graphics adapter identifier that locates which
        /// graphics adapter has the HMD attached. This must match the adapter used by the application
        /// or no rendering output will be possible. This is important for stability on multi-adapter systems. An
        /// application that simply chooses the default adapter will not run reliably on multi-adapter systems.
        /// </param>
        /// <remarks>
        /// Call Marshal.PtrToStructure(...) to convert the IntPtr to the OVR.ovrHmd type.
        /// </remarks>
        /// <returns>
        /// Returns an ovrResult indicating success or failure. Upon failure
        /// the returned pHmd will be null.
        /// </returns>
        /// <example>
        /// <code>
        /// IntPtr sessionPtr;
        /// ovrGraphicsLuid luid;
        /// ovrResult result = Create(ref session, ref luid);
        /// if(OVR_FAILURE(result))
        /// ...
        /// </code>
        /// </example>
        /// <see cref="Destroy"/>
        public static Result Create(ref IntPtr sessionPtr, ref GraphicsLuid pLuid)
        {
            if (Environment.Is64BitProcess)
                return OculusNative64.ovr_Create(ref sessionPtr, ref pLuid);
            else
                return OculusNative32.ovr_Create(ref sessionPtr, ref pLuid);
        }

        /// <summary>
        /// Destroys the HMD.
        /// </summary>
        /// <param name="sessionPtr">Specifies an IntPtr previously returned by ovr_Create.</param>
        public static void Destroy(IntPtr sessionPtr)
        {
            if (Environment.Is64BitProcess)
                OculusNative64.ovr_Destroy(sessionPtr);
            else
                OculusNative32.ovr_Destroy(sessionPtr);
        }

        /// <summary>
        /// Shuts down all Oculus functionality.
        /// </summary>
        /// <remarks>
        /// No API functions may be called after Shutdown except Initialize.
        /// </remarks>
        public static void Shutdown()
        {
            if (Environment.Is64BitProcess)
                OculusNative64.ovr_Shutdown();
            else
                OculusNative32.ovr_Shutdown();
        }

        /// <summary>
        /// Returns information about the current HMD.
        /// 
        /// ovr_Initialize must be called prior to calling this function,
        /// otherwise ovrHmdDesc::Type will be set to ovrHmd_None without
        /// checking for the HMD presence.
        /// </summary>
        /// <param name="sessionPtr">
        /// Specifies an ovrSession previously returned by ovr_Create() or NULL.
        /// </param>
        /// <returns>
        /// Returns an ovrHmdDesc. If invoked with NULL session argument, ovrHmdDesc::Type
        /// set to ovrHmd_None indicates that the HMD is not connected.
        /// </returns>
        public static HmdDesc GetHmdDesc(IntPtr sessionPtr)
        {
            HmdDesc hmdDesc;

            if (Environment.Is64BitProcess)
            {
                HmdDesc64 hmdDesc64;
                OculusNative64.ovr_GetHmdDesc64(out hmdDesc64, sessionPtr);
                hmdDesc = new HmdDesc(hmdDesc64);
            }
            else
            {
                OculusNative32.ovr_GetHmdDesc32(out hmdDesc, sessionPtr);
            }

            return hmdDesc;
        }

        /// <summary>
        /// Returns status information for the application.
        /// </summary>
        /// <param name="sessionPtr">Specifies an IntPtr previously returned by ovr_Create.</param>
        /// <param name="sessionStatus">Provides a SessionStatus that is filled in.</param>
        /// <returns>
        /// Returns an ovrResult indicating success or failure. In the case of failure, use ovr_GetLastErrorInfo 
        /// to get more information.
        /// Return values include but aren't limited to:
        /// - Result.Success: Completed successfully.
        /// - Result.ServiceConnection: The service connection was lost and the application must destroy the session.
        /// </returns>
        public static Result GetSessionStatus(IntPtr sessionPtr, ref SessionStatus sessionStatus)
        {
            if (Environment.Is64BitProcess)
                return OculusNative64.ovr_GetSessionStatus(sessionPtr, ref sessionStatus);
            else
                return OculusNative32.ovr_GetSessionStatus(sessionPtr, ref sessionStatus);
        }

        /// <summary>
        /// Returns tracking state reading based on the specified absolute system time.
        ///
        /// Pass an absTime value of 0.0 to request the most recent sensor reading. In this case
        /// both PredictedPose and SamplePose will have the same value.
        ///
        /// This may also be used for more refined timing of front buffer rendering logic, and so on.
        /// This may be called by multiple threads.
        /// </summary>
        /// <param name="sessionPtr">
        /// Specifies an IntPtr previously returned by ovr_Create.
        /// </param>
        /// <param name="absTime">
        /// Specifies the absolute future time to predict the return
        /// TrackingState value. Use 0 to request the most recent tracking state.
        /// </param>
        /// <param name="latencyMarker">
        /// Specifies that this call is the point in time where
        /// the "App-to-Mid-Photon" latency timer starts from. If a given ovrLayer
        /// provides "SensorSampleTimestamp", that will override the value stored here.
        /// </param>
        /// <returns>Returns the TrackingState that is predicted for the given absTime.</returns>
        /// <see cref="TrackingState"/>
        /// <see cref="GetEyePoses"/>
        /// <see cref="GetTimeInSeconds"/>
        public static TrackingState GetTrackingState(IntPtr sessionPtr, double absTime, bool latencyMarker)
        {
            TrackingState trackingState;
            if (Environment.Is64BitProcess)
                OculusNative64.ovr_GetTrackingState(out trackingState, sessionPtr, absTime, latencyMarker ? (byte)1 : (byte)0);
            else
                OculusNative32.ovr_GetTrackingState(out trackingState, sessionPtr, absTime, latencyMarker ? (byte)1 : (byte)0);


            return trackingState;
        }

        public static Result GetAudioDeviceOutWaveId(ref uint deviceOutId)
        {
            if (Environment.Is64BitProcess)
                return OculusNative64.ovr_GetAudioDeviceOutWaveId(ref deviceOutId);
            else
                return OculusNative32.ovr_GetAudioDeviceOutWaveId(ref deviceOutId);

        }

        public static string GetAsciiString(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            string result = System.Text.Encoding.ASCII.GetString(bytes);

            int pos = result.IndexOf('\0'); // Clean all chars after the '\0' Note: TrimEnd('\0') does not work when there are some other characters after the '\0'
            if (pos >= 0)
                result = result.Substring(0, pos);

            return result;
        }

        /// Indicates if an ovrResult indicates success.
        ///
        /// Some functions return additional successful values other than ovrSucces and
        /// require usage of this macro to indicate successs.
        public static bool OVR_SUCCESS(Result result)
        {
            return (result >= 0);
        }

    }
}
