using System;
using System.Runtime.InteropServices;

namespace Urho
{
	internal static class Consts
	{
#if IOS
		public const string NativeImport = "@rpath/Urho.framework/Urho";
#else
		public const string NativeImport = "mono-urho";
#endif
	}
}

