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

	printf ("KEY_A = 0x%08x\n", KEY_A);
	printf ("KEY_B = 0x%08x\n", KEY_B);
	printf ("KEY_C = 0x%08x\n", KEY_C);
	printf ("KEY_D = 0x%08x\n", KEY_D);
	printf ("KEY_E = 0x%08x\n", KEY_E);
	printf ("KEY_F = 0x%08x\n", KEY_F);
	printf ("KEY_G = 0x%08x\n", KEY_G);
	printf ("KEY_H = 0x%08x\n", KEY_H);
	printf ("KEY_I = 0x%08x\n", KEY_I);
	printf ("KEY_J = 0x%08x\n", KEY_J);
	printf ("KEY_K = 0x%08x\n", KEY_K);
	printf ("KEY_L = 0x%08x\n", KEY_L);
	printf ("KEY_M = 0x%08x\n", KEY_M);
	printf ("KEY_N = 0x%08x\n", KEY_N);
	printf ("KEY_O = 0x%08x\n", KEY_O);
	printf ("KEY_P = 0x%08x\n", KEY_P);
	printf ("KEY_Q = 0x%08x\n", KEY_Q);
	printf ("KEY_R = 0x%08x\n", KEY_R);
	printf ("KEY_S = 0x%08x\n", KEY_S);
	printf ("KEY_T = 0x%08x\n", KEY_T);
	printf ("KEY_U = 0x%08x\n", KEY_U);
	printf ("KEY_V = 0x%08x\n", KEY_V);
	printf ("KEY_W = 0x%08x\n", KEY_W);
	printf ("KEY_X = 0x%08x\n", KEY_X);
	printf ("KEY_Y = 0x%08x\n", KEY_Y);
	printf ("KEY_Z = 0x%08x\n", KEY_Z);
	printf ("KEY_0 = 0x%08x\n", KEY_0);
	printf ("KEY_1 = 0x%08x\n", KEY_1);
	printf ("KEY_2 = 0x%08x\n", KEY_2);
	printf ("KEY_3 = 0x%08x\n", KEY_3);
	printf ("KEY_4 = 0x%08x\n", KEY_4);
	printf ("KEY_5 = 0x%08x\n", KEY_5);
	printf ("KEY_6 = 0x%08x\n", KEY_6);
	printf ("KEY_7 = 0x%08x\n", KEY_7);
	printf ("KEY_8 = 0x%08x\n", KEY_8);
	printf ("KEY_9 = 0x%08x\n", KEY_9);
	printf ("KEY_BACKSPACE = 0x%08x\n", KEY_BACKSPACE);
	printf ("KEY_TAB = 0x%08x\n", KEY_TAB);
	printf ("KEY_RETURN = 0x%08x\n", KEY_RETURN);
	printf ("KEY_RETURN2 = 0x%08x\n", KEY_RETURN2);
	printf ("KEY_KP_ENTER = 0x%08x\n", KEY_KP_ENTER);
	printf ("KEY_SHIFT = 0x%08x\n", KEY_SHIFT);
	printf ("KEY_CTRL = 0x%08x\n", KEY_CTRL);
	printf ("KEY_ALT = 0x%08x\n", KEY_ALT);
	printf ("KEY_GUI = 0x%08x\n", KEY_GUI);
	printf ("KEY_PAUSE = 0x%08x\n", KEY_PAUSE);
	printf ("KEY_CAPSLOCK = 0x%08x\n", KEY_CAPSLOCK);
	printf ("KEY_ESC = 0x%08x\n", KEY_ESC);
	printf ("KEY_SPACE = 0x%08x\n", KEY_SPACE);
	printf ("KEY_PAGEUP = 0x%08x\n", KEY_PAGEUP);
	printf ("KEY_PAGEDOWN = 0x%08x\n", KEY_PAGEDOWN);
	printf ("KEY_END = 0x%08x\n", KEY_END);
	printf ("KEY_HOME = 0x%08x\n", KEY_HOME);
	printf ("KEY_LEFT = 0x%08x\n", KEY_LEFT);
	printf ("KEY_UP = 0x%08x\n", KEY_UP);
	printf ("KEY_RIGHT = 0x%08x\n", KEY_RIGHT);
	printf ("KEY_DOWN = 0x%08x\n", KEY_DOWN);
	printf ("KEY_SELECT = 0x%08x\n", KEY_SELECT);
	printf ("KEY_PRINTSCREEN = 0x%08x\n", KEY_PRINTSCREEN);
	printf ("KEY_INSERT = 0x%08x\n", KEY_INSERT);
	printf ("KEY_DELETE = 0x%08x\n", KEY_DELETE);
	printf ("KEY_LGUI = 0x%08x\n", KEY_LGUI);
	printf ("KEY_RGUI = 0x%08x\n", KEY_RGUI);
	printf ("KEY_APPLICATION = 0x%08x\n", KEY_APPLICATION);
	printf ("KEY_KP_0 = 0x%08x\n", KEY_KP_0);
	printf ("KEY_KP_1 = 0x%08x\n", KEY_KP_1);
	printf ("KEY_KP_2 = 0x%08x\n", KEY_KP_2);
	printf ("KEY_KP_3 = 0x%08x\n", KEY_KP_3);
	printf ("KEY_KP_4 = 0x%08x\n", KEY_KP_4);
	printf ("KEY_KP_5 = 0x%08x\n", KEY_KP_5);
	printf ("KEY_KP_6 = 0x%08x\n", KEY_KP_6);
	printf ("KEY_KP_7 = 0x%08x\n", KEY_KP_7);
	printf ("KEY_KP_8 = 0x%08x\n", KEY_KP_8);
	printf ("KEY_KP_9 = 0x%08x\n", KEY_KP_9);
	printf ("KEY_KP_MULTIPLY = 0x%08x\n", KEY_KP_MULTIPLY);
	printf ("KEY_KP_PLUS = 0x%08x\n", KEY_KP_PLUS);
	printf ("KEY_KP_MINUS = 0x%08x\n", KEY_KP_MINUS);
	printf ("KEY_KP_PERIOD = 0x%08x\n", KEY_KP_PERIOD);
	printf ("KEY_KP_DIVIDE = 0x%08x\n", KEY_KP_DIVIDE);
	printf ("KEY_F1 = 0x%08x\n", KEY_F1);
	printf ("KEY_F2 = 0x%08x\n", KEY_F2);
	printf ("KEY_F3 = 0x%08x\n", KEY_F3);
	printf ("KEY_F4 = 0x%08x\n", KEY_F4);
	printf ("KEY_F5 = 0x%08x\n", KEY_F5);
	printf ("KEY_F6 = 0x%08x\n", KEY_F6);
	printf ("KEY_F7 = 0x%08x\n", KEY_F7);
	printf ("KEY_F8 = 0x%08x\n", KEY_F8);
	printf ("KEY_F9 = 0x%08x\n", KEY_F9);
	printf ("KEY_F10 = 0x%08x\n", KEY_F10);
	printf ("KEY_F11 = 0x%08x\n", KEY_F11);
	printf ("KEY_F12 = 0x%08x\n", KEY_F12);
	printf ("KEY_F13 = 0x%08x\n", KEY_F13);
	printf ("KEY_F14 = 0x%08x\n", KEY_F14);
	printf ("KEY_F15 = 0x%08x\n", KEY_F15);
	printf ("KEY_F16 = 0x%08x\n", KEY_F16);
	printf ("KEY_F17 = 0x%08x\n", KEY_F17);
	printf ("KEY_F18 = 0x%08x\n", KEY_F18);
	printf ("KEY_F19 = 0x%08x\n", KEY_F19);
	printf ("KEY_F20 = 0x%08x\n", KEY_F20);
	printf ("KEY_F21 = 0x%08x\n", KEY_F21);
	printf ("KEY_F22 = 0x%08x\n", KEY_F22);
	printf ("KEY_F23 = 0x%08x\n", KEY_F23);
	printf ("KEY_F24 = 0x%08x\n", KEY_F24);
	printf ("KEY_NUMLOCKCLEAR = 0x%08x\n", KEY_NUMLOCKCLEAR);
	printf ("KEY_SCROLLLOCK = 0x%08x\n", KEY_SCROLLLOCK);
	printf ("KEY_LSHIFT = 0x%08x\n", KEY_LSHIFT);
	printf ("KEY_RSHIFT = 0x%08x\n", KEY_RSHIFT);
	printf ("KEY_LCTRL = 0x%08x\n", KEY_LCTRL);
	printf ("KEY_RCTRL = 0x%08x\n", KEY_RCTRL);
	printf ("KEY_LALT = 0x%08x\n", KEY_LALT);
	printf ("KEY_RALT = 0x%08x\n", KEY_RALT);
	
	return 1;
}
