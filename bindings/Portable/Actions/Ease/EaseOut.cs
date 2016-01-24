using System;

using Urho;
namespace Urho.Actions
{
	public class EaseOut : EaseRateAction
	{
		#region Constructors

		public EaseOut (FiniteTimeAction action, float rate) : base (action, rate)
		{
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new EaseOutState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new EaseOut ((FiniteTimeAction)InnerAction.Reverse (), 1 / Rate);
		}
	}


	#region Action state

	public class EaseOutState : EaseRateActionState
	{
		public EaseOutState (EaseOut action, Node target) : base (action, target)
		{
		}

		public override void Update (float time)
		{
			InnerActionState.Update ((float)(Math.Pow (time, 1 / Rate)));      
		}
	}

	#endregion Action state
}