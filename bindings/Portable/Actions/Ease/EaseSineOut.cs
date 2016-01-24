using System;

using Urho;
namespace Urho.Actions
{
	public class EaseSineOut : ActionEase
	{
		#region Constructors

		public EaseSineOut (FiniteTimeAction action) : base (action)
		{
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new EaseSineOutState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new EaseSineIn ((FiniteTimeAction)InnerAction.Reverse ());
		}
	}


	#region Action state

	public class EaseSineOutState : ActionEaseState
	{
		public EaseSineOutState (EaseSineOut action, Node target) : base (action, target)
		{
		}

		public override void Update (float time)
		{
			InnerActionState.Update (EaseMath.SineOut (time));
		}
	}

	#endregion Action state
}