using System;

using Urho;
namespace Urho.Actions
{
	public class EaseSineInOut : ActionEase
	{
		#region Constructors

		public EaseSineInOut (FiniteTimeAction action) : base (action)
		{
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new EaseSineInOutState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new EaseSineInOut ((FiniteTimeAction)InnerAction.Reverse ());
		}
	}


	#region Action state

	public class EaseSineInOutState : ActionEaseState
	{
		public EaseSineInOutState (EaseSineInOut action, Node target) : base (action, target)
		{
		}

		public override void Update (float time)
		{
			InnerActionState.Update (EaseMath.SineInOut (time));
		}
	}

	#endregion Action state
}