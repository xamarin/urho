using System;
using Urho.Actions;

namespace Urho.Actions
{
	public class ShaderParameterFromTo<TShaderParamType> : FiniteTimeAction
	{
		public string Parameter { get; set; }
		public TShaderParamType ToValue { get; set; }
		public TShaderParamType FromValue { get; set; }
		public Material Material { get; set; }
		public Action<string, TShaderParamType, TShaderParamType, float, Material> ValueAction { get; set; }

		public ShaderParameterFromTo(string parameter, 
			TShaderParamType fromValue, 
			TShaderParamType toValue,
			Action<string, TShaderParamType, TShaderParamType, float, Material> valueAction, // if only generics would support '+'/'-' constraints...
			float duration, Material material = null) : base(duration)
		{
			ValueAction = valueAction;
			Parameter = parameter;
			FromValue = fromValue;
			ToValue = toValue;
			Material = material;
		}

		protected internal override ActionState StartAction(Node target)
		{
			return new ShaderParameterFromToState<TShaderParamType>(this, target);
		}

		public override FiniteTimeAction Reverse()
		{
			return new ShaderParameterFromTo<TShaderParamType>(Parameter, ToValue, FromValue, ValueAction, Duration, Material);
		}
	}

	public class ShaderParameterFloatFromTo : ShaderParameterFromTo<float>
	{
		public ShaderParameterFloatFromTo(string parameter, float fromValue, float toValue, float duration, Material material = null)
			: base(parameter, fromValue, toValue, ValueAction, duration, material) {}

		static void ValueAction(string parameter, float from, float to, float duration, Material material) => 
			material.SetShaderParameter(parameter, from + (to - from) * duration);
	}

	public class ShaderParameterVector2FromTo : ShaderParameterFromTo<Vector2>
	{
		public ShaderParameterVector2FromTo(string parameter, Vector2 fromValue, Vector2 toValue, float duration, Material material = null)
			: base(parameter, fromValue, toValue, ValueAction, duration, material) { }

		static void ValueAction(string parameter, Vector2 from, Vector2 to, float duration, Material material) =>
			material.SetShaderParameter(parameter, from + (to - from) * duration);
	}

	public class ShaderParameterVector3FromTo : ShaderParameterFromTo<Vector3>
	{
		public ShaderParameterVector3FromTo(string parameter, Vector3 fromValue, Vector3 toValue, float duration, Material material = null)
			: base(parameter, fromValue, toValue, ValueAction, duration, material) { }

		static void ValueAction(string parameter, Vector3 from, Vector3 to, float duration, Material material) =>
			material.SetShaderParameter(parameter, from + (to - from) * duration);
	}

	public class ShaderParameterVector4FromTo : ShaderParameterFromTo<Vector4>
	{
		public ShaderParameterVector4FromTo(string parameter, Vector4 fromValue, Vector4 toValue, float duration, Material material = null)
			: base(parameter, fromValue, toValue, ValueAction, duration, material) { }

		static void ValueAction(string parameter, Vector4 from, Vector4 to, float duration, Material material) =>
			material.SetShaderParameter(parameter, from + (to - from) * duration);
	}

	public class ShaderParameterColorFromTo : ShaderParameterFromTo<Color>
	{
		public ShaderParameterColorFromTo(string parameter, Color fromValue, Color toValue, float duration, Material material = null)
			: base(parameter, fromValue, toValue, ValueAction, duration, material) { }

		static void ValueAction(string parameter, Color from, Color to, float duration, Material material) =>
			material.SetShaderParameter(parameter, from + (to - from) * duration);
	}

	public class ShaderParameterFromToState<TShaderParamType> : FiniteTimeActionState
	{
		protected string ParameterName { get; set; }
		protected TShaderParamType FromValue { get; set; }
		protected TShaderParamType ToValue { get; set; }
		protected Material Material { get; set; }
		public Action<string, TShaderParamType, TShaderParamType, float, Material> ValueAction { get; set; }

		public ShaderParameterFromToState(ShaderParameterFromTo<TShaderParamType> action, Node target)
			: base(action, target)
		{
			ParameterName = action.Parameter;
			ToValue = action.ToValue;
			FromValue = action.FromValue;
			ValueAction = action.ValueAction;
			Material = action.Material ?? target.GetComponent<StaticModel>()?.GetMaterial(0);
			if (Material == null)
				throw new InvalidOperationException("Material is not specified or the Node doesn't have any StaticMaterial with a Material.");
		}

		public override void Update(float time)
		{
			ValueAction(ParameterName, FromValue, ToValue, time, Material);
		}
	}
}
