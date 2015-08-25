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
		internal static extern bool Scene_LoadXML(IntPtr handle, string file);

		public bool LoadXml(string file)
		{
			return Scene_LoadXML(handle, file);
		}

		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Scene_SaveXML(IntPtr handle, string file, string indentation);

		public bool SaveXml(string file, string indentation = "\t")
		{
			return Scene_SaveXML(handle, file, indentation);
		}
	}
}
