using System;
using System.Runtime.InteropServices;

namespace Urho
{
	internal class NativeToManagedStringMarshaler : ICustomMarshaler
	{
		private static readonly NativeToManagedStringMarshaler StaticInstance = new NativeToManagedStringMarshaler();

		public void CleanUpManagedData(object managedObj) {}

		public virtual void CleanUpNativeData(IntPtr pNativeData) {}

		public int GetNativeDataSize() => -1;

		public virtual IntPtr MarshalManagedToNative(Object managedObj)
		{
			throw new NotSupportedException();
		}

		public virtual object MarshalNativeToManaged(IntPtr pNativeData)
		{
			if (pNativeData == IntPtr.Zero)
				return null;

			return Marshal.PtrToStringAnsi(pNativeData);
		}
		
		public new static ICustomMarshaler GetInstance(string cookie) => StaticInstance;
	}
}
