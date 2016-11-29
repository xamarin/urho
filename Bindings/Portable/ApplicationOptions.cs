using System;
using System.Linq;
using System.Text;

namespace Urho
{
	/// <summary>
	/// Application options, see full description at:
	/// http://urho3d.github.io/documentation/1.5/_running.html 
	/// </summary>
	public class ApplicationOptions
	{
		/// <param name="assetsFolder">usually it's "Data". Can be null if built-in assets are enough for you</param>
		public ApplicationOptions(string assetsFolder)
		{
			if (assetsFolder != null)
			{
				ResourcePaths = new[] { assetsFolder };
			}
		}

		public ApplicationOptions() {}

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

#if XFORMS
		/// <summary>
		/// iOS only
		/// </summary>
		public OrientationType Orientation { get; set; } = OrientationType.LandscapeAndPortrait;
#else
		/// <summary>
		/// iOS only
		/// </summary>
		public OrientationType Orientation { get; set; } = OrientationType.Landscape;
#endif
		/// <summary>
		/// Resource path(s) to use (default: Data, CoreData)
		/// </summary>
		public string[] ResourcePaths { get; set; } = null;

		/// <summary>
		/// Resource package files to use (default: empty)
		/// </summary>
		public string[] ResourcePackagesPaths { get; set; } = null;

#if WINDOWS_UWP
		public bool TouchEmulation { get { return true; } set {} }
#else
		/// <summary>
		/// Touch emulation on desktop platform
		/// </summary>
		public bool TouchEmulation { get; set; } = false;
#endif

		/// <summary>
		/// Enable high DPI, only supported by Apple platforms (OSX, iOS, and tvOS)
		/// </summary>
		public bool HighDpi { get; set; } = true;

		/// <summary>
		/// Add any flag listed here: http://urho3d.github.io/documentation/1.5/_running.html 
		/// </summary>
		public string AdditionalFlags { get; set; } = string.Empty;

		/// <summary>
		/// Windows: external window handle (WinForms Panel.Handle) to use in order to display Urho game
		/// You can use it in WPF via WindowsFormsHost (and a WF panel inside it)
		/// </summary>
		public IntPtr ExternalWindow { get; set; }

		public bool DelayedStart { get; set; } = false;

		public bool AutoloadCoreData { get; set; } = true;

		public string[] ResourcePrefixPaths { get; set; }

		public enum OrientationType
		{
			Landscape,
			Portrait,
			LandscapeAndPortrait
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("args");//it will be skipped by Urho;

#if !IOS //always use -w on iOS
			if (WindowedMode)
#endif
				builder.Append(" -w");

			if (!LimitFps)
				builder.Append(" -nolimit");

			if (DelayedStart)
				builder.Append(" -delayedstart");

			if (Width > 0)
				builder.AppendFormat(" -x {0}", Width);

			if (Height > 0)
				builder.AppendFormat(" -y {0}", Height);

#if !IOS //always use -s on iOS and UWP
			if (ResizableWindow)
#endif
				builder.Append(" -s");

			string[] resourcePathes = new[] { "CoreData" }.Concat(ResourcePaths ?? new string[0]).ToArray();
			if (!AutoloadCoreData)
				resourcePathes = ResourcePaths ?? new string[0];
			builder.AppendFormat(" -p \"{0}\"", string.Join(";", resourcePathes.Distinct()));

			if (ResourcePackagesPaths?.Length > 0)
				builder.AppendFormat(" -pf \"{0}\"", string.Join(";", ResourcePackagesPaths));

			if (ResourcePrefixPaths?.Length > 0)
				builder.AppendFormat(" -pp \"{0}\"", string.Join(";", ResourcePrefixPaths));

#if !WINDOWS_UWP
			if (TouchEmulation)
#endif
			builder.Append(" -touch");

			if (HighDpi)
				builder.Append(" -hd");

			switch (Orientation)
			{
				case OrientationType.Landscape:
					builder.Append(" -landscape");
					break;
				case OrientationType.Portrait:
					builder.Append(" -portrait");
					break;
				case OrientationType.LandscapeAndPortrait:
					builder.Append(" -landscape -portrait");
					break;
			}

			return builder + " " + AdditionalFlags;
		}
	}
}
