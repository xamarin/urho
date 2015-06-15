using System;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;

namespace Urho {
	[StructLayout (LayoutKind.Sequential)]
	public partial struct ProfilerBlock {
		IntPtr block;
	}

	public enum UrhoObjectFlag {
		Empty
	}
		
	public interface ISerializer {
		IntPtr Handle { get; }
	}

	public interface IDeserializer {
		IntPtr Handle { get; }
	}
}
