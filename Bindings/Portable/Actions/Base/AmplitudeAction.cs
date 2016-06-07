using Urho;

namespace Urho.Actions
{
	public abstract class AmplitudeAction : FiniteTimeAction
	{
		public float Amplitude { get; }

		#region Constructors

		protected AmplitudeAction (float duration, float amplitude = 0) : base (duration)
		{
			Amplitude = amplitude;
		}

		#endregion Constructors
	}


	#region Action state

	public abstract class AmplitudeActionState : FiniteTimeActionState
	{
		protected float Amplitude { get; private set; }
		protected internal float AmplitudeRate { get; set; }

		protected AmplitudeActionState (AmplitudeAction action, Node target) : base (action, target)
		{
			Amplitude = action.Amplitude;
			AmplitudeRate = 1.0f;
		}
	}

	#endregion Action state
}
