using System;
using System.Runtime.InteropServices;

namespace Urho.Network {
	public partial class Connection {
		public void SendMessage (int msgId, bool reliable, bool inOrder, byte [] buffer, uint contentId = 0)
		{
			unsafe {
				fixed (byte *p = &buffer[0])
					Connection_SendMessage (handle, msgId, reliable, inOrder, p, (uint) buffer.Length, contentId);
			}
		}

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		extern static IntPtr Connection_GetControls (IntPtr handle);

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		extern static IntPtr Connection_SetControls (IntPtr handle, IntPtr controlHandle);
		
		public Controls Controls {
			get {
				return new Controls (this, Connection_GetControls (handle));
			}

			set {
				Connection_SetControls (handle, value.handle);
			}
		}
	}
}
