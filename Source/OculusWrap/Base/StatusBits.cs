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
    /// Bit flags describing the current status of sensor tracking.
    /// The values must be the same as in enum StatusBits
    /// </summary>
    /// <see cref="TrackingState"/>
    [Flags]
    public enum StatusBits
    {
        /// <summary>
        /// No flags.
        /// </summary>
        None					= 0x0000,

        /// <summary>
        /// Orientation is currently tracked (connected and in use).
        /// </summary>
        OrientationTracked		= 0x0001,

        /// <summary>
        /// Position is currently tracked (false if out of range).
        /// </summary>
        PositionTracked			= 0x0002,   
    }
}