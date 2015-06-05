//
// TODO: optimization, remove the static initializer, so we do not need to add code
// at JIT/AOT time to probe for static class initialization
//
using System;
using System.Collections.Generic;
using System.Threading;

namespace Urho {
	public class RefCounted : IDisposable {
		// TODO: replace this with an init with some compare and exchange.
		static List<IntPtr> deadHandles = new List<IntPtr> ();
		IntPtr handle;
		
		public IntPtr Handle => handle;

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		protected virtual void Dispose (bool disposing)
		{
			if (handle != IntPtr.Zero){
				if (Thread.CurrentThread == MainThread)
					ReleaseRef ();
				else
					lock (deadHandles)
						deadHandles.Add (handle);
				handle = IntPtr.Zero
			}
		}

		public void FlushHandles ()
		{
			lock (deadHandles){
				foreach (var p in deadHandles)
					RefCounted.RefCounted_ReleaseRef (p);
				deadHandles.Clear ();
			}
		}

		~UrhoBase ()
		{
			Dispose (false);
		}
	}
}
