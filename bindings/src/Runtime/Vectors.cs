using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Urho
{
	internal static class Vectors {
		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		internal extern static int VectorSharedPtr_Count (IntPtr h);

		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		internal extern static IntPtr VectorSharedPtr_GetIdx (IntPtr h, int idx);

		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		internal extern static void VectorSharedPtr_SetIdx (IntPtr h, int idx, IntPtr v);

		internal class ProxyUrhoObject<T> : ProxyRefCounted<T> where T : UrhoObject
		{
			public ProxyUrhoObject(IntPtr handle) : base(handle) { }

			public override T this[int idx]
			{
				get {
					return Runtime.LookupRefCounted<T>(VectorSharedPtr_GetIdx(handle, idx));
				}
				set { throw new NotImplementedException("Proxy has not implemented this yet"); }
			}
		}

		internal class ProxyRefCounted<T> : IList<T> where T : RefCounted
		{
			protected IntPtr handle;
			public ProxyRefCounted(IntPtr handle)
			{
				this.handle = handle;
			}
		
			public virtual T this [int idx] {
				get {
					return Runtime.LookupRefCounted<T> (VectorSharedPtr_GetIdx (handle, idx));
				}
				set {
					// Mhm, how do we get the SharedPtr from an existing object?
					throw new NotImplementedException ("Proxy has not implemented this yet");
				}
			}
				
			public int IndexOf (T v)
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");
			}
			
			public void Insert (int idx, T v)
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");
			}
			
			public void RemoveAt (int idx)
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");
			}
			
			public bool Remove (T val)
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");
			}
			
			public void CopyTo (T [] target, int p)
			{
				int c = Count;
				for (int i = 0; i < c; i++)
					target [i+p] = this[i];
			}
			
			public int Count {
				get {
					return VectorSharedPtr_Count (handle);
				}
			}
			
			public bool IsReadOnly => false;
			
			public void Add (T v)
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");			
			}
			
			public void Clear ()
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");			
			}
			
			public bool Contains (T v)
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");			
			}
			
			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");			
			}
			
			public IEnumerator<T> GetEnumerator ()
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");			
			}
		}
	}
}