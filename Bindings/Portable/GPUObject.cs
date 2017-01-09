using System;
namespace Urho
{
	partial class GPUObject
	{
		IntPtr handle;

		public GPUObject(IntPtr handle)
		{
			this.handle = handle;
		}
	}

	public interface IGPUObject
	{
		GPUObject AsGPUObject();
	}
}
