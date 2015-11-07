#include <Urho3D/Core/Context.h>
#include "SharpComponent.h"
#include "../AllUrho.h"

ComponentEventCallback componentEventCallback;
extern "C" DllExport
void SetComponentCallbacks(ComponentEventCallback callback)
{
	componentEventCallback = callback;
}

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
	ACCESSOR_ATTRIBUTE("ManagedState", Serialize, SetManagedState, String, String(""), AM_DEFAULT);
}

void SharpComponent::SetManagedState(const String& state)
{
	managedState = state;
}

const String& SharpComponent::GetManagedState() const
{
	return managedState;
}

const String& SharpComponent::Serialize() const
{
	if (componentEventCallback)
	{
		componentEventCallback((void*)this, SharpComponentEvent::Save);
	}
	return managedState;
}

const String& SharpComponent::GetName()
{
	return name;
}

void SharpComponent::ApplyAttributes()
{
	if (componentEventCallback)
	{
		componentEventCallback(this, SharpComponentEvent::Load);
	}
}