using System;
using System.Runtime.InteropServices;

namespace Urho
{
	internal static class Consts
	{
#if WINDOWS_D3D
		public const string NativeImport = "mono-urho-d3d";
#elif IOS
		public const string NativeImport = "@rpath/Urho.framework/Urho";
#elif UWP_HOLO
		public const string NativeImport = "mono-holourho";
#else
		public const string NativeImport = "mono-urho";
#endif
	}
}

