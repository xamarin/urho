//
// TODO: optimization, remove the static initializer, so we do not need to add code
// at JIT/AOT time to probe for static class initialization
//
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Urho.IO;

namespace Urho {
	public partial class RefCounted : IDisposable {
		internal IntPtr handle;

		public IntPtr Handle => handle;

		[Preserve]
		internal RefCounted (UrhoObjectFlag empty) { }

		[Preserve]
		protected RefCounted (IntPtr handle)
		{
			if (handle == IntPtr.Zero)
				throw new ArgumentException ($"Attempted to instantiate a {GetType()} with a null handle");
			this.handle = handle;
			Runtime.RegisterObject (this);
		}
		
		public void Dispose ()
		{
			DeleteNativeObject();
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		/// <summary>
		/// Called by RefCounted::~RefCounted - we don't need to check Refs here - just mark it as deleted and remove from cache
		/// </summary>
		internal void HandleNativeDelete()
		{
			LogSharp.Trace($"{GetType().Name}: HandleNativeDelete");
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern void TryDeleteRefCounted(IntPtr handle);

		/// <summary>
		/// Try to delete underlying native object if nobody uses it (Refs==0)
		/// </summary>
		void DeleteNativeObject()
		{
			if (!IsDeleted && AllowNativeDelete)
			{
				LogSharp.Trace($"{GetType().Name}: DeleteNativeObject");
				TryDeleteRefCounted(handle);
			}
		}

		protected virtual void Dispose (bool disposing)
		{
			if (IsDeleted)
				return;
			
			if (disposing)
			{
				IsDeleted = true;
				OnDeleted();
			}
			Runtime.UnregisterObject(handle);
		}
		
		protected void CheckEngine()
		{
			UrhoPlatformInitializer.DefaultInit();
		}

		/// <summary>
		/// True if underlying native object is deleted
		/// </summary>
		public bool IsDeleted { get; private set; }

		protected virtual void OnDeleted() { }

		protected virtual bool AllowNativeDelete => true;

		public override bool Equals (object other)
		{
			if (other == null)
				return false;
			if (other.GetType () != GetType ())
				return false;
			var or = other as RefCounted;
			if (or != null && or.handle == handle)
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
			if (!Runtime.IsClosing && !IsDeleted)
			{
				var ptr = Handle;
				Application.InvokeOnMain(() => TryDeleteRefCounted(ptr));
			}
			Dispose (false);
		}
	}
}
