using System;

namespace Urho
{
	public class TintTo : FiniteTimeAction
	{
		public Color ColorTo { get; }

		#region Constructors

		public TintTo (float duration, float red, float green, float blue) : base (duration)
		{
			ColorTo = new Color(red, green, blue);
		}

		#endregion Constructors

		public override FiniteTimeAction Reverse()
		{
			throw new System.NotImplementedException ();
		}

		protected internal override ActionState StartAction(Node target)
		{
			return new TintToState (this, target);
		}
	}

	public class TintToState : FiniteTimeActionState
	{
		StaticSprite2D staticSprite;

		protected Color ColorFrom { get; set; }

		protected Color ColorTo { get; set; }

		public TintToState (TintTo action, Node target)
			: base (action, target)
		{   
			ColorTo = action.ColorTo;
			staticSprite = Target.GetComponent<StaticSprite2D>();
			if (staticSprite == null)
			{
				throw new NotSupportedException("The node should have StaticSprite2D");
			}
			ColorFrom = staticSprite.Color;
		}

		public override void Update (float time)
		{
			staticSprite.Color = new Color(ColorFrom.R + (ColorTo.R - ColorFrom.R) * time,
				ColorFrom.G + (ColorTo.G - ColorFrom.G) * time,
				ColorFrom.B + (ColorTo.B - ColorFrom.B) * time);
		}
	}
}