using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urho.Actions
{
	public class RotateAroundBy : FiniteTimeAction
	{
		public Vector3 Point { get; set; }
		public float DeltaX { get; set; }
		public float DeltaY { get; set; }
		public float DeltaZ { get; set; }
		public TransformSpace TransformSpace { get; set; }

		#region Constructors

		public RotateAroundBy(float duration, Vector3 point, float deltaX, float deltaY, float deltaZ, TransformSpace ts = TransformSpace.World) : base(duration)
		{
			Point = point;
			DeltaX = deltaX;
			DeltaY = deltaY;
			DeltaZ = deltaZ;
			TransformSpace = ts;
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new RotateAroundByState(this, target);
		}

		public override FiniteTimeAction Reverse()
		{
			return new RotateAroundBy(Duration, Point, -DeltaX, -DeltaY, -DeltaZ);
		}
	}

	public class RotateAroundByState : FiniteTimeActionState
	{
		protected Vector3 Point;
		protected float DeltaX;
		protected float DeltaY;
		protected float DeltaZ;
		protected TransformSpace TransformSpace;

		float prevTime;

		public RotateAroundByState(RotateAroundBy action, Node target)
			: base(action, target)
		{
			Point = action.Point;
			DeltaX = action.DeltaX;
			DeltaY = action.DeltaY;
			DeltaZ = action.DeltaZ;
			TransformSpace = action.TransformSpace;
		}


		public override void Update(float time)
		{
			if (Target == null)
				return;

			var timeDelta = time - prevTime;
			Target.RotateAround(Point, new Quaternion(timeDelta * DeltaX, timeDelta * DeltaY, timeDelta * DeltaZ), TransformSpace);
			prevTime = time;
		}
	}
}
