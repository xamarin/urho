#include "UrhoHolo.h"

HolographicSpace^ holographicSpace;
HolographicFrame^ current_frame;

void __cdecl WINRT_InitGameBar(struct SDL_VideoDevice *p){};
void __cdecl WINRT_QuitGameBar(struct SDL_VideoDevice *p){};

extern "C"
{
	__declspec(dllexport) void InitializeSpace()
	{
		SDL_SetMainReady();
		auto obj = CoreWindow::GetForCurrentThread()->CustomProperties->Lookup("HolographicSpace");
		holographicSpace = safe_cast<HolographicSpace^>(obj);
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

	char *getenvCompat(const char *name)
	{
		return NULL;
	}

	ComPtr<ID3D11Device> d3ddevice;
	ComPtr<ID3D11DeviceContext> context;
	Direct3D11::IDirect3DDevice^ m_d3dInteropDevice;
	ComPtr<ID3D11Device4> m_d3dDevice;
	ComPtr<ID3D11DeviceContext3> m_d3dContext;
	ComPtr<IDXGIAdapter3> m_dxgiAdapter;
	D3D_FEATURE_LEVEL m_d3dFeatureLevel = D3D_FEATURE_LEVEL_10_0;

	HRESULT SDL_UWP_CreateWinrtSwapChain(int width, int height, int multiSample, ID3D11Device** device, IDXGISwapChain** sc, ID3D11DeviceContext** dc)
	{
		UINT creationFlags = D3D11_CREATE_DEVICE_BGRA_SUPPORT;
		D3D_FEATURE_LEVEL featureLevels[] =
		{
			D3D_FEATURE_LEVEL_12_1,
			D3D_FEATURE_LEVEL_12_0,
			D3D_FEATURE_LEVEL_11_1,
			D3D_FEATURE_LEVEL_11_0,
			D3D_FEATURE_LEVEL_10_1,
			D3D_FEATURE_LEVEL_10_0
		};

		LUID id = { holographicSpace->PrimaryAdapterId.LowPart, holographicSpace->PrimaryAdapterId.HighPart };

		if ((id.HighPart != 0) && (id.LowPart != 0))
		{
			ComPtr<IDXGIFactory1> dxgiFactory;
			DX::ThrowIfFailed(CreateDXGIFactory2(0, IID_PPV_ARGS(&dxgiFactory)));
			ComPtr<IDXGIFactory4> dxgiFactory4;
			DX::ThrowIfFailed(dxgiFactory.As(&dxgiFactory4));
			DX::ThrowIfFailed(dxgiFactory4->EnumAdapterByLuid(id, IID_PPV_ARGS(&m_dxgiAdapter)));
		}

		const HRESULT hr = D3D11CreateDevice(
			m_dxgiAdapter.Get(),        // Either nullptr, or the primary adapter determined by Windows Holographic.
			D3D_DRIVER_TYPE_HARDWARE,   // Create a device using the hardware graphics driver.
			0,                          // Should be 0 unless the driver is D3D_DRIVER_TYPE_SOFTWARE.
			creationFlags,              // Set debug and Direct2D compatibility flags.
			featureLevels,              // List of feature levels this app can support.
			ARRAYSIZE(featureLevels),   // Size of the list above.
			D3D11_SDK_VERSION,          // Always set this to D3D11_SDK_VERSION for Windows Store apps.
			&d3ddevice,                    // Returns the Direct3D device created.
			&m_d3dFeatureLevel,         // Returns feature level of device created.
			&context                    // Returns the device immediate context.
		);

		if (FAILED(hr))
		{
			// If the initialization fails, fall back to the WARP device.
			// For more information on WARP, see:
			// http://go.microsoft.com/fwlink/?LinkId=286690
			DX::ThrowIfFailed(
				D3D11CreateDevice(
					nullptr,              // Use the default DXGI adapter for WARP.
					D3D_DRIVER_TYPE_WARP, // Create a WARP device instead of a hardware device.
					0,
					creationFlags,
					featureLevels,
					ARRAYSIZE(featureLevels),
					D3D11_SDK_VERSION,
					&d3ddevice,
					&m_d3dFeatureLevel,
					&context
				)
			);
		}

		DX::ThrowIfFailed(d3ddevice.As(&m_d3dDevice));
		DX::ThrowIfFailed(context.As(&m_d3dContext));
		ComPtr<IDXGIDevice3> dxgiDevice;
		DX::ThrowIfFailed(m_d3dDevice.As(&dxgiDevice));
		m_d3dInteropDevice = CreateDirect3DDevice(dxgiDevice.Get());
		holographicSpace->SetDirect3D11Device(m_d3dInteropDevice);
		current_frame = holographicSpace->CreateNextFrame();
		ComPtr<IDXGIAdapter> dxgiAdapter;
		DX::ThrowIfFailed(dxgiDevice->GetAdapter(&dxgiAdapter));
		DX::ThrowIfFailed(dxgiAdapter.As(&m_dxgiAdapter));

		*device = m_d3dDevice.Get();
		*dc = m_d3dContext.Get();
		return S_OK;
	}

	ComPtr<ID3D11Texture2D> cameraBackBuffer;
	ComPtr<ID3D11Resource> resource;

	void Camera_SetHoloProjection(Camera* camera, const class Matrix4& view, const class Matrix4& projection)
	{
		float nearClip = projection.m32_ / projection.m22_; //0.1
		float farClip = projection.m32_ / (projection.m22_ + 1); //20
		float fovVertical = 360 * atanf(1 / projection.m11_) / M_PI; //17.1
		float aspect = projection.m11_ / projection.m00_; //1.76

		camera->SetSkew(projection.m10_);
		camera->SetProjectionOffset(Vector2(-projection.m20_ / 2.0f, -projection.m21_ / 2.0f));
		camera->SetAspectRatio(aspect);
		camera->SetFov(fovVertical);
		camera->SetNearClip(nearClip);
		camera->SetFarClip(farClip);

		Matrix4 viewt = view.Inverse().Transpose();
		Quaternion rotation = viewt.Rotation();
		rotation.x_ *= -1; //RH to LH
		rotation.y_ *= -1;

		auto cameraNode = camera->GetNode();
		cameraNode->SetWorldPosition(Vector3(viewt.m03_, viewt.m13_, -viewt.m23_));
		cameraNode->SetWorldRotation(rotation);
	}
	
	__declspec(dllexport) void Camera_SetHoloProjections(
		Camera* leftEyeCamera, Camera* rightEyeCamera, Camera* cullingCamera, 
		const class Matrix4& leftView, const class Matrix4& leftProjection, 
		const class Matrix4& rightView, const class Matrix4& rightProjection)
	{
		Camera_SetHoloProjection(leftEyeCamera, leftView, leftProjection);
		Camera_SetHoloProjection(rightEyeCamera, rightView, rightProjection);

		if (cullingCamera)
		{
			auto leftCameraNode = leftEyeCamera->GetNode();
			auto rightCameraNode = rightEyeCamera->GetNode();
			
			auto leftToRightVec = (rightCameraNode->GetWorldPosition() - leftCameraNode->GetWorldPosition());
			float separation = leftToRightVec.Length();

			// Note: atanf and tanf cancel, as does reciprocal
			// float fovHorizontal = 360 * atanf(1 / projection.m00_) / M_PI;
			// float fovHorizontalHalfedRad = atanf(1 / leftProjection.m00_);
			// float cullEyePullback = (0.5f * separation) / tanf(fovHorizontalHalfedRad);
			float cullEyePullback = (0.5f * separation) * leftProjection.m00_;

			// Move cull camera between eyes and pull back
			auto cullCameraPosition = (leftCameraNode->GetWorldPosition() + (0.5f * leftToRightVec)) - (leftCameraNode->GetWorldDirection().Normalized() * cullEyePullback);
			
			// Copy projection and rotation from left camera and set the position
			Camera_SetHoloProjection(cullingCamera, leftView, leftProjection);
			cullingCamera->GetNode()->SetWorldPosition(cullCameraPosition);
			
			// Move culling camera's near and far planes ahead to match that of the eye cameras
			cullingCamera->SetNearClip(cullingCamera->GetNearClip() + cullEyePullback);
			cullingCamera->SetFarClip(cullingCamera->GetFarClip() + cullEyePullback);
		}
	}

	ID3D11Texture2D* HoloLens_GetBackbuffer()
	{
		auto pose = current_frame->CurrentPrediction->CameraPoses->First()->Current;//TOOD: remove First()
		auto renderingParams = current_frame->GetRenderingParameters(pose);
		IDirect3DSurface^ surface = renderingParams->Direct3D11BackBuffer;
		DX::ThrowIfFailed(Windows::Graphics::DirectX::Direct3D11::GetDXGIInterfaceFromObject(surface, IID_PPV_ARGS(&resource)));
		DX::ThrowIfFailed(resource.As(&cameraBackBuffer));
		return cameraBackBuffer.Get();
	}
}
template <typename t = byte>
t* GetDataFromIBuffer(Windows::Storage::Streams::IBuffer^ container)
{
	if (container == nullptr)
	{
		return nullptr;
	}

	unsigned int bufferLength = container->Length;

	if (!(bufferLength > 0))
	{
		return nullptr;
	}

	HRESULT hr = S_OK;

	ComPtr<IUnknown> pUnknown = reinterpret_cast<IUnknown*>(container);
	ComPtr<IBufferByteAccess> spByteAccess;
	hr = pUnknown.As(&spByteAccess);
	if (FAILED(hr))
	{
		return nullptr;
	}

	byte* pRawData = nullptr;
	hr = spByteAccess->Buffer(&pRawData);
	if (FAILED(hr))
	{
		return nullptr;
	}

	return reinterpret_cast<t*>(pRawData);
}
