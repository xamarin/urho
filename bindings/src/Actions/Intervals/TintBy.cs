using System;
using Urho;
using Urho.Actions;
using Urho.Shapes;
using Urho.Urho2D;

namespace Urho.Actions
{

	public class TintBy : FiniteTimeAction
	{
		public float DeltaB { get; }
		public float DeltaG { get; }
		public float DeltaR { get; }

		#region Constructors

		public TintBy (float duration, float deltaRed, float deltaGreen, float deltaBlue) : base (duration)
		{
			DeltaR = deltaRed;
			DeltaG = deltaGreen;
			DeltaB = deltaBlue;
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new TintByState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new TintBy (Duration, DeltaR, DeltaG, DeltaB);
		}
	}


	public class TintByState : FiniteTimeActionState
	{
		StaticSprite2D staticSprite;
		Shape shape;

		protected float DeltaB { get; set; }

		protected float DeltaG { get; set; }

		protected float DeltaR { get; set; }

		protected float FromB { get; set; }

		protected float FromG { get; set; }

		protected float FromR { get; set; }

		public TintByState (TintBy action, Node target)
			: base (action, target)
		{   
			DeltaB = action.DeltaB;
			DeltaG = action.DeltaG;
			DeltaR = action.DeltaR;

			staticSprite = Target.GetComponent<StaticSprite2D>();
			if (staticSprite != null)
			{
				var color = staticSprite.Color;
				FromR = color.R;
				FromG = color.G;
				FromB = color.B;
				return;
			}

			shape = Target.GetComponent<Shape>();
			if (shape != null)
			{
				FromR = shape.Color.R;
				FromG = shape.Color.G;
				FromB = shape.Color.B;
				return;
			}

			throw new NotSupportedException("The node should have StaticSprite2D or Shape component");
		}

		public override void Update (float time)
		{
			var color = new Color(
				FromR + DeltaR * time,
				FromG + DeltaG * time,
				FromB + DeltaB * time);

			if (staticSprite != null)
				staticSprite.Color = color;

			if (shape != null)
				shape.Color = color;
		}
	}
}