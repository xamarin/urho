using System;
using Urho;
using Urho.Shapes;
using Urho.Urho2D;

namespace Urho.Actions
{
	public class FadeIn : FiniteTimeAction
	{
		#region Constructors

		public FadeIn (float duration) : base (duration)
		{
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new FadeInState (this, target);

		}

		public override FiniteTimeAction Reverse ()
		{
			return new FadeOut (Duration);
		}
	}

	public class FadeInState : FiniteTimeActionState
	{
		StaticSprite2D staticSprite;
		Shape shape;

		protected uint Times { get; set; }

		protected bool OriginalState { get; set; }

		public FadeInState (FadeIn action, Node target)
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
				staticSprite.Alpha = time;

			if (shape != null)
				shape.Color = new Color(shape.Color, time);
		}
	}
}