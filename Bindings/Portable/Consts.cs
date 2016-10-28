using System;
using System.Runtime.InteropServices;

namespace Urho
{
	internal static class Consts
	{
#if IOS
		public const string NativeImport = "@rpath/Urho.framework/Urho";
#elif UWP_HOLO
		public const string NativeImport = "mono-holourho";
#elif WINDOWS_D3D
		public const string NativeImport = "mono-urho-d3d";
#else
		public const string NativeImport = "mono-urho";
#endif
	}
}

