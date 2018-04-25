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

namespace CableGuardian
{
    /// <summary>
    /// Result codes, returned by calls to Oculus SDK.
    /// </summary>
    /// <remarks>
    /// Return codes with a value of 0 or greater are consider successful, 
    /// while return codes with values less than 0 are considered failures.
    /// </remarks>
    public enum Result
    {
        #region Successful results.
        /// <summary>
        /// This is a general success result. 
        /// </summary>
        Success = 0,


        /// <summary>
        /// Returned from a call to SubmitFrame. The call succeeded, but what the app
        /// rendered will not be visible on the HMD. Ideally the app should continue
        /// calling SubmitFrame, but not do any rendering. When the result becomes
        /// ovrSuccess, rendering should continue as usual.
        /// </summary>
        Success_NotVisible = 1000,

        /// <summary>
        /// Boundary is invalid due to sensor change or was not setup.
        /// </summary>
        Success_BoundaryInvalid = 1001,

        /// <summary>
        /// Device is not available for the requested operation.
        /// </summary>
        Success_DeviceUnavailable = 1002,


        /// <summary>
        /// The HMD Firmware is out of date but is acceptable.
        /// </summary>
        HMDFirmwareMismatchSuccess = 4100,

        /// <summary>
        /// The Tracker Firmware is out of date but is acceptable.
        /// </summary>
        TrackerFirmwareMismatchSuccess = 4101,

        /// <summary>
        /// The controller firmware is out of date but is acceptable.
        /// </summary>
        ControllerFirmwareMismatchSuccess = 4104,

        /// <summary>
        /// The tracker driver interface was not found. Can be a temporary error
        /// </summary>
        TrackerDriverNotFound      = 4105,
        #endregion

        #region General errors
        /// <summary>
        /// Failure to allocate memory.
        /// </summary>
        MemoryAllocationFailure = -1000,   

        /// <summary>
        /// Failure to create a socket.
        /// </summary>
        SocketCreationFailure   = -1001,   

        /// <summary>
        /// Invalid IntPtr parameter provided.
        /// </summary>
        InvalidSession          = -1002,   

        /// <summary>
        /// The operation timed out.
        /// </summary>
        Timeout                 = -1003,   

        /// <summary>
        /// The system or component has not been initialized.
        /// </summary>
        NotInitialized          = -1004,   

        /// <summary>
        /// Invalid parameter provided. See error info or log for details.
        /// </summary>
        InvalidParameter        = -1005,   

        /// <summary>
        /// Generic service error. See error info or log for details.
        /// </summary>
        ServiceError            = -1006,   

        /// <summary>
        /// The given HMD doesn't exist.
        /// </summary>
        NoHmd                   = -1007,   

        /// <summary>
        /// Function call is not supported on this hardware/software
        /// </summary>
        Unsupported                = -1009,

        /// <summary>
        /// Specified device type isn't available.
        /// </summary>
        DeviceUnavailable          = -1010,   

        /// <summary>
        /// The headset was in an invalid orientation for the requested operation (e.g. vertically oriented during ovr_RecenterPose).
        /// </summary>
        InvalidHeadsetOrientation  = -1011,

        /// <summary>
        /// The client failed to call ovr_Destroy on an active session before calling ovr_Shutdown. Or the client crashed.
        /// </summary>
        ClientSkippedDestroy       = -1012,

        /// <summary>
        /// The client failed to call ovr_Shutdown or the client crashed.
        /// </summary>
        ClientSkippedShutdown      = -1013,

        /// <summary>
        /// The service watchdog discovered a deadlock.
        /// </summary>
        ServiceDeadlockDetected = -1014,

        /// <summary>
        /// Function call is invalid for object's current state
        /// </summary>
        InvalidOperation = -1015,

        /// <summary>
        /// Increase size of output array
        /// </summary>
        InsufficientArraySize = -1016,

        /// <summary>
        /// There is not any external camera information stored by ovrServer.
        /// </summary>
        NoExternalCameraInfo = -1017,

        /// <summary>
        /// Tracking is lost when ovr_GetDevicePoses() is called.
        /// </summary>
        LostTracking = -1018,
        #endregion

        #region Audio error range, reserved for Audio errors.
        /// <summary>
        /// First Audio error.
        /// </summary>
        AudioReservedBegin = -2000,   

        /// <summary>
        /// Failure to find the specified audio device.
        /// </summary>
        AudioDeviceNotFound		= -2001,

        /// <summary>
        /// Generic COM error.
        /// </summary>
        AudioComError			= -2002,

        /// <summary>
        /// Last Audio error.
        /// </summary>
        AudioReservedEnd		= -2999,   
        #endregion

        #region Initialization errors.
        /// <summary>
        /// Generic initialization error.
        /// </summary>
        Initialize              = -3000,   

