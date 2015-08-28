namespace Urho
{
	public abstract class FiniteTimeAction : Action
	{
		public float Duration { get; }

		protected FiniteTimeAction(float duration)
		{
			Duration = duration;
		}
	}
}
