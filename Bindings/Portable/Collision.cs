//
// Helpers to surface a simpler API for collisions
//
using System;
using System.Runtime.InteropServices;

namespace Urho.Physics {
	
	public unsafe struct CollisionData
	{
		public Vector3 ContactPosition, ContactNormal;
		public float ContactDistance, ContactImpulse;
		
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
			return data.ToStructsArray<CollisionData>(size);
		}
	}
}