using Urho;

namespace Urho.Actions
{
	public class MoveBy : FiniteTimeAction
	{
		#region Constructors

		public MoveBy (float duration, Vector3 position) : base (duration)
		{
			PositionDelta = position;
		}

		#endregion Constructors

		public Vector3 PositionDelta { get; private set; }

		protected internal override ActionState StartAction(Node target)
		{
			return new MoveByState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new MoveBy (Duration, new Vector3(-PositionDelta.X, -PositionDelta.Y, -PositionDelta.Z));
		}
	}

	public class MoveByState : FiniteTimeActionState
	{
		protected Vector3 PositionDelta;
		protected Vector3 EndPosition;
		protected Vector3 StartPosition;
		protected Vector3 PreviousPosition;

		public MoveByState (MoveBy action, Node target)
			: base (action, target)
		{ 
			PositionDelta = action.PositionDelta;
			PreviousPosition = StartPosition = target.Position;
		}

		public override void Update (float time)
		{
			if (Target == null)
				return;

			var currentPos = Target.Position;
			var diff = currentPos - PreviousPosition;
			StartPosition = StartPosition + diff;
			Vector3 newPos = StartPosition + PositionDelta * time;
			Target.Position = newPos;
			PreviousPosition = newPos;
		}
	}

}