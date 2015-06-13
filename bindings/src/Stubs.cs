using System;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;

namespace Urho {
	[StructLayout (LayoutKind.Sequential)]
	public partial struct ProfilerBlock {
		IntPtr block;
	}

	public partial class Network {
	}

	public interface ISerializer {
		IntPtr Handle { get; }
	}

	public interface IDeserializer {
		IntPtr Handle { get; }
	}
}
