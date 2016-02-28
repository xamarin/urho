using System;

using Urho;
namespace Urho.Actions
{
	public class EaseExponentialInOut : ActionEase
	{
		#region Constructors

		public EaseExponentialInOut (FiniteTimeAction action) : base(action)
		{
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new EaseExponentialInOutState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new EaseExponentialInOut ((FiniteTimeAction)InnerAction.Reverse ());
		}
	}


	#region Action state

	public class EaseExponentialInOutState : ActionEaseState
	{
		public EaseExponentialInOutState (EaseExponentialInOut action, Node target) : base (action, target)
		{
		}

		public override void Update (float time)
		{
			InnerActionState.Update (EaseMath.ExponentialInOut (time));
		}
	}

	#endregion Action state
}