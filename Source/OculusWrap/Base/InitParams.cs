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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using IntPtr = System.IntPtr;

namespace CableGuardian
{
	/// <summary>
	/// Parameters for the ovr_Initialize call.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack=8)]
	public class InitParams
	{
		/// <summary>
		/// Flags from ovrInitFlags to override default behavior.
		/// Pass 0 for the defaults.
		/// </summary>
		/// <remarks>
		/// Combination of ovrInitFlags or 0
		/// </remarks>
		public InitFlags Flags;

		/// <summary>
		/// Request a specific minimum minor version of the LibOVR runtime.
		/// Flags must include ovrInit_RequestVersion or this will be ignored.
		/// </summary>
		public uint RequestedMinorVersion;

		/// <summary>
		/// Log callback function, which may be called at any time asynchronously from
		/// multiple threads until ovr_Shutdown() completes.
		/// 
		/// Pass null for no log callback.
		/// </summary>
		/// <remarks>
		/// Function pointer or 0
		/// </remarks>
		[MarshalAs(UnmanagedType.FunctionPtr)]
		public LogCallback LogCallback;

        /// <summary>
        /// User-supplied data which is passed as-is to LogCallback. Typically this 
        /// is used to store an application-specific pointer which is read in the 
        /// callback function.
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
        public IntPtr UserData;

		/// <summary>
		/// Number of milliseconds to wait for a connection to the server.
		/// 
		/// Pass 0 for the default timeout.
		/// </summary>
		/// <remarks>
		/// Timeout in Milliseconds or 0
		/// </remarks>
		public uint ConnectionTimeoutMS;

        /// <summary>
        /// Constructor
        /// </summary>
	    public InitParams()
	    {
	    }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="flags">InitFlags</param>
        /// <param name="requestedMinorVersion">uint</param>
        public InitParams(InitFlags flags, uint requestedMinorVersion)
	    {
	        Flags = flags;
	        RequestedMinorVersion = requestedMinorVersion;
	    }
	}
}
