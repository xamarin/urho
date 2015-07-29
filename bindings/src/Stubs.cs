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

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct WorkItem
    {
        [DllImport("mono-urho")]
        internal static extern IntPtr WorkItem_WorkItem();

        public WorkItem(IntPtr p)
        {
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct RefCount
    {
        [DllImport("mono-urho")]
        internal static extern IntPtr RefCount_RefCount();
    }
}
