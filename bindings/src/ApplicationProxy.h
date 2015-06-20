class ApplicationProxy;

typedef void(*callback_t)(ApplicationProxy *);

class ApplicationProxy : public Urho3D::Application {
public:
	ApplicationProxy (Urho3D::Context *ctx, callback_t cstart, callback_t csetup, callback_t cstop) : Urho3D::Application (ctx)
	{
		setup = csetup;
		start = cstart;
		stop = cstop;
	}

	void Setup ()
	{
		engineParameters_["FullScreen"]  = false;
		engineParameters_["Headless"]    = false;
		
		setup (this);
	}

	void Start ()
	{
		start (this);
	}

	void Stop ()
	{
		stop (this);
	}
	
private:
	callback_t setup, start, stop;
};

extern "C" {
void *
ApplicationProxy_ApplicationProxy (Urho3D::Context *context, callback_t setup, callback_t start, callback_t stop);

}
