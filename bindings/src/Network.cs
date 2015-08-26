using System;
using System.Runtime.InteropServices;

namespace Urho {
	public partial class Network {
		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		extern static int Network_Connect (IntPtr handle, string address, short port, IntPtr scene);

		public bool Connect (string address, short port, Scene scene)
		{
			if (address == null)
				throw new ArgumentNullException ("address");
			return Network_Connect (handle, address, port, scene?.Handle ?? IntPtr.Zero) != 0;
		}
	}
	
}
