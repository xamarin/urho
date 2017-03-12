using System;
using System.Runtime.InteropServices;

namespace Urho
{
	partial class Frustum : IDisposable
	{
		IntPtr handle;

		public IntPtr Handle => handle;

		public Frustum(IntPtr handle)
		{
			this.handle = handle;
		}

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal extern static IntPtr Frustum_GetVertices(IntPtr target, out int count);


		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		internal extern static IntPtr Frustum_GetPlanes(IntPtr target, out int count);


		public Vector3[] Vertices
		{
			get
			{
				int count;
				var ptr = Frustum_GetVertices(handle, out count);
				var array = ptr.ToStructsArray<Vector3>(count);
				return array;
			}
		}

		public Plane[] Planes
		{
			get
			{
				int count;
				var ptr = Frustum_GetPlanes(handle, out count);
				return ptr.ToStructsArray<Plane>(count);
			}
		}

		public void Dispose()
		{
		}
	}
}