        /// <summary>
        /// Couldn't load LibOVRRT.
        /// </summary>
        LibLoad                 = -3001,   

        /// <summary>
        /// LibOVRRT version incompatibility.
        /// </summary>
        LibVersion              = -3002,   

        /// <summary>
        /// Couldn't connect to the OVR Service.
        /// </summary>
        ServiceConnection       = -3003,   

        /// <summary>
        /// OVR Service version incompatibility.
        /// </summary>
        ServiceVersion          = -3004,   

        /// <summary>
        /// The operating system version is incompatible.
        /// </summary>
        IncompatibleOS          = -3005,   

        /// <summary>
        /// Unable to initialize the HMD display.
        /// </summary>
        DisplayInit             = -3006,   

        /// <summary>
        /// Unable to start the server. Is it already running?
        /// </summary>
        ServerStart             = -3007,   

        /// <summary>
        /// Attempting to re-initialize with a different version.
        /// </summary>
        Reinitialization        = -3008,   

        /// <summary>
        /// Chosen rendering adapters between client and service do not match
        /// </summary>
        MismatchedAdapters		= -3009,

        /// <summary>
        /// Calling application has leaked resources
        /// </summary>
        LeakingResources           = -3010,

        /// <summary>
        /// Client version too old to connect to service
        /// </summary>
        ClientVersion              = -3011,

        /// <summary>
        /// The operating system is out of date.
        /// </summary>
        OutOfDateOS                = -3012,

        /// <summary>
        /// The graphics driver is out of date.
        /// </summary>
        OutOfDateGfxDriver         = -3013,

        /// <summary>
        /// The graphics hardware is not supported
        /// </summary>
        IncompatibleGPU            = -3014,
 
        /// <summary>
        /// No valid VR display system found.
        /// </summary>
        NoValidVRDisplaySystem     = -3015,

        /// <summary>
        /// Feature or API is obsolete and no longer supported.
        /// </summary>
        Obsolete                   = -3016,

        /// <summary>
        /// No supported VR display system found, but disabled or driverless adapter found.
        /// </summary>
        DisabledOrDefaultAdapter   = -3017,

        /// <summary>
        /// The system is using hybrid graphics (Optimus, etc...), which is not support.
        /// </summary>
        HybridGraphicsNotSupported = -3018,

        /// <summary>
        /// Initialization of the DisplayManager failed.
        /// </summary>
        DisplayManagerInit         = -3019,

        /// <summary>
        /// Failed to get the interface for an attached tracker
        /// </summary>
        TrackerDriverInit          = -3020,

        /// <summary>
        /// LibOVRRT signature check failure.
        /// </summary>
        LibSignCheck = -3021,

        /// <summary>
        /// LibOVRRT path failure.
        /// </summary>
        LibPath = -3022,

        /// <summary>
        /// LibOVRRT symbol resolution failure.
        /// </summary>
        LibSymbols = -3023,

        /// <summary>
        /// Failed to connect to the service because remote connections to the service are not allowed.
        /// </summary>
        RemoteSession = -3024,

        /// <summary>
        /// Vulkan initialization error.
        /// </summary>
        InitializeVulkan = -3025,

        #endregion

        #region Hardware Errors
        /// <summary>
        /// Headset has no bundle adjustment data.
        /// </summary>
        InvalidBundleAdjustment = -4000,   

        /// <summary>
        /// The USB hub cannot handle the camera frame bandwidth.
        /// </summary>
        USBBandwidth					= -4001,   

        /// <summary>
        /// The USB camera is not enumerating at the correct device speed.
        /// </summary>
        USBEnumeratedSpeed				= -4002,

        /// <summary>
        /// Unable to communicate with the image sensor.
        /// </summary>
        ImageSensorCommError			= -4003,

        /// <summary>
        /// We use this to report various sensor issues that don't fit in an easily classifiable bucket.
        /// </summary>
        GeneralTrackerFailure			= -4004,

        /// <summary>
        /// A more than acceptable number of frames are coming back truncated.
        /// </summary>
        ExcessiveFrameTruncation		= -4005,

        /// <summary>
        /// A more than acceptable number of frames have been skipped.
        /// </summary>
        ExcessiveFrameSkipping			= -4006,

        /// <summary>
        /// The sensor is not receiving the sync signal (cable disconnected?).
        /// </summary>
        SyncDisconnected				= -4007,

        /// <summary>
        /// Failed to read memory from the sensor.
        /// </summary>
        TrackerMemoryReadFailure   = -4008,

        /// <summary>
        /// Failed to write memory from the sensor.
        /// </summary>
        TrackerMemoryWriteFailure  = -4009,

        /// <summary>
        /// Timed out waiting for a camera frame.
        /// </summary>
        TrackerFrameTimeout        = -4010,

