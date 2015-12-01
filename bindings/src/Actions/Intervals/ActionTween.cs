using System;

namespace Urho.Actions
{
	public class ActionTween : FiniteTimeAction
	{
		#region Properties

		public float From { get; private set; }
		public float To { get; private set; }
		public string Key { get; private set; }
		public Action<float, string> TweenAction { get; private set; }

		#endregion Properties

		#region Constructors

		public ActionTween (float duration, string key, float from, float to, Action<float,string> tweenAction) : base(duration)
		{
			Key = key;
			To = to;
			From = from;
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
			TweenAction = action.TweenAction;
			From = action.From;
			To = action.To;
			Key = action.Key;
			Delta = To - From;
		}

		public override void Update (float time)
		{
			float amt = To - Delta * (1 - time);
			TweenAction?.Invoke (amt, Key);
		}

	}
}