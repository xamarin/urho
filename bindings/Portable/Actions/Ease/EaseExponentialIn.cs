using System;

using Urho;
namespace Urho.Actions
{
	public class EaseExponentialIn : ActionEase
	{
		#region Constructors

		public EaseExponentialIn (FiniteTimeAction action) : base (action)
		{
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new EaseExponentialInState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new EaseExponentialOut ((FiniteTimeAction)InnerAction.Reverse ());
		}
	}


	#region Action state

	public class EaseExponentialInState : ActionEaseState
	{
		public EaseExponentialInState (EaseExponentialIn action, Node target) : base (action, target)
		{
		}

		public override void Update (float time)
		{
			InnerActionState.Update (EaseMath.ExponentialIn (time));
		}
	}

	#endregion Action state
}