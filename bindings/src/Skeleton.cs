using System;
using System.Runtime.InteropServices;

namespace Urho {

	// Skeletons are typically references to internal storage in other objects
	// we surface this to C# as a pointer to the data, but to ensure that we do not
	// have dangling pointers, we only surface the constructor that retains a copy
	// to the container
	
	public partial class Skeleton {
		IntPtr handle;
		
		public Skeleton (IntPtr handle, object container)
		{
			this.handle = handle;
		}

		public BoneWrapper GetBoneSafe(uint index)
		{
			unsafe
			{
				Bone* result = Skeleton_GetBone(handle, index);
				if (result == null)
					return null;
				return new BoneWrapper(this, result);
			}
		}

		public BoneWrapper GetBoneSafe(String name)
		{
			unsafe
			{
				Bone* result = Skeleton_GetBone0(handle, new StringHash(name).Code);
				if (result == null)
					return null;
				return new BoneWrapper(this, result);
			}
		}
	}

	public partial class AnimatedModel {
		
		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		extern static IntPtr AnimatedModel_GetSkeleton (IntPtr handle);
		
		public Skeleton Skeleton {
			get {
				return new Skeleton (AnimatedModel_GetSkeleton (handle), this);
			}
		}
	}
	
}
