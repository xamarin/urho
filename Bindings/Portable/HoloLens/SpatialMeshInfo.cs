using System;

namespace Urho.SharpReality
{
	public struct SpatialMeshInfo
	{
		public string SurfaceId { get; set; }
		public DateTimeOffset Date { get; set; }
		public SpatialVertex[] VertexData { get; set; }
		public short[] IndexData { get; set; }
		public Vector3 BoundsCenter { get; set; }
		public Quaternion BoundsRotation { get; set; }
		public Vector3 Extents { get; set; }
		public Matrix4 Transform { get; set; }
	}
}