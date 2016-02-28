using Urho;
namespace Urho.Actions
{
	public class Hide : ActionInstant
	{
		#region Constructors

		public Hide ()
		{
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new HideState (this, target);

		}

		public override FiniteTimeAction Reverse ()
		{
			return (new Show ());
		}
	}

	public class HideState : ActionInstantState
	{
		public HideState (Hide action, Node target)
			: base (action, target)
		{   
			target.Enabled = false;
		}
	}
}