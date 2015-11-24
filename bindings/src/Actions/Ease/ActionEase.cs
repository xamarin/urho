using Urho;
namespace Urho.Actions
{
	public class ActionEase : FiniteTimeAction
	{
		protected internal FiniteTimeAction InnerAction { get; private set; }

		#region Constructors

		public ActionEase(FiniteTimeAction action) : base (action.Duration)
		{
			InnerAction = action;
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new ActionEaseState (this, target);
		}

		public override FiniteTimeAction Reverse()
		{
			return new ActionEase ((FiniteTimeAction)InnerAction.Reverse ());
		}
	}


	#region Action state

	public class ActionEaseState : FiniteTimeActionState
	{
		protected FiniteTimeActionState InnerActionState { get; private set; }

		public ActionEaseState (ActionEase action, Node target) : base (action, target)
		{
			InnerActionState = (FiniteTimeActionState)action.InnerAction.StartAction (target);
		}

		protected internal override void Stop ()
		{
			InnerActionState.Stop ();
			base.Stop ();
		}

		public override void Update (float time)
		{
			InnerActionState.Update (time);
		}
	}

	#endregion Action state
}