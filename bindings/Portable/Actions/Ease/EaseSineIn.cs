using Urho;
namespace Urho.Actions
{
	public class EaseSineIn : ActionEase
	{
		#region Constructors

		public EaseSineIn (FiniteTimeAction action) : base (action)
		{
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new EaseSineInState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new EaseSineOut ((FiniteTimeAction)InnerAction.Reverse ());
		}
	}


	#region Action state

	public class EaseSineInState : ActionEaseState
	{
		public EaseSineInState (EaseSineIn action, Node target) : base (action, target)
		{
		}

		public override void Update (float time)
		{
			InnerActionState.Update (EaseMath.SineIn (time));
		}
	}

	#endregion Action state
}