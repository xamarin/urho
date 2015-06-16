#include <type_traits>

void urho_binding_asserts ()
{
	static_assert (sizeof (Urho3D::StringHash)  == 4, "The size should be 4");
	static_assert (sizeof (Urho3D::Vector2)  == 8, "Vector2 should have two floats");
}

