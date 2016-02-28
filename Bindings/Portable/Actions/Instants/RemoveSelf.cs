using System;

using Urho;
namespace Urho.Actions
{
	public class RemoveSelf : ActionInstant
	{
		protected internal override ActionState StartAction(Node target)
		{
			return new RemoveSelfState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			throw new NotSupportedException();
		}
	}

	public class RemoveSelfState : ActionInstantState
	{
		public RemoveSelfState (RemoveSelf action, Node target)
			: base (action, target)
		{   
		}

		public override void Update (float time)
		{
			Target.Parent.RemoveChild(Target);
		}
	}
}