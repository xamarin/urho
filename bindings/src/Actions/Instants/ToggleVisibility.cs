using Urho;
namespace Urho.Actions
{
	public class ToggleVisibility : ActionInstant
	{
		#region Constructors

		public ToggleVisibility ()
		{
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new ToggleVisibilityState (this, target);
		}
	}

	public class ToggleVisibilityState : ActionInstantState
	{
		public ToggleVisibilityState (ToggleVisibility action, Node target)
			: base (action, target)
		{
			target.Enabled = !target.Enabled;
		}
	}
}