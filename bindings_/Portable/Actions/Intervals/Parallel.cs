using System;
using System.Collections.Generic;
using Urho;

namespace Urho.Actions
{
	public class Parallel : FiniteTimeAction
	{
		public FiniteTimeAction[] Actions { get; private set; }

		#region Constructors

		public Parallel (params FiniteTimeAction[] actions) : base ()
		{
			// Can't call base(duration) because max action duration needs to be determined here
			float maxDuration = 0.0f;
			foreach (FiniteTimeAction action in actions)
			{
				if (action.Duration > maxDuration)
				{
					maxDuration = action.Duration;
				}
			}
			Duration = maxDuration;

			Actions = actions;

			for (int i = 0; i < Actions.Length; i++)
			{
				var actionDuration = Actions [i].Duration;
				if (actionDuration < Duration)
				{
					Actions [i] = new Sequence (Actions [i], new DelayTime (Duration - actionDuration));
				}
			}
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new ParallelState (this, target);

		}

		public override FiniteTimeAction Reverse ()
		{
			FiniteTimeAction[] rev = new FiniteTimeAction[Actions.Length];
			for (int i = 0; i < Actions.Length; i++)
			{
				rev [i] = Actions [i].Reverse ();
			}

			return new Parallel (rev);
		}

	}

	public class ParallelState : FiniteTimeActionState
	{

		protected FiniteTimeAction[] Actions { get; set; }

		protected FiniteTimeActionState[] ActionStates { get; set; }

		public ParallelState (Parallel action, Node target)
			: base (action, target)
		{   
			Actions = action.Actions;
			ActionStates = new FiniteTimeActionState[Actions.Length];

			for (int i = 0; i < Actions.Length; i++)
			{
				ActionStates [i] = (FiniteTimeActionState)Actions [i].StartAction (target);
			}
		}

		protected internal override void Stop ()
		{
			for (int i = 0; i < Actions.Length; i++)
			{
				ActionStates [i].Stop ();
			}
			base.Stop ();
		}

		public override void Update (float time)
		{
			for (int i = 0; i < Actions.Length; i++)
			{
				ActionStates [i].Update (time);
			}
		}
	}
}