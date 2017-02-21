using System.Runtime.InteropServices;

namespace Urho
{	
	public partial class VertexBuffer
	{
		public void SetData(PositionNormal[] data)
		{
			unsafe { fixed (PositionNormal* p = &data[0]) SetData(p); }
		}

		public void SetData(PositionNormalColor[] data)
		{
			unsafe { fixed (PositionNormalColor* p = &data[0]) SetData(p); }
		}

		public void SetData(PositionNormalColorTexcoord[] data)
		{
			unsafe { fixed (PositionNormalColorTexcoord* p = &data[0]) SetData(p); }
		}

		public void SetData(PositionNormalColorTexcoordTangent[] data)
		{
			unsafe { fixed (PositionNormalColorTexcoordTangent* p = &data[0]) SetData(p); }
		}

		public void SetData (float [] data)
		{
			unsafe { fixed (float* p = &data[0]) SetData(p); }
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct PositionNormal
		{
			public Vector3 Position;
			public Vector3 Normal;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct PositionNormalColor
		{
			public Vector3 Position;
			public Vector3 Normal;
			public uint Color;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct PositionNormalColorTexcoord
		{
			public Vector3 Position;
			public Vector3 Normal;
			public uint Color;
			public Vector2 TexCoord;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct PositionNormalColorTexcoordTangent
		{
			public Vector3 Position;
			public Vector3 Normal;
			public uint Color;
			public Vector2 TexCoord;
			public Vector4 Tangent;
		}
	}
}
