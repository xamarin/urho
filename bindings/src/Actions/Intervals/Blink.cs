using Urho;
namespace Urho.Actions
{
	public class Blink : FiniteTimeAction
	{
		public uint Times { get; private set; }


		#region Constructors

		public Blink (float duration, uint numOfBlinks) : base (duration)
		{
			Times = numOfBlinks;
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new BlinkState (this, target);

		}

		public override FiniteTimeAction Reverse ()
		{
			return new Blink (Duration, Times);
		}
	}

	public class BlinkState : FiniteTimeActionState
	{

		protected uint Times { get; set; }

		protected bool OriginalState { get; set; }

		public BlinkState (Blink action, Node target)
			: base (action, target)
		{ 
			Times = action.Times;
			OriginalState = target.Enabled;
		}

		public override void Update (float time)
		{
			if (Target != null && !IsDone)
			{
				float slice = 1.0f / Times;
				float m = time % slice;
				Target.Enabled = m > (slice / 2);
			}
		}

		protected internal override void Stop ()
		{
			Target.Enabled = OriginalState;
			base.Stop ();
		}

	}
}