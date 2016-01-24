using Urho;
namespace Urho.Actions
{
	public class Speed : BaseAction
	{
		public float SpeedValue { get; }

		protected internal FiniteTimeAction InnerAction { get; }


		#region Constructors

		public Speed (FiniteTimeAction action, float speedValue)
		{
			InnerAction = action;
			SpeedValue = speedValue;
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new SpeedState (this, target);
		}

		public virtual FiniteTimeAction Reverse ()
		{
			return (FiniteTimeAction)(BaseAction)new Speed ((FiniteTimeAction)InnerAction.Reverse(), SpeedValue);
		}
	}


	#region Action state

	internal class SpeedState : ActionState
	{
		#region Properties

		public float Speed { get; }

		protected FiniteTimeActionState InnerActionState { get; }

		public override bool IsDone 
		{
			get { return InnerActionState.IsDone; }
		}

		#endregion Properties

		public SpeedState (Speed action, Node target) : base (action, target)
		{
			this.InnerActionState = (FiniteTimeActionState)action.InnerAction.StartAction (target);
			this.Speed = action.SpeedValue;
		}

		protected internal override void Stop ()
		{
			InnerActionState.Stop ();
			base.Stop ();
		}

		protected internal override void Step (float dt)
		{
			InnerActionState.Step (dt * Speed);
		}
	}

	#endregion Action state
}