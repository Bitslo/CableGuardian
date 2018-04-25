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
    /// Signature for the logging callback.
    /// </summary>
    /// <param name="userData">UserData is an arbitrary value specified by the user of ovrInitParams.</param>
    /// <param name="level">Level is one of the ovrLogLevel constants.</param>
    /// <param name="message">Message is a UTF8-encoded null-terminated string.</param>
    /// <see cref="InitParams"/>
    /// <seealso cref="LogLevel"/>
    /// <seealso cref="OvrWrap.Initialize"/>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LogCallback(IntPtr userData, LogLevel level, string message);
}