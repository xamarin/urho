using System;
using System.Runtime.InteropServices;

namespace Urho
{
	partial class Scene
	{
		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Scene_LoadXMLFromCache(IntPtr handle, IntPtr cache, string file);

		public bool LoadXmlFromCache(ResourceCache cache, string file)
		{
			return Scene_LoadXMLFromCache(handle, cache.Handle, file);
		}

		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Scene_LoadXMLFile(IntPtr handle, string file);

		public bool LoadXml(string file)
		{
			var result = Scene_LoadXMLFile(handle, file);
			if (result)
			{
				// LoadXml will mark a lot of objects for collection
				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect();
			}
			return result;
		}

		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Scene_SaveXMLFile(IntPtr handle, string file, string indentation);

		public bool SaveXml(string file, string indentation = "\t")
		{
			return Scene_SaveXMLFile(handle, file, indentation);
		}
	}
}
