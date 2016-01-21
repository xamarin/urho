using Urho;
namespace Urho.Actions
{
	public class EaseBackOut : ActionEase
	{
		#region Constructors

		public EaseBackOut (FiniteTimeAction action) : base (action)
		{
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new EaseBackOutState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new EaseBackIn ((FiniteTimeAction)InnerAction.Reverse ());
		}
	}


	#region Action state

	public class EaseBackOutState : ActionEaseState
	{
		public EaseBackOutState (EaseBackOut action, Node target) : base (action, target)
		{
		}

		public override void Update (float time)
		{
			InnerActionState.Update (EaseMath.BackOut (time));
		}
	}

	#endregion Action state
}