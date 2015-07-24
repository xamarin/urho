using System;
using System.Runtime.InteropServices;

namespace Urho {
	internal class PodVectorHelper {
		[DllImport ("mono-urho")]
		internal extern static IntPtr Create_PODVector__NodePtr ();

		[DllImport ("mono-urho")]
		internal extern static void Destroy_PODVector__NodePtr (IntPtr value);
	}
	
	public class PodVector<T> where T : UrhoObject {
		IntPtr handle;
		
		public PodVector ()
		{
			if (typeof (T) == typeof (Node))
				handle = PodVectorHelper.Create_PODVector__NodePtr ();
		}
	}
}
