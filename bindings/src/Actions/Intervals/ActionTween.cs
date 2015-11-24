using System.Diagnostics;
using System;

using Urho;
namespace Urho.Actions
{
	public interface IActionTweenDelegate
	{
		void UpdateTweenAction (float value, string key);
	}

	public class ActionTween : FiniteTimeAction
	{
		#region Properties

		public float From { get; private set; }
		public float To { get; private set; }
		public string Key { get; private set; }
		public Action<float, string> TweenAction { get; private set; }

		#endregion Properties


		#region Constructors

		public ActionTween (float duration, string key, float from, float to)
			: base (duration)
		{
			Key = key;
			To = to;
			From = from;
		}

		public ActionTween (float duration, string key, float from, float to, Action<float,string> tweenAction) : this (duration, key, from, to)
		{
			TweenAction = tweenAction;
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new ActionTweenState (this, target);

		}

		public override FiniteTimeAction Reverse ()
		{
			return new ActionTween (Duration, Key, To, From, TweenAction);
		}
	}

	public class ActionTweenState : FiniteTimeActionState
	{
		protected float Delta;

		protected float From { get; private set; }

		protected float To { get; private set; }

		protected string Key { get; private set; }

		protected Action<float, string> TweenAction { get; private set; }

		public ActionTweenState (ActionTween action, Node target)
			: base (action, target)
		{ 
			Debug.Assert (Target is IActionTweenDelegate, "target must implement ActionTweenDelegate");
			TweenAction = action.TweenAction;
			From = action.From;
			To = action.To;
			Key = action.Key;
			Delta = To - From;
		}

		public override void Update (float time)
		{
			float amt = To - Delta * (1 - time);
			if (TweenAction != null)
			{
				TweenAction (amt, Key);
			}
			else if (Target is IActionTweenDelegate)
			{
				((IActionTweenDelegate)Target).UpdateTweenAction (amt, Key);
			}
		}

	}
}