typedef void (*HandlerFunctionPtr)(void *data, int stringHash, void *variantMap /* Urho3D::VariantMap& */);

class NotificationProxy : public Urho3D::EventHandler {
public:
		NotificationProxy (Urho3D::Object *receiver, HandlerFunctionPtr func, void *data, Urho3D::StringHash evtHash)
		: Urho3D::EventHandler(receiver, data), theFunc(func), evtHash_(evtHash) {
	}

	virtual void Invoke(Urho3D::VariantMap& eventData)
	{
		void *p = &eventData;
		(*theFunc)(userData_, eventType_.Value (), p);
	}

	virtual Urho3D::EventHandler* Clone() const
	{
		NotificationProxy *x = new NotificationProxy(receiver_, theFunc, userData_, evtHash_);
		x->SetSenderAndEventType (sender_, eventType_);
		return x;
	}

	void Unsub ()
	{
		receiver_->UnsubscribeFromEvent (evtHash_);
		// This will in turn call delete on the NotificationProxy
	}
	
protected:
		HandlerFunctionPtr theFunc;
		Urho3D::StringHash evtHash_;
};
