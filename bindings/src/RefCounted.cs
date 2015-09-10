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
		//static Thread MainThread;
		
		public IntPtr Handle => handle;

		internal RefCounted (UrhoObjectFlag empty)
		{
		}
		
		public RefCounted (IntPtr handle)
		{
			if (handle == IntPtr.Zero)
				throw new ArgumentException ($"Attempted to instantiate a {GetType()} with a null handle");
			this.handle = handle;
			Runtime.RegisterObject (this);
		}
		
		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		protected virtual void Dispose (bool disposing)
		{
			if (handle != IntPtr.Zero){
				//if (Thread.CurrentThread == MainThread) TODO:
				//	RefCounted.RefCounted_ReleaseRef (handle);
				//else
				lock (deadHandles)
						deadHandles.Add (handle);
				handle = IntPtr.Zero;
			}
		}

		public void FlushHandles ()
		{
			lock (deadHandles){
				foreach (var p in deadHandles){
					Runtime.UnregisterObject (p);
					RefCounted.RefCounted_ReleaseRef (p);
				}
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

		public static bool operator ==(RefCounted _a, RefCounted _b)
		{
			object a = _a;
			object b = _b;
			if (a == null){
				if (b == null)
					return true;
				return false;
			} else {
				if (b == null)
					return false;
				return _a.handle == _b.handle;
			}
		}
		
		public static bool operator !=(RefCounted _a, RefCounted _b)
		{
			object a = _a;
			object b = _b;
			if (a == null)
				return b != null;
			else {
				if (b == null)
					return true;
				return _a.handle != _b.handle;
			}
		}

		public override int GetHashCode ()
		{
			if (IntPtr.Size == 8) //means 64bit
				return unchecked ((int) (long) handle);
			return (int) handle;
		}

		~RefCounted ()
		{
			Dispose (false);
		}
    }
}
