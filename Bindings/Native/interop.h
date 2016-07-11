//
// Struct defines that we use to cast blittable classes,
// so that the C++ compiler will return a value that our P/Invoke
// layer can consume
//

#ifndef URHO_INTEROP_H
#define URHO_INTEROP_H

namespace Interop {
	struct IntVector2 {
		int a, b;
	};

	struct Vector2 {
		float x, y;
	};

	struct Vector3 {
		float x, y, z;
	};

	struct Vector4 {
		float x, y, z, w;
	};

	struct Quaternion {
		float w, x, y, z;
	};

	struct Color {
		float r, g, b, a;
	};
	
	struct IntRect {
		int left, top, right, bottom;
	};
			
	struct BoundingBox {
		Vector3 min, max;
		bool defined;
	};

	struct Plane {
		Vector3 normal, absNormal;
		float d;
	};

	struct Matrix3x4 {
		float m00;
		float m01;
		float m02;
		float m03;
		float m10;
		float m11;
		float m12;
		float m13;
		float m20;
		float m21;
		float m22;
		float m23;
	};

	struct Matrix4 {
		float m00;
		float m01;
		float m02;
		float m03;
		float m10;
		float m11;
		float m12;
		float m13;
		float m20;
		float m21;
		float m22;
		float m23;
		float m30;
		float m31;
		float m32;
		float m33;
	};
}
#endif
