using System;
using System.Runtime.InteropServices;

namespace Urho
{
	partial class Scene
	{
		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Scene_LoadXML(IntPtr handle, string file);

		public bool LoadXML(string file)
		{
			return Scene_LoadXML(handle, file);
		}

		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		internal static extern bool Scene_SaveXML(IntPtr handle, string file, string indentation);

		public bool SaveXML(string file, string indentation)
		{
			return Scene_SaveXML(handle, file, indentation);
		}
	}
}
