using System;

using Urho;
namespace Urho.Actions
{
	public class BezierBy : FiniteTimeAction
	{
		public BezierConfig BezierConfig { get; private set; }


		#region Constructors

		public BezierBy (float t, BezierConfig config) : base (t)
		{
			BezierConfig = config;
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new BezierByState (this, target);

		}

		public override FiniteTimeAction Reverse ()
		{
			BezierConfig r;

			r.EndPosition = -BezierConfig.EndPosition;
			r.ControlPoint1 = BezierConfig.ControlPoint2 + -BezierConfig.EndPosition;
			r.ControlPoint2 = BezierConfig.ControlPoint1 + -BezierConfig.EndPosition;

			var action = new BezierBy (Duration, r);
			return action;
		}
	}

	public class BezierByState : FiniteTimeActionState
	{
		protected BezierConfig BezierConfig { get; set; }

		protected Vector3 StartPosition { get; set; }

		protected Vector3 PreviousPosition { get; set; }


		public BezierByState (BezierBy action, Node target)
			: base (action, target)
		{ 
			BezierConfig = action.BezierConfig;
			PreviousPosition = StartPosition = target.Position;
		}

		public override void Update (float time)
		{
			if (Target != null)
			{
				float xa = 0;
				float xb = BezierConfig.ControlPoint1.X;
				float xc = BezierConfig.ControlPoint2.X;
				float xd = BezierConfig.EndPosition.X;

				float ya = 0;
				float yb = BezierConfig.ControlPoint1.Y;
				float yc = BezierConfig.ControlPoint2.Y;
				float yd = BezierConfig.EndPosition.Y;

				float za = 0;
				float zb = BezierConfig.ControlPoint1.Z;
				float zc = BezierConfig.ControlPoint2.Z;
				float zd = BezierConfig.EndPosition.Z;

				float x = SplineMath.CubicBezier (xa, xb, xc, xd, time);
				float y = SplineMath.CubicBezier (ya, yb, yc, yd, time);
				float z = SplineMath.CubicBezier (za, zb, zc, zd, time);

				Vector3 currentPos = Target.Position;
				Vector3 diff = currentPos - PreviousPosition;
				StartPosition = StartPosition + diff;

				Vector3 newPos = StartPosition + new Vector3 (x, y, z);
				Target.Position = newPos;

				PreviousPosition = newPos;
			}
		}

	}

	public struct BezierConfig
	{
		public Vector3 ControlPoint1;
		public Vector3 ControlPoint2;
		public Vector3 EndPosition;
	}
}