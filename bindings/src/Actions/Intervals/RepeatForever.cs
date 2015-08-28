using System.Diagnostics;

namespace Urho
{
	public class RepeatForever : FiniteTimeAction
	{
		public FiniteTimeAction InnerAction { get; private set; }


		#region Constructors

		public RepeatForever (params FiniteTimeAction[] actions)
		{
			Debug.Assert (actions != null);
			InnerAction = new Sequence (actions);

		}

		public RepeatForever (FiniteTimeAction action)
		{
			Debug.Assert (action != null);
			InnerAction = action;
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new RepeatForeverState (this, target);

		}

		public override FiniteTimeAction Reverse ()
		{
			return new RepeatForever (InnerAction.Reverse () as FiniteTimeAction);
		}
	}

	public class RepeatForeverState : FiniteTimeActionState
	{

		private FiniteTimeAction InnerAction { get; set; }

		private FiniteTimeActionState InnerActionState { get; set; }

		public RepeatForeverState (RepeatForever action, Node target)
			: base (action, target)
		{ 
			InnerAction = action.InnerAction;
			InnerActionState = (FiniteTimeActionState)InnerAction.StartAction (target);
		}

		protected internal override void Step (float dt)
		{
			InnerActionState.Step (dt);

			if (InnerActionState.IsDone)
			{
				float diff = InnerActionState.Elapsed - InnerActionState.Duration;
				InnerActionState = (FiniteTimeActionState)InnerAction.StartAction (Target);
				InnerActionState.Step (0f);
				InnerActionState.Step (diff);
			}
		}

		public override bool IsDone {
			get { return false; }
		}

	}
}