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
}
