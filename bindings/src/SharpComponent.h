#pragma once

#include <Urho3D/Scene/Component.h>

using namespace Urho3D;

class SharpComponent : public Component
{
public:
	OBJECT(SharpComponent);

	SharpComponent(Context* context);
	SharpComponent(const String& typeName, Context* context);

	void RegisterObject(Context* context);

	void SetManagedState(const String& state);
	const String& GetManagedState();
	const String& GetName();

	virtual void ApplyAttributes();

private:
	String name;
	String managedState;
};

