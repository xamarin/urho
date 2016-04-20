using System;
using System.IO;
using System.Reflection;
using Windows.Storage;

namespace Urho.UWP
{
	public static class UwpUrhoInitializer
	{
		internal static void OnInited()
		{
			var folder = ApplicationData.Current.LocalFolder.Path;
		}
	}
}
