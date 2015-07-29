using System;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;

namespace Urho {
	[StructLayout (LayoutKind.Sequential)]
	public struct Ray {
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct IntRect {
		public int Left, Top, Right, Bottom;
		public IntRect (int left, int top, int right, int bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct Rect {
		public Vector2 Min, Max;
		public bool Defined;

		public Rect (int left, int top, int right, int bottom)
		{
			Min = new Vector2 (left, top);
			Max = new Vector2 (right, bottom);
			Defined = true;
		}
		
		public Rect (Vector2 min, Vector2 max)
		{
			Min = min;
			Max = max;
			Defined = true;
		}
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

		public BoundingBox (Vector3 min, Vector3 max)
		{
			Min = min;
			Max = max;
			Defined = true;
		}
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

		public Color (float r = 1f, float g = 1f, float b = 1f, float a = 1f)
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

		public static Color White = new Color (1, 1, 1);
		public static Color Gray = new Color (0.5f, 0.5f, 0.5f);
		public static Color Black = new Color (0.0f, 0.0f, 0.0f);
		public static Color Red = new Color (1.0f, 0.0f, 0.0f);
		public static Color Green = new Color (0.0f, 1.0f, 0.0f);
		public static Color Blue = new Color (0.0f, 0.0f, 1.0f);
		public static Color Cyan = new Color (0.0f, 1.0f, 1.0f);
		public static Color Magenta = new Color (1.0f, 0.0f, 1.0f);
		public static Color Yellow = new Color (1.0f, 1.0f, 0.0f);
		public static Color Transparent = new Color (0.0f, 0.0f, 0.0f, 0.0f);
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
		public UrhoString Name;
		public int NameHash;
		public uint ParentIndex;
		public Vector3 InitialPosition;
		public Quaternion InitialRotation;
		public Vector3 InitialScale;
		public Matrix3x4 OffsetMatrix;
		public bool Animated;
		public char CollisionMask;
		public float Radius;
		public BoundingBox BoundingBox;
		private WeakPtr Node;
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
		public Vector3 Position;
		public Vector2 Size;
		public Rect UV;
		public Color Color;
		public float Rotation;
		public bool Enabled;
		public float SortDistance;
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

	[StructLayout (LayoutKind.Sequential)]
	public struct BiasParameters {
		public float ConstantBias;
		public float SlopeScaleBias;

		public BiasParameters (float constantBias, float slopeScaleBias)
		{
			ConstantBias = constantBias;
			SlopeScaleBias = slopeScaleBias;
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct CascadeParameters {
		public float Split1, Split2, Split3, Split4;
		public float FadeStart;
		public float BiasAutoAdjust;

		public CascadeParameters (float split1, float split2, float split3, float split4, float fadeStart, float biasAutoAdjust = 1f)
		{
			Split1 = split1;
			Split2 = split2;
			Split3 = split3;
			Split4 = split4;
			FadeStart = fadeStart;
			BiasAutoAdjust = biasAutoAdjust;
		}
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct FocusParameters {
		public bool Focus;
		public bool NonUniform;
		public bool AutoSize;
		public float Quantize;
		public float MinView;
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
