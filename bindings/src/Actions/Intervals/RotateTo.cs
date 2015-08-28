using System;

namespace Urho
{
	public class RotateTo : FiniteTimeAction
	{
		public float DistanceAngleX { get; private set; }
		public float DistanceAngleY { get; private set; }

		#region Constructors

		public RotateTo (float duration, float deltaAngleX, float deltaAngleY) : base (duration)
		{
			DistanceAngleX = deltaAngleX;
			DistanceAngleY = deltaAngleY;
		}

		public RotateTo (float duration, float deltaAngle) : this (duration, deltaAngle, deltaAngle)
		{
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new RotateToState (this, target);
		}

		public override FiniteTimeAction Reverse()
		{
			throw new NotImplementedException();
		}
	}


	public class RotateToState : FiniteTimeActionState
	{
		protected float DiffAngleY;
		protected float DiffAngleX;

		protected float DistanceAngleX { get; set; }

		protected float DistanceAngleY { get; set; }

		protected float StartAngleX;
		protected float StartAngleY;

		public RotateToState (RotateTo action, Node target)
			: base (action, target)
		{ 
			DistanceAngleX = action.DistanceAngleX;
			DistanceAngleY = action.DistanceAngleY;

			var sourceRotation = Target.Rotation;

			// Calculate X
			StartAngleX = sourceRotation.X;
			if (StartAngleX > 0)
			{
				StartAngleX = StartAngleX % 360.0f;
			}
			else
			{
				StartAngleX = StartAngleX % -360.0f;
			}

			DiffAngleX = DistanceAngleX - StartAngleX;
			if (DiffAngleX > 180)
			{
				DiffAngleX -= 360;
			}
			if (DiffAngleX < -180)
			{
				DiffAngleX += 360;
			}

			//Calculate Y: It's duplicated from calculating X since the rotation wrap should be the same
			StartAngleY = sourceRotation.Y;

			if (StartAngleY > 0)
			{
				StartAngleY = StartAngleY % 360.0f;
			}
			else
			{
				StartAngleY = StartAngleY % -360.0f;
			}

			DiffAngleY = DistanceAngleY - StartAngleY;
			if (DiffAngleY > 180)
			{
				DiffAngleY -= 360;
			}

			if (DiffAngleY < -180)
			{
				DiffAngleY += 360;
			}
		}

		public override void Update (float time)
		{
			Target?.Rotate(new Quaternion(StartAngleX + DiffAngleX * time, StartAngleY + DiffAngleY * time, 0 /*TODO:!!!*/), TransformSpace.Local);
		}
	}
}