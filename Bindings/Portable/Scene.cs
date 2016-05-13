using System;
using System.Runtime.InteropServices;
using Urho.IO;
using Urho.Resources;

namespace Urho
{
	partial class Scene
	{
		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Scene_LoadXMLFromCache(IntPtr handle, IntPtr cache, string file);

		public bool LoadXmlFromCache(ResourceCache cache, string file)
		{
			Runtime.ValidateRefCounted(this);
			return Scene_LoadXMLFromCache(handle, cache.Handle, file);
		}

		public bool LoadXml(string path)
		{
			Runtime.ValidateRefCounted(this);
			using (var file = new File(Context, path, FileMode.Read))
			{
				return LoadXml(file);
			}
		}

		public bool SaveXml(string path, string indentation = "\t")
		{
			Runtime.ValidateRefCounted(this);
			using (var file = new File(Context, path, FileMode.Write))
			{
				return SaveXml(file, indentation);
			}
		}
	}
}
