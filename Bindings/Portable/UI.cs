using System;
using System.Runtime.InteropServices;
using Urho.Resources;

namespace Urho.Gui
{
	partial	class UI
	{
		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		extern static void UI_LoadLayoutToElement(IntPtr handle, IntPtr to, IntPtr cache, string name);

		public void LoadLayoutToElement(UIElement container, ResourceCache cache, string name)
		{
			Runtime.ValidateRefCounted(this);
			UI_LoadLayoutToElement(Handle, container.Handle, cache.Handle, name);
		}
	}
}