        /// <summary>
        /// Truncated frame returned from sensor.
        /// </summary>
        TrackerTruncatedFrame      = -4011,

        /// <summary>
        /// The sensor driver has encountered a problem.
        /// </summary>
        TrackerDriverFailure       = -4012,

        /// <summary>
        /// The sensor wireless subsystem has encountered a problem.
        /// </summary>
        TrackerNRFFailure          = -4013,

        /// <summary>
        /// The hardware has been unplugged
        /// </summary>
        HardwareGone               = -4014,

        /// <summary>
        /// The nordic indicates that sync is enabled but it is not sending sync pulses
        /// </summary>
        NordicEnabledNoSync        = -4015,

        /// <summary>
        /// It looks like we're getting a sync signal, but no camera frames have been received
        /// </summary>
        NordicSyncNoFrames         = -4016,

        /// <summary>
        /// A catastrophic failure has occurred.  We will attempt to recover by resetting the device
        /// </summary>
        CatastrophicFailure        = -4017,

        /// <summary>
        /// The catastrophic recovery has timed out.
        /// </summary>
        CatastrophicTimeout        = -4018,

        /// <summary>
        /// Catastrophic failure has repeated too many times.
        /// </summary>
        RepeatCatastrophicFail     = -4019,

        /// <summary>
        /// Could not open handle for Rift device (likely already in use by another process).
        /// </summary>
        USBOpenDeviceFailure       = -4020,

        /// <summary>
        /// Unexpected HMD issues that don't fit a specific bucket.
        /// </summary>
        HMDGeneralFailure          = -4021,

        /// <summary>
        /// The HMD Firmware is out of date and is unacceptable.
        /// </summary>
        HMDFirmwareMismatchError	= -4100,

        /// <summary>
        /// The sensor Firmware is out of date and is unacceptable.
        /// </summary>
        TrackerFirmwareMismatch    = -4101,

        /// <summary>
        /// A bootloader HMD is detected by the service.
        /// </summary>
        BootloaderDeviceDetected   = -4102,

        /// <summary>
        /// The sensor calibration is missing or incorrect.
        /// </summary>
        TrackerCalibrationError    = -4103,

        /// <summary>
        /// The controller firmware is out of date and is unacceptable.
        /// </summary>
        ControllerFirmwareMismatch = -4104,

        /// <summary>
        /// Too many lost IMU samples.
        /// </summary>
        IMUTooManyLostSamples      = -4200,

        /// <summary>
        /// IMU rate is outside of the expected range.
        /// </summary>
        IMURateError               = -4201,

        /// <summary>
        /// A feature report has failed.
        /// </summary>
        FeatureReportFailure       = -4202,
	
        #endregion

        #region Synchronization Errors
        /// <summary>
        /// Requested async work not yet complete.
        /// </summary>
        Incomplete               = -5000,

        /// <summary>
        /// Requested async work was abandoned and result is incomplete.
        /// </summary>
        Abandoned                = -5001,
        #endregion

        #region Rendering errors
        /// <summary>
        /// In the event of a system-wide graphics reset or cable unplug this is returned to the app.
        /// </summary>
        DisplayLost                = -6000,

        /// <summary>
        /// ovr_CommitTextureSwapChain was called too many times on a texture swapchain without calling submit to use the chain.
        /// </summary>
        TextureSwapChainFull       = -6001,

        /// <summary>
        /// The IntPtr is in an incomplete or inconsistent state. Ensure ovr_CommitTextureSwapChain was called at least once first.
        /// </summary>
        TextureSwapChainInvalid    = -6002,

        /// <summary>
        /// Graphics device has been reset (TDR, etc...)
        /// </summary>
        GraphicsDeviceReset        = -6003,
            
        /// <summary>
        /// HMD removed from the display adapter
        /// </summary>
        DisplayRemoved             = -6004,

        /// <summary>
        /// Content protection is not available for the display
        /// </summary>
        ContentProtectionNotAvailable = -6005,

        /// <summary>
        /// Application declared itself as an invisible type and is not allowed to submit frames.
        /// </summary>
        ApplicationInvisible       = -6006,

        /// <summary>
        /// The given request is disallowed under the current conditions.
        /// </summary>
        Disallowed                 = -6007,

        /// <summary>
        /// Display portion of HMD is plugged into an incompatible port (ex: IGP)
        /// </summary>
        DisplayPluggedIncorrectly = -6008,

        #endregion

        #region  Calibration errors
        /// <summary>
        /// Result of a missing calibration block
        /// </summary>
        NoCalibration = -9000,

        /// <summary>
        /// Result of an old calibration block
        /// </summary>
        OldVersion = -9001,

        /// <summary>
        /// Result of a bad calibration block due to lengths
        /// </summary>
        MisformattedBlock = -9002, 
        #endregion
    }
}