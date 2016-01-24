using System;
using System.Runtime.InteropServices;

namespace Urho {
	public class PodVector<T> where T : UrhoObject {
		IntPtr handle;
		
		public PodVector ()
		{
#if false
			if (typeof (T) == typeof (Node))
				handle = PodVectorHelper.PODVector__NodePtr_new ();
			else
				throw new NotImplementedException ("There is no support for PodVectors of the specified type");
#endif
		}
	}
}
