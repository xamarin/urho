using System;
using Urho;
using Urho.Shapes;
using Urho.Urho2D;

namespace Urho.Actions
{
	public class TintTo : FiniteTimeAction
	{
		public Color ColorTo { get; }

		// Optional parameters for Material:
		public string ShaderParameterName { get; set; } = CoreAssets.ShaderParameters.MatDiffColor;
		public int MaterialIndex { get; set; } = 0;

		#region Constructors

		public TintTo (float duration, float red, float green, float blue, float alpha = 1) : base (duration)
		{
			ColorTo = new Color(red, green, blue, alpha);
		}

		public TintTo(float duration, Color colorTo) 
			: this(duration, colorTo.R, colorTo.G, colorTo.B, colorTo.A) { }

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
		Material material;

		protected Color ColorFrom { get; set; }
		protected Color ColorTo { get; set; }
		protected string ShaderParameterName { get; set; }
		protected int MaterialIndex { get; set; }

		public TintToState (TintTo action, Node target)
			: base (action, target)
		{
			ColorTo = action.ColorTo;
			ShaderParameterName = action.ShaderParameterName;
			MaterialIndex = action.MaterialIndex;
			staticSprite = Target.GetComponent<StaticSprite2D>();
			if (staticSprite != null)
			{
				ColorFrom = staticSprite.Color;
				return;
			}

			var staticModel = Target.GetComponent<StaticModel>();
			if (staticModel != null)
			{
				material = staticModel.GetMaterial(0);
				if (material != null)
				{
					ColorFrom = material.GetShaderParameter(ShaderParameterName);
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
				ColorFrom.R + (ColorTo.R - ColorFrom.R) * time,
				ColorFrom.G + (ColorTo.G - ColorFrom.G) * time,
				ColorFrom.B + (ColorTo.B - ColorFrom.B) * time,
				MathHelper.Clamp(ColorFrom.A + (ColorTo.A - ColorFrom.A) * time, 0, 1));

			if (staticSprite != null)
				staticSprite.Color = color;

			if (material != null)
				material.SetShaderParameter(ShaderParameterName, color);
		}
	}
}