using System;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;

namespace Urho {
	[StructLayout (LayoutKind.Sequential)]
	public struct Ray {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct IntVector2 {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Vector2 {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Vector3 {
	}
	
	[StructLayout (LayoutKind.Sequential)]
	public struct Vector4 {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct IntRect {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct ResourceRef {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct HashIteratorBase {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Iterator {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct ResourceRefList {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct BoundingBox {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Quaternion {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Matrix4 {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Matrix3x4 {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Color {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Frustum {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Variant {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct XPathResultSet {
	}
}

namespace System {
	//
	// Hacks until I get Sharpie to not mess with my types
	//
	[StructLayout (LayoutKind.Sequential)]
	public struct nuint {
		UIntPtr x;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct nint {
		IntPtr x;
	}

}
