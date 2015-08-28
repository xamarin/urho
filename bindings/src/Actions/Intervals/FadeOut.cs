using System;

namespace Urho
{
	public class FadeOut : FiniteTimeAction
	{
		#region Constructors

		public FadeOut (float durtaion) : base (durtaion)
		{
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new FadeOutState (this, target);

		}

		public override FiniteTimeAction Reverse ()
		{
			return new FadeIn (Duration);
		}
	}

	public class FadeOutState : FiniteTimeActionState
	{
		StaticSprite2D staticSprite;

		public FadeOutState (FadeOut action, Node target)
			: base (action, target)
		{
			staticSprite = Target.GetComponent<StaticSprite2D>();
			if (staticSprite == null)
			{
				throw new NotSupportedException("The node should have StaticSprite2D");
			}
		}

		public override void Update (float time)
		{
			staticSprite.Alpha = 1 - time;
		}
	}
}