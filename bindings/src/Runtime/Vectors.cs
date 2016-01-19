using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Urho
{
	internal static class Vectors {
		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		internal extern static int VectorSharedPtr_Count (IntPtr h);

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		internal extern static IntPtr VectorSharedPtr_GetIdx (IntPtr h, int idx);

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		internal extern static void VectorSharedPtr_SetIdx (IntPtr h, int idx, IntPtr v);

		internal class ProxyUrhoObject<T> : ProxyRefCounted<T>, IReadOnlyList<T> where T : UrhoObject
		{
			public ProxyUrhoObject(IntPtr handle) : base(handle) { }

			public override T this[int idx] => Runtime.LookupObject<T>(VectorSharedPtr_GetIdx(handle, idx));

			IEnumerator IEnumerable.GetEnumerator() => new ProxyUrhoObjectEnumerator<UrhoObject>(handle);

			public override IEnumerator<T> GetEnumerator() => new ProxyUrhoObjectEnumerator<T>(handle);

			class ProxyUrhoObjectEnumerator<U> : IEnumerator, IEnumerator<U> where U : UrhoObject
			{
				readonly IntPtr handle;
				int index;
				U current;

				public ProxyUrhoObjectEnumerator(IntPtr handle)
				{
					this.handle = handle;
				}

				public bool MoveNext()
				{
					var count = VectorSharedPtr_Count(handle);
					if (count < 1 || count <= index)
						return false;

					current = Runtime.LookupObject<U>(VectorSharedPtr_GetIdx(handle, index));
					index++;
					return true;
				}

				public void Reset()
				{
					index = 0;
					current = null;
				}

				U IEnumerator<U>.Current => current;

				public object Current => current;

				public void Dispose()
				{
					Reset();
				}
			}
		}

		internal class ProxyRefCounted<T> : IReadOnlyList<T> where T : RefCounted
		{
			protected IntPtr handle;
			public ProxyRefCounted(IntPtr handle)
			{
				this.handle = handle;
			}
		
			public virtual T this [int idx] => Runtime.LookupRefCounted<T> (VectorSharedPtr_GetIdx (handle, idx));

			public int Count => VectorSharedPtr_Count (handle);
			
			IEnumerator IEnumerable.GetEnumerator () => new ProxyRefCountedEnumerator<RefCounted>(handle);

			public virtual IEnumerator<T> GetEnumerator () => new ProxyRefCountedEnumerator<T>(handle);

			class ProxyRefCountedEnumerator<U> : IEnumerator, IEnumerator<U> where U : RefCounted
			{
				readonly IntPtr handle;
				int index;
				U current;

				public ProxyRefCountedEnumerator(IntPtr handle)
				{
					this.handle = handle;
				}

				public bool MoveNext()
				{
					var count = VectorSharedPtr_Count(handle);
					if (count < 1 || count <= index)
						return false;

					current = Runtime.LookupRefCounted<U>(VectorSharedPtr_GetIdx(handle, index));
					index++;
					return true;
				}

				public void Reset()
				{
					index = 0;
					current = null;
				}

				U IEnumerator<U>.Current => current;

				public object Current => current;

				public void Dispose()
				{
					Reset();
				}
			}
		}
	}
}