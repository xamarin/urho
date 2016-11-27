//
// Helpers to surface a simpler API for collisions
//
using System;
using System.Runtime.InteropServices;

namespace Urho.Physics {
	
	public unsafe struct CollisionData  {
		public Vector3 ContactPosition, ContactNormal;
		public float ContactDistance, ContactImpulse;

		public CollisionData (byte *p)
		{
			ContactPosition = *(Vector3 *)p;
			p += 12;
			ContactNormal = *(Vector3 *) p;
			p += 12;
			ContactDistance = *(float *) p;
			p += 4;
			ContactImpulse = *(float *) p;
		}
		
		public override string ToString ()
		{
			return $"[CollisionData: Position={ContactPosition}, Normal={ContactNormal}, Distance={ContactDistance}, Impuse={ContactImpulse}";
		}

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		extern static int MemoryStream_Size (IntPtr data);

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		extern static int MemoryStream_GetData (IntPtr data);
		
		internal static CollisionData [] FromContactData (IntPtr data, int size)
		{
			if (data == IntPtr.Zero || size < 1)
				return new CollisionData[0];

			const int encodedSize = 4 * 3+3+1+1;
			var n = size / encodedSize;
			byte *ptr = (byte *) data;
			var ret = new CollisionData [n];
			
			for (int i = 0; i < n; i++){
				ret [i] = new CollisionData (ptr);
				ptr += encodedSize;
			}
			return ret;
		}
	}

}
