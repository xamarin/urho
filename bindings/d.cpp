#define URHO3D_OPENGL

#include "AllUrho.h"

using namespace Urho3D;

struct Foo {
	float x, y;
};
	
Vector2 a (123,456);
Foo b;

extern "C" {
	Vector2 GetIt ()
	{
		return a;
	}
}
typedef Vector2 (*ptrfunc)();

int main ()
{
	ptrfunc func = (ptrfunc) &GetIt;
	Vector2 x = func ();
	Foo *y = (Foo *) &x;
	printf ("%g %g\n", x.x_, x.y_);
	printf ("%g %g\n", y->x, y->y);
		
	printf ("%lu == %lu\n", sizeof(Foo), sizeof(Urho3D::Vector2));
	return 1;
}
