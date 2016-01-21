using System;

using Urho;
namespace Urho.Actions
{
	public partial class EaseCustom : ActionEase
	{
		public Func<float, float> EaseFunc { get; private set; }


		#region Constructors

		public EaseCustom (FiniteTimeAction action, Func<float, float> easeFunc) : base (action)
		{
			EaseFunc = easeFunc;
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new EaseCustomState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new ReverseTime (this);
		}
	}


	#region Action state

	public class EaseCustomState : ActionEaseState
	{
		protected Func<float, float> EaseFunc { get; private set; }

		public EaseCustomState (EaseCustom action, Node target) : base (action, target)
		{
			EaseFunc = action.EaseFunc;
		}

		public override void Update (float time)
		{
			InnerActionState.Update (EaseFunc (time));
		}
	}

	#endregion Action state
}