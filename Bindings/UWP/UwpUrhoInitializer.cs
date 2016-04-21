using System;
using System.IO;
using System.Runtime.InteropServices;
using Windows.Storage;

namespace Urho.UWP
{
	public static class UwpUrhoInitializer
	{
		internal static void OnInited()
		{
			var folder = ApplicationData.Current.LocalFolder.Path;
			if (IntPtr.Size == 8)
			{
				throw new NotSupportedException("x86_64 is not supported yet. Please use x86.");
			}
		}
	}
}
