using System;
using System.Runtime.InteropServices;

namespace Urho {
	public partial class Model {
		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		extern static IntPtr Model_Clone (IntPtr handle);
		
		public Model Clone ()
		{
			return Runtime.LookupObject<Model> (Model_Clone (handle));
		}
	}
}
