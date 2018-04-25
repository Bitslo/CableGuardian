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
    /// HMD capability bits reported by device.
    /// </summary>
    [Flags]
    public enum HmdCaps
    {
        /// <summary>
        /// No flags.
        /// </summary>
        None = 0x0000,

        // Read only flags

        /// <summary>
        /// Means HMD device is a virtual debug device.
        /// </summary>
        /// <remarks>
        /// (read only) 
        /// </remarks>
        DebugDevice = 0x0010,
    }
}