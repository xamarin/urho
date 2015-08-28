using System;
using System.Diagnostics;

namespace Urho
{
	public class Spawn : FiniteTimeAction
	{
		public FiniteTimeAction ActionOne { get; protected set; }
		public FiniteTimeAction ActionTwo { get; protected set; }


		#region Constructors

		protected Spawn (FiniteTimeAction action1, FiniteTimeAction action2)
			: base (Math.Max (action1.Duration, action2.Duration))
		{
			InitSpawn (action1, action2);
		}

		public Spawn (params FiniteTimeAction[] actions)
		{
			FiniteTimeAction prev = actions [0];
			FiniteTimeAction next = null;

			if (actions.Length == 1)
			{
				next = new ExtraAction ();
			}
			else
			{
				// We create a nested set of SpawnActions out of all of the actions
				for (int i = 1; i < actions.Length - 1; i++)
				{
					prev = new Spawn (prev, actions [i]);
				}

				next = actions [actions.Length - 1];
			}

			// Can't call base(duration) because we need to determine max duration
			// Instead call base's init method here
			if (prev != null && next != null)
			{
				Duration = Math.Max (prev.Duration, next.Duration);
				InitSpawn (prev, next);
			}
		}

		private void InitSpawn (FiniteTimeAction action1, FiniteTimeAction action2)
		{
			Debug.Assert (action1 != null);
			Debug.Assert (action2 != null);

			float d1 = action1.Duration;
			float d2 = action2.Duration;

			ActionOne = action1;
			ActionTwo = action2;

			if (d1 > d2)
			{
				ActionTwo = new Sequence (action2, new DelayTime (d1 - d2));
			}
			else if (d1 < d2)
			{
				ActionOne = new Sequence (action1, new DelayTime (d2 - d1));
			}
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new SpawnState (this, target);

		}

		public override FiniteTimeAction Reverse ()
		{
			return new Spawn (ActionOne.Reverse (), ActionTwo.Reverse ());
		}
	}

	public class SpawnState : FiniteTimeActionState
	{

		protected FiniteTimeAction ActionOne { get; set; }

		private FiniteTimeActionState ActionStateOne { get; set; }

		protected FiniteTimeAction ActionTwo { get; set; }

		private FiniteTimeActionState ActionStateTwo { get; set; }

		public SpawnState (Spawn action, Node target)
			: base (action, target)
		{ 
			ActionOne = action.ActionOne;
			ActionTwo = action.ActionTwo;

			ActionStateOne = (FiniteTimeActionState)ActionOne.StartAction (target);
			ActionStateTwo = (FiniteTimeActionState)ActionTwo.StartAction (target);
		}

		protected internal override void Stop ()
		{
			ActionStateOne.Stop ();
			ActionStateTwo.Stop ();

			base.Stop ();
		}

		public override void Update (float time)
		{
			if (ActionOne != null)
			{
				ActionStateOne.Update (time);
			}

			if (ActionTwo != null)
			{
				ActionStateTwo.Update (time);
			}
		}

	}

}
