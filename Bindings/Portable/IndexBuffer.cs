using System;

namespace Urho
{
	public partial class IndexBuffer
	{
		public unsafe void SetData (short [] vertexData)
		{
			if (vertexData == null)
				throw new ArgumentException(nameof(vertexData));

			if (vertexData.Length == 0)
				SetData((void*)IntPtr.Zero);

			Runtime.ValidateRefCounted(this);
			fixed (short *ptr = &vertexData [0]) {
				SetData (ptr);
			}
		}

		public unsafe void SetData (uint [] vertexData)
		{
			if (vertexData == null)
				throw new ArgumentException(nameof(vertexData));

			if (vertexData.Length == 0)
				SetData((void*)IntPtr.Zero);

			Runtime.ValidateRefCounted(this);
			fixed (uint* ptr = &vertexData [0]) {
				SetData (ptr);
			}
		}
	}
}
