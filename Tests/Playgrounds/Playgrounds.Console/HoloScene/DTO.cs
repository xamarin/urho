using System.Runtime.InteropServices;

namespace Playgrounds.Console.HoloScene
{
	[StructLayout(LayoutKind.Sequential)]
	public struct SpatialVertexDto
	{
		public Vector3Dto Position { get; set; }
		public Vector3Dto Normal { get; set; }
		public uint Color { get; set; }
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Vector3Dto
	{
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Vector4Dto
	{
		public float W { get; set; }
		public Vector3Dto XYZ { get; set; }
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Matrix4Dto
	{
		public Vector4Dto Row0 { get; set; }
		public Vector4Dto Row1 { get; set; }
		public Vector4Dto Row2 { get; set; }
		public Vector4Dto Row3 { get; set; }
	}

	public class SurfaceDto
	{
		public string Id { get; set; }
		public SpatialVertexDto[] VertexData { get; set; }
		public short[] IndexData { get; set; }
		public Vector3Dto BoundsCenter { get; set; }
		public Vector4Dto BoundsOrientation { get; set; }
		//public Vector3Dto BoundsExtents { get; set; }
		//public Matrix4Dto Transform { get; set; }
	}
}
