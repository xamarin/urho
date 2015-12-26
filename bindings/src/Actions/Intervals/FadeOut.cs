using System;
using Urho;
using Urho.Shapes;
using Urho.Urho2D;

namespace Urho.Actions
{
	public class FadeOut : FiniteTimeAction
	{
		#region Constructors

		public FadeOut (float duration) : base (duration)
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
		Shape shape;

		public FadeOutState (FadeOut action, Node target)
			: base (action, target)
		{
			staticSprite = Target.GetComponent<StaticSprite2D>();
			if (staticSprite != null)
				return;

			shape = Target.GetComponent<Shape>();
			if (shape != null)
				return;

			throw new NotSupportedException("The node should have StaticSprite2D or Shape component");
		}

		public override void Update (float time)
		{
			if (staticSprite != null)
				staticSprite.Alpha = 1 - time;

			if (shape != null)
				shape.Color = new Color(shape.Color, 1 - time);
		}
	}
}