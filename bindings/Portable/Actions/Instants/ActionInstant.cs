using Urho;
namespace Urho.Actions
{
	public class ActionInstant : FiniteTimeAction
	{
		#region Constructors

		protected ActionInstant ()
		{
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new ActionInstantState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new ActionInstant ();
		}
	}

	public class ActionInstantState : FiniteTimeActionState
	{

		public ActionInstantState (ActionInstant action, Node target)
			: base (action, target)
		{
		}

		public override bool IsDone => true;

		protected internal override void Step (float dt)
		{
			Update (1);
		}

		public override void Update (float time)
		{
			// ignore
		}
	}
}