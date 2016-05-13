using System;
using System.Runtime.InteropServices;

namespace Urho.Navigation
{
	partial class NavigationMesh
	{
		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal extern static IntPtr urho_navigationmesh_findpath(IntPtr navMesh, Vector3 start, Vector3 end, out int count);

		public Vector3[] FindPath(Vector3 start, Vector3 end)
		{
			Runtime.ValidateRefCounted(this);
			int count;
			var ptr = urho_navigationmesh_findpath(Handle, start, end, out count);
			if (ptr == IntPtr.Zero)
				return new Vector3[0];

			var res = new Vector3[count];

			int structSize = Marshal.SizeOf(typeof(Vector3));
			for (int i = 0; i < count; i++)
			{
				IntPtr data = new IntPtr(ptr.ToInt64() + structSize * i);
				Vector3 item = (Vector3)Marshal.PtrToStructure(data, typeof(Vector3));
				res[i] = item;
			}
			
			return res;
		}

		public unsafe Vector3 FindNearestPoint(Vector3 hitPos, Vector3 vector3)
		{
			Runtime.ValidateRefCounted(this);
			return FindNearestPoint(hitPos, vector3, null, null);
		}
	}
}
