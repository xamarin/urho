using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Urho;

namespace Urho.Actions
{
	public class Sequence : FiniteTimeAction
	{
		public FiniteTimeAction[] Actions { get; } = new FiniteTimeAction[2];

		public Action<SequenceState> CancelAction { get; set; }

		#region Constructors

		public Sequence (FiniteTimeAction action1, FiniteTimeAction action2) : base (action1.Duration + action2.Duration)
		{
			InitSequence (action1, action2);
		}

		public Sequence(FiniteTimeAction[] actions, FiniteTimeAction other)
		{
			InitFromArray(actions, other);
		}

		public Sequence (FiniteTimeAction[] actions) : base ()
		{
			InitFromArray(actions, null);
		}

		void InitFromArray(FiniteTimeAction[] actions, FiniteTimeAction other)
		{
			var prev = actions[0];

			// Can't call base(duration) because we need to calculate duration here
			float combinedDuration = 0.0f;
			foreach (FiniteTimeAction action in actions)
			{
				combinedDuration += action.Duration;
			}
			Duration = combinedDuration;

			if (actions.Length == 1)
			{
				InitSequence(prev, other ?? new ExtraAction());
			}
			else
			{
				// Basically what we are doing here is creating a whole bunch of 
				// nested Sequences from the actions.
				int count = other != null ? actions.Length : actions.Length - 1;

				for (int i = 1; i < count; i++)
				{
					prev = new Sequence(prev, actions[i]);
				}

				if (other != null)
				{
					InitSequence(prev, other);
				}
				else
				{
					InitSequence(prev, actions[actions.Length - 1]);
				}
			}
		}

		void InitSequence (FiniteTimeAction actionOne, FiniteTimeAction actionTwo)
		{
			Debug.Assert (actionOne != null);
			Debug.Assert (actionTwo != null);

			Actions [0] = actionOne;
			Actions [1] = actionTwo;
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new SequenceState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new Sequence (Actions [1].Reverse (), Actions [0].Reverse ());
		}
	}

	public class SequenceState : FiniteTimeActionState
	{
		protected int last;
		protected FiniteTimeAction[] actionSequences = new FiniteTimeAction[2];
		protected FiniteTimeActionState[] actionStates = new FiniteTimeActionState[2];
		protected float split;
		bool hasInfiniteAction = false;
		Action<SequenceState> cancelAction;

		public SequenceState (Sequence action, Node target)
			: base (action, target)
		{
			cancelAction = action.CancelAction;
			actionSequences = action.Actions;
			hasInfiniteAction = (actionSequences [0] is RepeatForever) || (actionSequences [1] is RepeatForever);
			split = actionSequences [0].Duration / Duration;
			last = -1;
		}

		public override bool IsDone {
			get {
				if (hasInfiniteAction && actionSequences [last] is RepeatForever)
				{
					return false;
				}

				return base.IsDone;
			}
		}


		protected internal override void Stop ()
		{
			// Issue #1305
			if (last != -1)
			{
				actionStates [last].Stop ();
			}
		}

		protected internal override void Step (float dt)
		{
			if (last > -1 && (actionSequences [last] is RepeatForever))
			{
				actionStates [last].Step (dt);
			}
			else
			{
				base.Step (dt);
			}
		}

		public override void Update (float time)
		{
			int found;
			float new_t;

			if (time < split)
			{
				// action[0]
				found = 0;
				if (split != 0)
					new_t = time / split;
				else
					new_t = 1;
			}
			else
			{
				// action[1]
				found = 1;
				if (split == 1)
					new_t = 1;
				else
					new_t = (time - split) / (1 - split);
			}

			if (found == 1)
			{
				if (last == -1)
				{
					// action[0] was skipped, execute it.
					actionStates [0] = (FiniteTimeActionState)actionSequences [0].StartAction (Target);
					actionStates [0].Update (1.0f);
					actionStates [0].Stop ();
				}
				else if (last == 0)
				{
					actionStates [0].Update (1.0f);
					actionStates [0].Stop ();
				}
			}
			else if (found == 0 && last == 1)
			{
				// Reverse mode ?
				// XXX: Bug. this case doesn't contemplate when _last==-1, found=0 and in "reverse mode"
				// since it will require a hack to know if an action is on reverse mode or not.
				// "step" should be overriden, and the "reverseMode" value propagated to inner Sequences.
				actionStates [1].Update (0);
				actionStates [1].Stop ();

			}

			// Last action found and it is done.
			if (found == last && actionStates [found].IsDone)
			{
				return;
			}


			// Last action found and it is done
			if (found != last)
			{
				actionStates [found] = (FiniteTimeActionState)actionSequences [found].StartAction (Target);
			}

			actionStates [found].Update (new_t);
			last = found;
		}

		internal void Cancel() => cancelAction?.Invoke(this);
	}
}