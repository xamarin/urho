using System;

namespace Urho
{
	public static class Consts
	{
#if __IOS__
		public const string NativeImport = "@rpath/Urho.framework/Urho";
#elif URHO_WIN32
		public const string NativeImport = "mono-urho32";
#else
		public const string NativeImport = "mono-urho";
#endif
	}
}

