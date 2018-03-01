using System;
using System.Runtime.InteropServices;

namespace Urho {

	// Skeletons are typically references to internal storage in other objects
	// we surface this to C# as a pointer to the data, but to ensure that we do not
	// have dangling pointers, we only surface the constructor that retains a copy
	// to the container
	
	public partial class Skeleton 
	{
		IntPtr handle;

		[Preserve]
		public Skeleton (IntPtr handle, object container)
		{
			this.handle = handle;
		}

		public BoneWrapper GetBoneSafe(uint index)
		{
			Runtime.ValidateObject(this);
			unsafe
			{
				Bone* result = Skeleton_GetBone(handle, index);
				if (result == null)
					return null;
				return new BoneWrapper(this, result);
			}
		}

		public BoneWrapper GetBoneSafe(StringHash nameHash)
		{
			Runtime.ValidateObject(this);
			unsafe
			{
				Bone* result = Skeleton_GetBone0(handle, nameHash.Code);
				if (result == null)
					return null;
				return new BoneWrapper(this, result);
			}
		}

		public BoneWrapper GetBoneSafe(string name) => GetBoneSafe(new StringHash(name));
	}

	public partial class AnimatedModel {
		
		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		extern static IntPtr AnimatedModel_GetSkeleton (IntPtr handle);
		
		public Skeleton Skeleton {
			get
			{
				Runtime.ValidateObject(this);
				return new Skeleton (AnimatedModel_GetSkeleton (handle), this);
			}
		}
	}
	
}
