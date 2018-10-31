using System;
using System.Runtime.InteropServices;

namespace Urho
{
	internal static class Consts
	{
#if __IOS__
		public const string NativeImport = "@rpath/Urho.framework/Urho";
#elif __UWP__
		public const string NativeImport = "mono-holourho";
#else
		public const string NativeImport = "mono-urho";
#endif
	}
}
