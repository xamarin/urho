using System.Text;

namespace Urho
{
	public class ApplicationOptions
	{
		// see http://urho3d.github.io/documentation/1.4/_running.html (Command line options)

		public int Width { get; set; } = 0;
		public int Height { get; set; } = 0;
		public bool WindowedMode { get; set; } = true;
		public bool ResizableWindow { get; set; } = false;
		public bool LimitFps { get; set; } = true;
		public OrientationType Orientation { get; set; } = OrientationType.Landscape;

		public enum OrientationType
		{
			Landscape,
			Portrait
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("args");//it will be skipped by Urho;

			if (WindowedMode)
				builder.Append(" -w");

			if (!LimitFps)
				builder.Append(" -nolimit");

			if (Width > 0)
				builder.AppendFormat(" -x {0}", Width);

			if (Height > 0)
				builder.AppendFormat(" -y {0}", Height);

			if (ResizableWindow)
				builder.Append("-s");

			builder.AppendFormat(" -{0}", Orientation.ToString().ToLower());

			return builder.ToString();
		}
	}
}
