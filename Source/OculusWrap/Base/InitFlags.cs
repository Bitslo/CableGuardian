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
    /// Flags for Initialize()
    /// </summary>
    [Flags]
    public enum InitFlags
    {
        /// <summary>
        /// No flags specified.
        /// </summary>
        None = 0x00000000,

        /// <summary>
        /// When a debug library is requested, a slower debugging version of the library will
        /// be run which can be used to help solve problems in the library and debug game code.
        /// </summary>
        Debug = 0x00000001,

        /// <summary>
        /// When a version is requested, the LibOVR runtime respects the RequestedMinorVersion
        /// field and verifies that the RequestedMinorVersion is supported. Normally when you 
        /// specify this flag you simply use OVR_MINOR_VERSION for ovrInitParams::RequestedMinorVersion,
        /// though you could use a lower version than OVR_MINOR_VERSION to specify previous 
        /// version behavior.
        /// </summary>
        RequestVersion = 0x00000004,

        /// <summary>
        /// This client will not be visible in the HMD.
        /// Typically set by diagnostic or debugging utilities.
        /// </summary>
        Invisible = 0x00000010,

        /// <summary>
        /// This client will alternate between VR and 2D rendering.
        /// Typically set by game engine editors and VR-enabled web browsers.
        /// </summary>
        MixedRendering = 0x00000020,


        /// <summary>
        /// These bits are writable by user code.
        /// </summary>
        WritableBits = 0x00ffffff
    }
}