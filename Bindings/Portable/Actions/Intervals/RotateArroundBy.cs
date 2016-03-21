using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urho.Actions
{
	public class RotateArroundBy : FiniteTimeAction
	{
		#region Constructors

		public RotateArroundBy(float duration, Vector3 point, float deltaX, float deltaY, float deltaZ) : base(duration)
		{
			Point = point;
			DeltaX = deltaX;
			DeltaY = deltaY;
			DeltaZ = deltaZ;
		}

		#endregion Constructors

		public Vector3 Point { get; private set; }
		public float DeltaX { get; private set; }
		public float DeltaY { get; private set; }
		public float DeltaZ { get; private set; }

		protected internal override ActionState StartAction(Node target)
		{
			return new RotateArroundByState(this, target);
		}

		public override FiniteTimeAction Reverse()
		{
			return new RotateArroundBy(Duration, Point, -DeltaX, -DeltaY, -DeltaZ);
		}
	}

	public class RotateArroundByState : FiniteTimeActionState
	{
		protected Vector3 Point;
		protected float DeltaX;
		protected float DeltaY;
		protected float DeltaZ;
		float prevTime;

		public RotateArroundByState(RotateArroundBy action, Node target)
			: base(action, target)
		{
			Point = action.Point;
			DeltaX = action.DeltaX;
			DeltaY = action.DeltaY;
			DeltaZ = action.DeltaZ;
		}

		public override void Update(float time)
		{
			if (Target == null)
				return;

			var timeDelta = time - prevTime;
			Target.RotateAround(Point, new Quaternion(timeDelta * DeltaX, timeDelta * DeltaY, timeDelta * DeltaZ), TransformSpace.World);
			prevTime = time;
		}
	}
}
