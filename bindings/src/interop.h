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
}
#endif
