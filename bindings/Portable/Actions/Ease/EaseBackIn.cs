using Urho;
namespace Urho.Actions
{
	public class EaseBackIn : ActionEase
	{
		#region Constructors

		public EaseBackIn (FiniteTimeAction action) : base (action)
		{
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new EaseBackInState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new EaseBackOut ((FiniteTimeAction)InnerAction.Reverse ());
		}
	}


	#region Action state

	public class EaseBackInState : ActionEaseState
	{
		public EaseBackInState (EaseBackIn action, Node target) : base (action, target)
		{
		}

		public override void Update (float time)
		{
			InnerActionState.Update (EaseMath.BackIn (time));
		}
	}

	#endregion Action state
}