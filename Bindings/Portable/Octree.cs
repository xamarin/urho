using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Urho
{
	partial class Octree
	{
		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr Octree_Raycast(IntPtr handle, ref Ray ray, ref RayQueryLevel level, float maxDistance, uint drawableFlags, uint viewMask, bool single, out int count);

		List<RayQueryResult> Raycast(Ray ray, RayQueryLevel level, float maxDistance, DrawableFlags drawableFlags, bool single, uint viewMask = UInt32.MaxValue)
		{
			Runtime.ValidateRefCounted(this);
			List<RayQueryResult> result = new List<RayQueryResult>();

			int count;
			var ptr = Octree_Raycast(Handle, ref ray, ref level, maxDistance, (uint)drawableFlags, viewMask, single, out count);

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

		public List<RayQueryResult> Raycast(Ray ray, RayQueryLevel level = RayQueryLevel.Triangle, float maxDistance = 10000, DrawableFlags drawableFlags = DrawableFlags.Geometry, uint viewMask = UInt32.MaxValue)
		{
			Runtime.ValidateRefCounted(this);
			return Raycast(ray, level, maxDistance, drawableFlags, false, viewMask);
		}

		public RayQueryResult? RaycastSingle(Ray ray, RayQueryLevel level = RayQueryLevel.Triangle, float maxDistance = 10000, DrawableFlags drawableFlags = DrawableFlags.Geometry, uint viewMask = UInt32.MaxValue)
		{
			Runtime.ValidateRefCounted(this);
			var result = Raycast(ray, level, maxDistance, drawableFlags, true, viewMask);
			if (result != null && result.Count > 0)
				return result[0];
			return null;
		}
	}
}
