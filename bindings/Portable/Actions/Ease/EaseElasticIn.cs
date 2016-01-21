using System;
using Urho;
namespace Urho.Actions
{
	public class EaseElasticIn : EaseElastic
	{
		#region Constructors

		public EaseElasticIn (FiniteTimeAction action) : this (action, 0.3f)
		{
		}

		public EaseElasticIn (FiniteTimeAction action, float period) : base (action, period)
		{
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new EaseElasticInState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new EaseElasticOut ((FiniteTimeAction)InnerAction.Reverse (), Period);
		}
	}


	#region Action state

	public class EaseElasticInState : EaseElasticState
	{
		public EaseElasticInState (EaseElasticIn action, Node target) : base (action, target)
		{
		}

		public override void Update (float time)
		{
			InnerActionState.Update (EaseMath.ElasticIn (time, Period));
		}
	}

	#endregion Action state
}