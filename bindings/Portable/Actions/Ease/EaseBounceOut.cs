using Urho;
namespace Urho.Actions
{
	public class EaseBounceOut : ActionEase
	{
		#region Constructors

		public EaseBounceOut (FiniteTimeAction action) : base (action)
		{
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new EaseBounceOutState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new EaseBounceIn ((FiniteTimeAction)InnerAction.Reverse ());
		}
	}


	#region Action state

	public class EaseBounceOutState : ActionEaseState
	{
		public EaseBounceOutState (EaseBounceOut action, Node target) : base (action, target)
		{
		}

		public override void Update (float time)
		{
			InnerActionState.Update (EaseMath.BounceOut (time));
		}
	}

	#endregion Action state
}