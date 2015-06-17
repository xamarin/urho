typedef void (*HandlerFunctionPtr)(void *data, int stringHash, void *variantMap /* Urho3D::VariantMap& */);

class NotificationProxy : public Urho3D::EventHandler {
public:
        NotificationProxy (Urho3D::Object *receiver, HandlerFunctionPtr func, void *data) : Urho3D::EventHandler(receiver, data), theFunc(func){}

        virtual void Invoke(Urho3D::VariantMap& eventData)
        {
		void *p = &eventData;
                (*theFunc)(userData_, eventType_.Value (), p);
        }

        virtual Urho3D::EventHandler* Clone() const
        {
                NotificationProxy *x = new NotificationProxy(receiver_, theFunc, userData_);
                x->SetSenderAndEventType (sender_, eventType_);
                return x;
        }


protected:
        HandlerFunctionPtr theFunc;
};
