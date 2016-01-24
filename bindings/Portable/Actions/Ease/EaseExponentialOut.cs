using System;

using Urho;
namespace Urho.Actions
{
	public class EaseExponentialOut : ActionEase
	{
		#region Constructors

		public EaseExponentialOut (FiniteTimeAction action) : base (action)
		{
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new EaseExponentialOutState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new EaseExponentialIn ((FiniteTimeAction)InnerAction.Reverse ());
		}
	}


	#region Action state

	public class EaseExponentialOutState : ActionEaseState
	{
		public EaseExponentialOutState (EaseExponentialOut action, Node target) : base (action, target)
		{
		}

		public override void Update (float time)
		{
			InnerActionState.Update (EaseMath.ExponentialOut (time));
		}
	}

	#endregion Action state
}