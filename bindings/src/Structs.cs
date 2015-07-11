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
		public int X, Y;
		public IntVector2 (int x, int y)
		{
			X = x;
			Y = y;
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Vector2 {
		public float X, Y;
		public Vector2 (float x, float y)
		{
			X = x;
			Y = y;
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Vector4 {
		public float X, Y, Z, W;
		public Vector4 (float x, float y, float z, float w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct IntRect {
		public int Left, Top, Right, Bottom;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct ResourceRef {
		public StringHash Type;
		public UrhoString Name;
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
		public Vector3 Min, Max;
		public bool Defined;

		public BoundingBox (float min, float max)
		{
			Min = new Vector3 (min, min, min);
			Max = new Vector3 (max, max, max);
			Defined = true;
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Quaternion {
		public float W, X, Y, Z;
		public Quaternion (float w, float x, float y, float z)
		{
			W = w;
			X = x;
			Y = y;
			Z = z;
		}

		// From Euler angles
		public Quaternion (float x, float y, float z)
		{
			const float M_DEGTORAD_2 = (float)Math.PI / 360.0f;
			x *= M_DEGTORAD_2;
			y *= M_DEGTORAD_2;
			z *= M_DEGTORAD_2;
			float sinX = (float)Math.Sin(x);
			float cosX = (float)Math.Cos(x);
			float sinY = (float)Math.Sin(y);
			float cosY = (float)Math.Cos(y);
			float sinZ = (float)Math.Sin(z);
			float cosZ = (float)Math.Cos(z);
			
			W = cosY * cosX * cosZ + sinY * sinX * sinZ;
			X = cosY * sinX * cosZ + sinY * cosX * sinZ;
			Y = sinY * cosX * cosZ - cosY * sinX * sinZ;
			Z = cosY * cosX * sinZ - sinY * sinX * cosZ;
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Matrix4 {
		public float m00;
		public float m01;
		public float m02;
		public float m03;
		public float m10;
		public float m11;
		public float m12;
		public float m13;
		public float m20;
		public float m21;
		public float m22;
		public float m23;
		public float m30;
		public float m31;
		public float m32;
		public float m33;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Matrix3x4 {
		public float m00;
		public float m01;
		public float m02;
		public float m03;
		public float m10;
		public float m11;
		public float m12;
		public float m13;
		public float m20;
		public float m21;
		public float m22;
		public float m23;
		
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Color {
		public float R, G, B, A;
		public Color ()
		{
			R = 1.0f;
			G = 1.0f;
			B = 1.0f;
			A = 1.0f;
		}

		public Color (float r, float g, float b, float a = 1f)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}

		public Color (Color source)
		{
			R = source.R;
			G = source.G;
			B = source.B;
			A = source.A;
		}

		public Color (Color source, float alpha)
		{
			R = source.R;
			G = source.G;
			B = source.B;
			A = alpha;
		}
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

	[StructLayout (LayoutKind.Sequential)]
	public struct WeakPtr {
		internal IntPtr ptr;
		internal IntPtr refCountPtr;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct TouchState {
		public int TouchID;
		public IntVector2 Position, LastPosition, Delta;
		public float Pressure;
		private WeakPtr _TouchedElement;

		[DllImport ("mono-urho")]
		extern static IntPtr TouchState_GetTouchedElement (ref TouchState state);
		
		public UIElement TouchedElement ()
		{
			if (_TouchedElement.ptr == IntPtr.Zero)
				return null;

			var x = TouchState_GetTouchedElement (ref this);
			if (x == IntPtr.Zero)
				return null;
			
			return Runtime.LookupObject<UIElement> (x);
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct ColorFrame {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct JoystickState {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct TextureFrame {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct LightBatchQueue {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Bone {
	}

	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct ReplicationState {
	}

	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct NodeReplicationState {
	}

	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct RenderPathCommand {
	}
	
	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct GPUObject {
	}
	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct GraphicsImpl {
	}
	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct FontGlyph {
	}
	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct RandomAccessIterator {
	}
	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct ModelMorph {
	}
	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct Octant {
	}
	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct CompressedLevel {
	}

	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct Billboard {
	}

	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct AnimationTrack {
	}

	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct CustomGeometryVertex {
	}

	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct NetworkState {
	}

	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct ComponentReplicationState {
	}

	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct ShaderParameter {
	}

	// DEBATABLE: maybe we should let the binder handle it?
	[StructLayout (LayoutKind.Sequential)]
	public struct UrhoString {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct PackageEntry {
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
