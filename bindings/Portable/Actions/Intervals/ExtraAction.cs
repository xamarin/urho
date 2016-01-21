using Urho;
namespace Urho.Actions
{
	// Extra action for making a Sequence or Spawn when only adding one action to it.
	internal class ExtraAction : FiniteTimeAction
	{
		public override FiniteTimeAction Reverse ()
		{
			return new ExtraAction ();
		}

		protected internal override ActionState StartAction(Node target)
		{
			return new ExtraActionState (this, target);

		}

		#region Action State

		public class ExtraActionState : FiniteTimeActionState
		{

			public ExtraActionState (ExtraAction action, Node target)
				: base (action, target)
			{
			}

			protected internal override void Step (float dt)
			{
			}

			public override void Update (float time)
			{
			}
		}

		#endregion Action State
	}
}