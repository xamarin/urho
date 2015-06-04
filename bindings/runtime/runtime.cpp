
//
// This is a notification that posts the message to a general function, does
// not have to be an Urho object
//
class FuncNotification : public EventHandler {
	typedef void (*HandlerFunctionPtr)(void *data, StringHash, VariantMap&);
public:
	FuncNotification (Object *receiver, HandlerFunctionPtr func, void *data) : EventHandler(receiver, data), theFunc(func){}

	virtual void Invoke(VariantMap& eventData)
	{
		(*theFunc)(userData_, eventType_, eventData);
	}

	virtual EventHandler* Clone() const
	{
		FuncNotification *x = new FuncNotification(receiver_, theFunc, userData_);
		x->SetSenderAndEventType (sender_, eventType_);
		return x;
	}

	
protected:
	HandlerFunctionPtr theFunc;
};

