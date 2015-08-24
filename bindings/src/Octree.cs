using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Urho
{
	partial class Octree
	{
		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr Octree_RaycastSingle(IntPtr handle, ref Ray ray, ref RayQueryLevel level, float maxDistance, uint drawableFlags, uint viewMask, out int count);

		public List<RayQueryResult> RaycastSingle(Ray ray, RayQueryLevel level, float maxDistance, DrawableFlags drawableFlags, uint viewMask = UInt32.MaxValue)
		{
			List<RayQueryResult> result = new List<RayQueryResult>();

			int count;
			var ptr = Octree_RaycastSingle(Handle, ref ray, ref level, maxDistance, (uint)drawableFlags, viewMask, out count);

			if (ptr == IntPtr.Zero)
				return result;

			int structSize = Marshal.SizeOf(typeof (RayQueryResult));
			for (int i = 0; i < count; i++)
			{
				IntPtr data = new IntPtr(ptr.ToInt64() + structSize * i);
				RayQueryResult item = (RayQueryResult)Marshal.PtrToStructure(data, typeof(RayQueryResult));
				result.Add(item);
			}

			return result;
		} 
	}
}
