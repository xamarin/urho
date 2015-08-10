using System;
using System.Runtime.InteropServices;

namespace Urho
{
	partial class NavigationMesh
	{
		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		internal extern static IntPtr urho_navigationmesh_findpath(IntPtr navMesh, Vector3 start, Vector3 end, out int count);

		public Vector3[] FindPath(Vector3 start, Vector3 end)
		{
			int count;
			var ptr = urho_navigationmesh_findpath(Handle, start, end, out count);
			if (ptr == IntPtr.Zero)
				return null;
			
			var res = new Vector3[count];
			for (int i = 0; i < count; i++)
			{
				var vectorPtr = Marshal.ReadIntPtr(ptr, i * IntPtr.Size);
				var vector = (Vector3)Marshal.PtrToStructure(vectorPtr, typeof (Vector3));
				res[i] = vector;
			}
			return res;
		}
    }
}
