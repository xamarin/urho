using System;
using Urho;
using Urho.Actions;
using Urho.Shapes;
using Urho.Urho2D;

namespace Urho.Actions
{

	public class TintBy : FiniteTimeAction
	{
		public float DeltaR { get; }
		public float DeltaG { get; }
		public float DeltaB { get; }
		public float DeltaA { get; }

		// Optional parameters for Material:
		public string ShaderParameterName { get; set; } = CoreAssets.ShaderParameters.MatDiffColor;
		public int MaterialIndex { get; set; } = 0;

		#region Constructors

		public TintBy (float duration, float deltaRed, float deltaGreen, float deltaBlue, float deltaAlpha = 0) : base (duration)
		{
			DeltaR = deltaRed;
			DeltaG = deltaGreen;
			DeltaB = deltaBlue;
			DeltaA = deltaAlpha;
		}

		public TintBy(float duration, Color deltaColor) 
			: this(duration, deltaColor.R, deltaColor.G, deltaColor.B, deltaColor.A) { }

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new TintByState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new TintBy (Duration, -DeltaR, -DeltaG, -DeltaB, -DeltaA)
				{
					ShaderParameterName = ShaderParameterName,
					MaterialIndex = MaterialIndex
				};
		}
	}


	public class TintByState : FiniteTimeActionState
	{
		StaticSprite2D staticSprite;
		Material material;

		protected float DeltaB { get; set; }
		protected float DeltaG { get; set; }
		protected float DeltaR { get; set; }
		protected float DeltaA { get; set; }
		protected float FromB { get; set; }
		protected float FromG { get; set; }
		protected float FromR { get; set; }
		protected float FromA { get; set; }
		protected string ShaderParameterName { get; set; }
		protected int MaterialIndex { get; set; }

		public TintByState (TintBy action, Node target)
			: base (action, target)
		{
			DeltaB = action.DeltaB;
			DeltaG = action.DeltaG;
			DeltaR = action.DeltaR;
			DeltaA = action.DeltaA;
			ShaderParameterName = action.ShaderParameterName;
			MaterialIndex = action.MaterialIndex;

			staticSprite = Target.GetComponent<StaticSprite2D>();
			if (staticSprite != null)
			{
				var color = staticSprite.Color;
				FromR = color.R;
				FromG = color.G;
				FromB = color.B;
				FromA = color.A;
				return;
			}

			var staticModel = Target.GetComponent<StaticModel>();
			if (staticModel != null)
			{
				material = staticModel.GetMaterial(0);
				if (material != null)
				{
					var color = material.GetShaderParameter(ShaderParameterName);
					FromR = color.R;
					FromG = color.G;
					FromB = color.B;
					FromA = color.A;
					return;
				}
				else
				{
					throw new NotSupportedException("StaticModel.Material should not be empty.");
				}
			}

			throw new NotSupportedException("The node should have StaticSprite2D or StaticModel+Material component");
		}

		public override void Update (float time)
		{
			var color = new Color(
				FromR + DeltaR * time,
				FromG + DeltaG * time,
				FromB + DeltaB * time,
				MathHelper.Clamp(FromA + DeltaA * time, 0, 1));

			if (staticSprite != null)
				staticSprite.Color = color;

			if (material != null)
				material.SetShaderParameter(ShaderParameterName, color);
		}
	}
}