#include "../AllUrho.h"
//
// This is a notification that posts the message to a general function, does
// not have to be an Urho object
//
class FuncNotification : public Urho3D::EventHandler {
typedef void (*HandlerFunctionPtr)(void *data, Urho3D::StringHash, Urho3D::VariantMap&);
	
public:
	FuncNotification (Urho3D::Object *receiver, HandlerFunctionPtr func, void *data) : EventHandler(receiver, data), theFunc(func){}

	virtual void Invoke(Urho3D::VariantMap& eventData)
	{
		(*theFunc)(userData_, eventType_, eventData);
	}

	virtual Urho3D::EventHandler* Clone() const
	{
		FuncNotification *x = new FuncNotification(receiver_, theFunc, userData_);
		x->SetSenderAndEventType (sender_, eventType_);
		return x;
	}

	
protected:
	HandlerFunctionPtr theFunc;
};

