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
using System.Text;

namespace CableGuardian
{
    /// <summary>
    /// 64 bit version of the HmdDesc.
    /// </summary>
    /// <remarks>
    /// This class is needed because the Oculus SDK defines padding fields on the 64 bit version of the Oculus SDK.
    /// </remarks>
    /// <see cref="HmdDesc"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct HmdDesc64
    {
        /// <summary>
        /// The type of HMD.
        /// </summary>
        public HmdType Type;
    
        /// <summary>
        /// Internal struct paddding.
        /// </summary>
        private int Pad0;

        /// <summary>
        /// Byte array for ProductName string
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=64)]
        public byte[] ProductNameBytes;

        /// <summary>
        /// Name string describing the product: "Oculus Rift DK1", etc.
        /// </summary>
        public string ProductName
        {
            get
            {
                return OculusWrap.GetAsciiString(ProductNameBytes);
            }
        }

        /// <summary>
        /// Byte array for Manufacturer string
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=64)]
        public byte[] ManufacturerBytes;

        /// <summary>
        /// HMD manufacturer identification string.
        /// </summary>
        public string Manufacturer
        {
            get
            {
                return OculusWrap.GetAsciiString(ManufacturerBytes);
            }
        }

        /// <summary>
        /// HID (USB) vendor identifier of the device.
        /// </summary>
        public short VendorId;

        /// <summary>
        /// HID (USB) product identifier of the device.
        /// </summary>
        public short ProductId;

        /// <summary>
        /// Sensor (and display) serial number.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=24)]
        public byte[] SerialNumber;

        /// <summary>
        /// Sensor firmware major version.
        /// </summary>
        public short FirmwareMajor;

        /// <summary>
        /// Sensor firmware minor version.
        /// </summary>
        public short FirmwareMinor;

        /// <summary>
        /// Capability bits described by HmdCaps which the HMD currently supports.
        /// </summary>
        public HmdCaps AvailableHmdCaps;

        /// <summary>
        /// Capability bits described by HmdCaps which are default for the current Hmd.
        /// </summary>
        public HmdCaps DefaultHmdCaps;

        /// <summary>
        /// Capability bits described by TrackingCaps which the system currently supports.
        /// </summary>
        public TrackingCaps AvailableTrackingCaps;

        /// <summary>
        /// Capability bits described by ovrTrackingCaps which are default for the current system.
        /// </summary>
        public TrackingCaps DefaultTrackingCaps;

        /// <summary>
        /// Defines the recommended FOVs for the HMD.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
        public FovPort[] DefaultEyeFov;

        /// <summary>
        /// Defines the maximum FOVs for the HMD.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
        public FovPort[] MaxEyeFov;

        /// <summary>
        /// Resolution of the full HMD screen (both eyes) in pixels.
        /// </summary>
        public Sizei Resolution;

        /// <summary>
        /// Nominal refresh rate of the display in cycles per second at the time of HMD creation.
        /// </summary>
        public float DisplayRefreshRate;

        /// <summary>
        /// Internal struct paddding.
        /// </summary>
        private int Pad1;
    }
}