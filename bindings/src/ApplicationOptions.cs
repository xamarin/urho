using System.Text;

namespace Urho
{
	/// <summary>
	/// Application options, see full description at:
	/// http://urho3d.github.io/documentation/1.4/_running.html 
	/// </summary>
	public class ApplicationOptions
	{
		/// <summary>
		/// Desktop only
		/// </summary>
		public int Width { get; set; } = 0;

		/// <summary>
		/// Desktop only
		/// </summary>
		public int Height { get; set; } = 0;
		
		/// <summary>
		/// Desktop only
		/// </summary>
		public bool WindowedMode { get; set; } = true;

		/// <summary>
		/// Desktop only
		/// </summary>
		public bool ResizableWindow { get; set; } = false;

		/// <summary>
		/// With limit enabled: 200 fps for Desktop (and always 60 fps for mobile despite of the flag)
		/// </summary>
		public bool LimitFps { get; set; } = true;

		/// <summary>
		/// iOS only
		/// </summary>
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
				builder.Append(" -s");

			builder.AppendFormat(" -{0}", Orientation.ToString().ToLower());

			return builder.ToString();
		}

		// Some predefined:

		public static ApplicationOptions Default { get; } = new ApplicationOptions();

		public static ApplicationOptions PortraitDefault { get; } = new ApplicationOptions { Height = 800, Width = 500, Orientation = OrientationType.Portrait };
	}
}
