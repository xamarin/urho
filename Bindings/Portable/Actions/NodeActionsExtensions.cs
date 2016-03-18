using System.Threading.Tasks;

namespace Urho.Actions
{
	public static class NodeActionsExtensions
	{
		public static Task MoveBy(this Node node, float duration, Vector3 position)
		{
			return node.RunActionsAsync(new MoveBy(duration, position));
		}

		public static Task MoveTo(this Node node, float duration, Vector3 position)
		{
			return node.RunActionsAsync(new MoveTo(duration, position));
		}

		public static Task ScaleBy(this Node node, float duration, float scale)
		{
			return node.RunActionsAsync(new ScaleBy(duration, scale));
		}

		public static Task ScaleBy(this Node node, float duration, float scaleX, float scaleY, float scaleZ)
		{
			return node.RunActionsAsync(new ScaleBy(duration, scaleX, scaleY, scaleZ));
		}

		public static Task ScaleTo(this Node node, float duration, float scale)
		{
			return node.RunActionsAsync(new ScaleTo(duration, scale));
		}

		public static Task ScaleTo(this Node node, float duration, float scaleX, float scaleY, float scaleZ)
		{
			return node.RunActionsAsync(new ScaleTo(duration, scaleX, scaleY, scaleZ));
		}

		public static Task TintTo(this Node node, float duration, float red, float green, float blue)
		{
			return node.RunActionsAsync(new TintTo(duration, red, green, blue));
		}

		public static Task TintBy(this Node node, float duration, float red, float green, float blue)
		{
			return node.RunActionsAsync(new TintBy(duration, red, green, blue));
		}

		public static Task RotateTo(this Node node, float duration, float deltaAngle)
		{
			return node.RunActionsAsync(new RotateTo(duration, deltaAngle));
		}

		public static Task RotateTo(this Node node, float duration, float deltaAngleX, float deltaAngleY, float deltaAngleZ)
		{
			return node.RunActionsAsync(new RotateTo(duration, deltaAngleX, deltaAngleY, deltaAngleZ));
		}

		public static Task RotateBy(this Node node, float duration, float deltaAngle)
		{
			return node.RunActionsAsync(new RotateBy(duration, deltaAngle));
		}

		public static Task RotateBy(this Node node, float duration, float deltaAngleX, float deltaAngleY, float deltaAngleZ)
		{
			return node.RunActionsAsync(new RotateBy(duration, deltaAngleX, deltaAngleY, deltaAngleZ));
		}

		public static Task BezierBy(this Node node, float duration, BezierConfig config)
		{
			return node.RunActionsAsync(new BezierBy(duration, config));
		}

		public static Task BezierTo(this Node node, float duration, BezierConfig config)
		{
			return node.RunActionsAsync(new BezierTo(duration, config));
		}
	}
}
