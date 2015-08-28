namespace Urho
{
	public class Show : ActionInstant
	{
		protected internal override ActionState StartAction(Node target)
		{
			return new ShowState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return (new Hide ());
		}
	}

	public class ShowState : ActionInstantState
	{
		public ShowState (Show action, Node target)
			: base (action, target)
		{   
			target.SetEnabled(true);
		}
	}
}