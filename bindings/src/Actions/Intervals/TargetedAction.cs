using Urho;

namespace Urho.Actions
{
	public class TargetedAction : FiniteTimeAction
	{
		public FiniteTimeAction Action { get; private set; }
		public Node ForcedTarget { get; private set; }

		#region Constructors

		public TargetedAction (Node target, FiniteTimeAction action) : base (action.Duration)
		{
			ForcedTarget = target;
			Action = action;
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new TargetedActionState (this, target);
		}

		public override FiniteTimeAction Reverse()
		{
			return new TargetedAction (ForcedTarget, Action.Reverse ());
		}
	}

	public class TargetedActionState : FiniteTimeActionState
	{
		protected FiniteTimeAction TargetedAction { get; set; }

		protected FiniteTimeActionState ActionState { get; set; }

		protected Node ForcedTarget { get; set; }

		public TargetedActionState (TargetedAction action, Node target)
			: base (action, target)
		{   
			ForcedTarget = action.ForcedTarget;
			TargetedAction = action.Action;

			ActionState = (FiniteTimeActionState)TargetedAction.StartAction (ForcedTarget);
		}

		protected internal override void Stop ()
		{
			ActionState.Stop ();
		}

		public override void Update (float time)
		{
			ActionState.Update (time);
		}
	}
}