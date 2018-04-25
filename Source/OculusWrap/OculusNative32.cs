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

// Note by Cable Guardian dev:
// The unmanaged calls were using [SuppressUnmanagedCodeSecurity] for better performance, 
// but I did not find a performance difference in my case, and I felt safer without it :)

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace CableGuardian
{
    static class OculusNative32
    {
        public const string _ovrDllName = "LibOVRRT32_1.dll";

        /// <summary>
        /// Detects Oculus Runtime and Device Status
        ///
        /// Checks for Oculus Runtime and Oculus HMD device status without loading the LibOVRRT
        /// shared library.  This may be called before ovr_Initialize() to help decide whether or
        /// not to initialize LibOVR.
        /// </summary>
        /// <param name="timeoutMilliseconds">Specifies a timeout to wait for HMD to be attached or 0 to poll.</param>
        /// <returns>Returns a DetectResult object indicating the result of detection.</returns>
        /// <see cref="DetectResult"/>
        //[SuppressUnmanagedCodeSecurity]
        [DllImport(_ovrDllName, EntryPoint = "ovr_Detect", SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern DetectResult ovr_Detect(int timeoutMilliseconds);


        /// <summary>
        /// Initializes all Oculus functionality.
        /// </summary>
        /// <param name="parameters">
        /// Initialize with extra parameters.
        /// Pass 0 to initialize with default parameters, suitable for released games.
        /// </param>
        /// <remarks>
        /// Library init/shutdown, must be called around all other OVR code.
        /// No other functions calls besides ovr_InitializeRenderingShim are allowed
        /// before ovr_Initialize succeeds or after ovr_Shutdown.
        /// 
        /// LibOVRRT shared library search order:
        ///      1) Current working directory (often the same as the application directory).
        ///      2) Module directory (usually the same as the application directory, but not if the module is a separate shared library).
        ///      3) Application directory
        ///      4) Development directory (only if OVR_ENABLE_DEVELOPER_SEARCH is enabled, which is off by default).
        ///      5) Standard OS shared library search location(s) (OS-specific).
        /// </remarks>
        //[SuppressUnmanagedCodeSecurity]
        [DllImport(_ovrDllName, EntryPoint = "ovr_Initialize", SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Result ovr_Initialize(InitParams parameters = null);

        /// <summary>
        /// Shuts down all Oculus functionality.
        /// </summary>
        /// <remarks>
        /// No API functions may be called after ovr_Shutdown except ovr_Initialize.
        /// </remarks>
        //[SuppressUnmanagedCodeSecurity]
        [DllImport(_ovrDllName, EntryPoint = "ovr_Shutdown", SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ovr_Shutdown();

        
        /// <summary>
        /// Creates a handle to a VR session.
        /// 
        /// Upon success the returned IntPtr must be eventually freed with ovr_Destroy when it is no longer needed.
        /// A second call to ovr_Create will result in an error return value if the previous Hmd has not been destroyed.
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
        /// ovrResult result = ovr_Create(ref session, ref luid);
        /// if(OVR_FAILURE(result))
        /// ...
        /// </code>
        /// </example>
        /// <see cref="Destroy"/>
        //[SuppressUnmanagedCodeSecurity]
        [DllImport(_ovrDllName, EntryPoint = "ovr_Create", SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Result ovr_Create(ref IntPtr sessionPtr, ref GraphicsLuid pLuid);

       
        /// <summary>
        /// Destroys the HMD.
        /// </summary>
        /// <param name="sessionPtr">Specifies an IntPtr previously returned by ovr_Create.</param>
        // [SuppressUnmanagedCodeSecurity]
        [DllImport(_ovrDllName, EntryPoint = "ovr_Destroy", SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ovr_Destroy(IntPtr sessionPtr);

        /// <summary>
        /// Returns information about the current HMD.
        /// 
        /// ovr_Initialize must have first been called in order for this to succeed, otherwise HmdDesc.Type
        /// will be reported as None.
        /// 
        /// Please note: This method will should only be called by a 32 bit process. 
        /// </summary>
        /// <param name="sessionPtr">
        /// Specifies an IntPtr previously returned by ovr_Create, else NULL in which
        /// case this function detects whether an HMD is present and returns its info if so.
        /// </param>
        /// <param name="result">
        /// Returns an ovrHmdDesc. If the hmd is null and ovrHmdDesc::Type is ovr_None then
        /// no HMD is present.
        /// </param>
        //[SuppressUnmanagedCodeSecurity]
        [DllImport(_ovrDllName, EntryPoint = "ovr_GetHmdDesc", SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ovr_GetHmdDesc32(out HmdDesc result, IntPtr sessionPtr);

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
        //[SuppressUnmanagedCodeSecurity]
        [DllImport(_ovrDllName, EntryPoint = "ovr_GetSessionStatus", SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Result ovr_GetSessionStatus(IntPtr sessionPtr, ref SessionStatus sessionStatus);


        /// <summary>
        /// Returns tracking state reading based on the specified absolute system time.
        ///
        /// Pass an absTime value of 0.0 to request the most recent sensor reading. In this case
        /// both PredictedPose and SamplePose will have the same value.
        ///
        /// This may also be used for more refined timing of front buffer rendering logic, and so on.
        /// This may be called by multiple threads.
        /// </summary>
        /// <param name="result">Returns the TrackingState that is predicted for the given absTime.</param>
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
        /// <see cref="TrackingState"/>
        /// <see cref="GetEyePoses"/>
        /// <see cref="GetTimeInSeconds"/>
        //[SuppressUnmanagedCodeSecurity]
        [DllImport(_ovrDllName, EntryPoint = "ovr_GetTrackingState", SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ovr_GetTrackingState(out TrackingState result, IntPtr sessionPtr, double absTime, Byte latencyMarker);

        /// Gets the GUID of the preferred VR audio device.
        ///
        /// \param[out] deviceOutGuid The GUID of the user's preferred VR audio device to use,
        ///             which will be valid upon a successful return value, else it will be NULL.
        ///
        /// \return Returns an ovrResult indicating success or failure. In the case of failure, use
        ///         ovr_GetLastErrorInfo to get more information.
        ///
        //[SuppressUnmanagedCodeSecurity]
        [DllImport(_ovrDllName, EntryPoint = "ovr_GetAudioDeviceOutWaveId", SetLastError = false, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Result ovr_GetAudioDeviceOutWaveId(ref uint deviceOutId);
    }




}
