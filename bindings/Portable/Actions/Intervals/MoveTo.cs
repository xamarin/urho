using Urho;

namespace Urho.Actions
{
	public class MoveTo : MoveBy
	{
		protected Vector3 EndPosition;

		#region Constructors

		public MoveTo (float duration, Vector3 position) : base (duration, position)
		{
			EndPosition = position;
		}

		#endregion Constructors

		public Vector3 PositionEnd {
			get { return EndPosition; }
		}

		protected internal override ActionState StartAction(Node target)
		{
			return new MoveToState (this, target);
		}
	}

	public class MoveToState : MoveByState
	{
		public MoveToState (MoveTo action, Node target)
			: base (action, target)
		{ 
			StartPosition = target.Position;
			PositionDelta = action.PositionEnd - target.Position;
		}

		public override void Update (float time)
		{
			if (Target != null)
			{
				Vector3 newPos = StartPosition + PositionDelta * time;
				Target.Position = newPos;
				PreviousPosition = newPos;
			}
		}
	}
}