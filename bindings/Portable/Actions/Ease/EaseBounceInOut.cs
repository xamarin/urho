using Urho;
namespace Urho.Actions
{
	public class EaseBounceInOut : ActionEase
	{
		#region Constructors

		public EaseBounceInOut (FiniteTimeAction action) : base (action)
		{
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new EaseBounceInOutState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new EaseBounceInOut ((FiniteTimeAction)InnerAction.Reverse ());
		}
	}


	#region Action state

	public class EaseBounceInOutState : ActionEaseState
	{
		public EaseBounceInOutState (EaseBounceInOut action, Node target) : base (action, target)
		{
		}

		public override void Update (float time)
		{
			InnerActionState.Update (EaseMath.BounceInOut (time));
		}
	}

	#endregion Action state
}