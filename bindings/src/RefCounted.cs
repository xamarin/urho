//
// TODO: optimization, remove the static initializer, so we do not need to add code
// at JIT/AOT time to probe for static class initialization
//
using System;
using System.Collections.Generic;
using System.Threading;

namespace Urho {
	public partial class RefCounted : IDisposable {
		// TODO: replace this with an init with some compare and exchange.
		static List<IntPtr> deadHandles = new List<IntPtr> ();
		internal IntPtr handle;
		static Thread MainThread;
		
		public IntPtr Handle => handle;

		internal RefCounted (UrhoObjectFlag empty)
		{
		}
		
		public RefCounted (IntPtr handle)
		{
			this.handle = handle;
		}
		
		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		protected virtual void Dispose (bool disposing)
		{
			if (handle != IntPtr.Zero){
				if (Thread.CurrentThread == MainThread)
					RefCounted.RefCounted_ReleaseRef (handle);
				else
					lock (deadHandles)
						deadHandles.Add (handle);
				handle = IntPtr.Zero;
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

		public override bool Equals (object other)
		{
			if (other == null)
				return false;
			if (other.GetType () != GetType ())
				return false;
			var or = other as RefCounted;
			if (or.handle == handle)
				return true;
			return false;
		}

		public static bool operator ==(RefCounted a, RefCounted b)
		{
			if (a == null){
				if (b == null)
					return true;
				return false;
			} else {
				if (b == null)
					return false;
				return a.handle == b.handle;
			}
		}
		
		public static bool operator !=(RefCounted a, RefCounted b)
		{
			if (a == null)
				return b != null;
			else {
				if (b == null)
					return true;
				return a.handle != b.handle;
			}
		}

		public override int GetHashCode ()
		{
			return (int) handle;
		}
		
		~RefCounted ()
		{
			Dispose (false);
		}
	}
}
