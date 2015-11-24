using System;

namespace Urho.Actions
{
	public abstract class FiniteTimeAction : BaseAction
	{
		float duration;

		#region Properties

		public virtual float Duration 
		{
			get 
			{
				return duration;
			}
			set 
			{
				float newDuration = value;

				// Prevent division by 0
				if (newDuration == 0)
				{
					newDuration = float.Epsilon;
				}

				duration = newDuration;
			}
		}

		#endregion Properties


		#region Constructors

		protected FiniteTimeAction() 
			: this (0)
		{
		}

		protected FiniteTimeAction (float duration)
		{
			Duration = duration;
		}

		#endregion Constructors

		public abstract FiniteTimeAction Reverse();

		protected internal override ActionState StartAction(Node target)
		{
			return new FiniteTimeActionState (this, target);
		}
	}

	public class FiniteTimeActionState : ActionState
	{
		bool firstTick;

		#region Properties

		public virtual float Duration { get; set; }
		public float Elapsed { get; private set; }

		public override bool IsDone => Elapsed >= Duration;

		#endregion Properties

		public FiniteTimeActionState (FiniteTimeAction action, Node target)
			: base (action, target)
		{ 
			Duration = action.Duration;
			Elapsed = 0.0f;
			firstTick = true;
		}

		protected internal override void Step(float dt)
		{
			if (firstTick)
			{
				firstTick = false;
				Elapsed = 0f;
			}
			else
			{
				Elapsed += dt;
			}

			Update (Math.Max (0f,Math.Min (1, Elapsed / Math.Max (Duration, float.Epsilon))));
		}

	}
}