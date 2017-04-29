using System;

namespace Urho.SharpReality
{
	public struct SpatialVertex
	{
		public float PositionX;
		public float PositionY;
		public float PositionZ;
		public float NormalX;
		public float NormalY;
		public float NormalZ;
		public uint ColorUint;

		public Color Color
		{
			get { throw new NotImplementedException(); }
			set { ColorUint = value.ToUInt(); }
		}

		public Vector3 Position
		{
			get { return new Vector3(PositionX, PositionY, PositionZ); }
			set
			{
				PositionX = value.X;
				PositionY = value.Y;
				PositionZ = value.Z;
			}
		}

		public Vector3 Normal
		{
			get { return new Vector3(NormalX, NormalY, NormalZ); }
			set
			{
				NormalX = value.X;
				NormalY = value.Y;
				NormalZ = value.Z;
			}
		}
	}
}