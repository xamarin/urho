using System;

namespace Urho
{
	public class FadeTo : FiniteTimeAction
	{
		public byte ToOpacity { get; }

		#region Constructors

		public FadeTo (float duration, byte opacity) : base (duration)
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

		protected float FromOpacity { get; set; }

		protected float ToOpacity { get; set; }

		public FadeToState (FadeTo action, Node target)
			: base (action, target)
		{              
			ToOpacity = action.ToOpacity;

			staticSprite = Target.GetComponent<StaticSprite2D>();
			if (staticSprite == null)
			{
				throw new NotSupportedException("The node should have StaticSprite2D");
			}
			FromOpacity = staticSprite.Alpha;
		}

		public override void Update (float time)
		{
			staticSprite.Alpha = FromOpacity + (ToOpacity - FromOpacity) * time;
		}
	}
}