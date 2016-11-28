using System;

namespace Urho {
	[Flags]
	public enum ViewOverrideFlags {
		None = 0,
		LowMaterialQuality = 1,
		DisableShadows = 2,
		DisableOcclusion = 4
	}

	public enum LogLevel {
		Raw = -1,
		Debug = 0,
		Info = 1,
		Warning = 2,
		Error = 3,
		None = 4
	}

	[Flags]
	public enum ElementMask : uint {
		None = 0x0,
		Position = 0x1,
		Normal = 0x2,
		Color = 0x4,
		TexCoord1 = 0x8,
		TexCoord2 = 0x10,
		CubeTexCoord1 = 0x20,
		CubeTexCoord2 = 0x40,
		Tangent = 0x80,
		BlendWeights = 0x100,
		BlendIndices = 0x200,
		InstanceMatrix1 = 0x400,
		InstanceMatrix2 = 0x800,
		InstanceMatrix3 = 0x1000,
		Default = 0xffffffff,
	}

	public enum SoundType {
		Master,
		Effect,
		Ambient,
		Voice,
		Music
	}

	public enum DrawableFlags : uint {
		Geometry = 0x1,
		Light = 0x2,
		Zone = 0x4,
		Geometry2D = 0x8,
		Any = 0xff,
	}

	public enum Platforms {
		Unknown,
		Android,
		iOS,
		Windows,
		MacOSX,
		Linux, 
		UWP,
		HoloLens
	}

	internal static class PlatformsMap
	{
		public static Platforms FromString(string str)
		{
			switch (str)
			{
				// ProcessUtils.cpp:L349
				case "Android": return Platforms.Android;
				case "iOS": return Platforms.iOS;
				case "Windows": return Platforms.Windows;
				case "Mac OS X": return Platforms.MacOSX;
				case "Linux": return Platforms.Linux;
			}
#if UWP_HOLO
			return Platforms.HoloLens;
#elif WINDOWS_UWP
			return Platforms.UWP;
#endif
			return Platforms.Unknown;
		}
	}

	internal enum CallbackType
	{
		Component_OnSceneSet,
		Component_SaveXml,
		Component_LoadXml,
		Component_AttachedToNode,
		Component_OnNodeSetEnabled,

		RefCounted_AddRef,
		RefCounted_Delete,

		Log_Write,
	};
}
