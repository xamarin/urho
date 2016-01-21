using System;
using System.Runtime.InteropServices;

namespace Urho
{
	partial class UrhoConsole
	{
		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Console_OpenConsoleWindow();
		
		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr Console_GetConsoleInput();

		public static void OpenConsoleWindow()
		{
			Console_OpenConsoleWindow();
		}

		public static string GetConsoleInput()
		{
			return Marshal.PtrToStringAnsi(Console_GetConsoleInput());
		}
	}
}
