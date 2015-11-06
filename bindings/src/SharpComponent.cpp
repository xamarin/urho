#include <Urho3D/Core/Context.h>
#include "SharpComponent.h"

SharpComponent::SharpComponent(Context* context) : Component(context)
{
}

SharpComponent::SharpComponent(const String& typeName, Context* context) : Component(context)
{
	name = typeName;
	RegisterObject(context);
}

void SharpComponent::RegisterObject(Context* context)
{
	context->RegisterFactory<SharpComponent>();
	ATTRIBUTE("SharpName", String, name, String(""), AM_DEFAULT);
	ATTRIBUTE("ManagedState", String, managedState, String(""), AM_DEFAULT);
}

void SharpComponent::SetManagedState(const String& state)
{
	managedState = state;
}

const String& SharpComponent::GetManagedState()
{
	return managedState;
}

const String& SharpComponent::GetName()
{
	return name;
}

void SharpComponent::ApplyAttributes()
{
}
