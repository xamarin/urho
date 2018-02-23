#include "UrhoUWP.h"

void __cdecl WINRT_InitGameBar(struct SDL_VideoDevice *p){};
void __cdecl WINRT_QuitGameBar(struct SDL_VideoDevice *p){};

SwapChainPanel^ FindSwapPanel(DependencyObject^ parent)
{
	SwapChainPanel^ panel = dynamic_cast<SwapChainPanel^>(parent);
	if (panel != nullptr)
	{
		return panel;
	}
	int count = VisualTreeHelper::GetChildrenCount(parent);
	for (int i = 0; i < count; i++)
	{
		DependencyObject^ child = VisualTreeHelper::GetChild(parent, i);
		SwapChainPanel^ panel = FindSwapPanel(child);
		if (panel != nullptr)
			return panel;
	}
	return nullptr;
}

void SetSwapChainPanel(int width, int height, ID3D11Device** d3d_device, IDXGISwapChain** d3d_swapchain, ID3D11DeviceContext** d3d_context)
{
	UINT creationFlags = D3D11_CREATE_DEVICE_BGRA_SUPPORT;
	D3D_FEATURE_LEVEL featureLevels[] =
	{
		D3D_FEATURE_LEVEL_11_1,
		D3D_FEATURE_LEVEL_11_0,
		D3D_FEATURE_LEVEL_10_1,
		D3D_FEATURE_LEVEL_10_0,
		D3D_FEATURE_LEVEL_9_3,
		D3D_FEATURE_LEVEL_9_2,
		D3D_FEATURE_LEVEL_9_1
	};


	ComPtr<ID3D11DeviceContext> context;
	ComPtr<ID3D11Device> device;
	HRESULT hr = D3D11CreateDevice(
		nullptr,					// Specify nullptr to use the default adapter.
		D3D_DRIVER_TYPE_HARDWARE,	// Create a device using the hardware graphics driver.
		0,							// Should be 0 unless the driver is D3D_DRIVER_TYPE_SOFTWARE.
		creationFlags,				// Set debug and Direct2D compatibility flags.
		featureLevels,				// List of feature levels this app can support.
		ARRAYSIZE(featureLevels),	// Size of the list above.
		D3D11_SDK_VERSION,			// Always set this to D3D11_SDK_VERSION for Windows Store apps.
		&device,					// Returns the Direct3D device created.
		0,			// Returns feature level of device created.
		&context					// Returns the device immediate context.
	);

	*d3d_device = device.Get();
	*d3d_context = context.Get();

	ComPtr<ID3D11Device3> d3dDevice;
	ComPtr<ID3D11DeviceContext3> d3dContext;
	device.As(&d3dDevice);
	context.As(&d3dContext);

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	ID3D11RenderTargetView* nullViews[] = { nullptr };
	d3dContext->OMSetRenderTargets(ARRAYSIZE(nullViews), nullViews, nullptr);
	d3dContext->Flush1(D3D11_CONTEXT_TYPE_ALL, nullptr);

	//  HRESULT hr = dxgiSwapChain->ResizeBuffers(2, 1024, 768, DXGI_FORMAT_B8G8R8A8_UNORM, 0);
	{
		DXGI_SCALING scaling = DXGI_SCALING_STRETCH;
		DXGI_SWAP_CHAIN_DESC1 swapChainDesc = { 0 };

		swapChainDesc.Width = lround(width);		// Match the size of the window.
		swapChainDesc.Height = lround(height);
		swapChainDesc.Format = DXGI_FORMAT_B8G8R8A8_UNORM;				// This is the most common swap chain format.
		swapChainDesc.Stereo = false;
		swapChainDesc.SampleDesc.Count = 1;								// Don't use multi-sampling.
		swapChainDesc.SampleDesc.Quality = 0;
		swapChainDesc.BufferUsage = DXGI_USAGE_RENDER_TARGET_OUTPUT;
		swapChainDesc.BufferCount = 2;									// Use double-buffering to minimize latency.
		swapChainDesc.SwapEffect = DXGI_SWAP_EFFECT_FLIP_SEQUENTIAL;	// All Windows Store apps must use _FLIP_ SwapEffects.
		swapChainDesc.Flags = 0;
		swapChainDesc.Scaling = scaling;
		swapChainDesc.AlphaMode = DXGI_ALPHA_MODE_IGNORE;

		ComPtr<IDXGIDevice3> dxgiDevice;
		d3dDevice.As(&dxgiDevice);

		ComPtr<IDXGIAdapter> dxgiAdapter;
		dxgiDevice->GetAdapter(&dxgiAdapter);

		ComPtr<IDXGIFactory4> dxgiFactory;
		dxgiAdapter->GetParent(IID_PPV_ARGS(&dxgiFactory));

		ComPtr<IDXGISwapChain1> swapChain;
		dxgiFactory->CreateSwapChainForComposition(
			d3dDevice.Get(),
			&swapChainDesc,
			nullptr,
			&swapChain);

		*d3d_swapchain = swapChain.Get();

		ComPtr<IDXGISwapChain3> dxgiSwapChain;
		swapChain.As(&dxgiSwapChain);

		ComPtr<ISwapChainPanelNative> panelNative;
		reinterpret_cast<IUnknown*>(FindSwapPanel(Window::Current->Content))->QueryInterface(IID_PPV_ARGS(&panelNative));
		panelNative->SetSwapChain(dxgiSwapChain.Get());
		//dxgiDevice->SetMaximumFrameLatency(1);
	}
}

extern "C"
{
	char *getenvCompat(const char *name)
	{
		return NULL;
	}

	const wchar_t* SDL_UWP_GetResourceDir()
	{
		return Windows::ApplicationModel::Package::Current->InstalledLocation->Path->Data();
	}

	const wchar_t* SDL_UWP_GetCacheDir()
	{
		return Windows::Storage::ApplicationData::Current->LocalFolder->Path->Data();
	}

	int SDL_UWP_MoveFile(const wchar_t* src, const wchar_t* dst)
	{
		return -1; //TODO
	}

	HRESULT SDL_UWP_CreateWinrtSwapChain(int width, int height, int multiSample, ID3D11Device** device, IDXGISwapChain** sc, ID3D11DeviceContext** dc)
	{
		SetSwapChainPanel(width, height, device, sc, dc);
		return S_OK;
	}
}