using System;
using Urho;
using Urho.Shapes;
using Urho.Urho2D;

namespace Urho.Actions
{
	public class FadeTo : FiniteTimeAction
	{
		public float ToOpacity { get; }

		#region Constructors

		public FadeTo (float duration, float opacity) : base (duration)
		{
			ToOpacity = opacity;
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new FadeToState (this, target);
		}

		public override FiniteTimeAction Reverse()
		{
			throw new NotImplementedException();
		}
	}

	public class FadeToState : FiniteTimeActionState
	{
		StaticSprite2D staticSprite;
		Shape shape;

		protected float FromOpacity { get; set; }

		protected float ToOpacity { get; set; }

		public FadeToState (FadeTo action, Node target)
			: base (action, target)
		{
			ToOpacity = action.ToOpacity;


			staticSprite = Target.GetComponent<StaticSprite2D>();
			if (staticSprite != null)
			{
				FromOpacity = staticSprite.Alpha;
				return;
			}

			shape = Target.GetComponent<Shape>();
			if (shape != null)
			{
				FromOpacity = shape.Color.A;
				return;
			}

			throw new NotSupportedException("The node should have StaticSprite2D or Shape component");
		}

		public override void Update (float time)
		{
			if (staticSprite != null)
				staticSprite.Alpha = FromOpacity + (ToOpacity - FromOpacity) * time;

			if (shape != null)
				shape.Color = new Color(shape.Color, FromOpacity + (ToOpacity - FromOpacity) * time);
		}
	}
}