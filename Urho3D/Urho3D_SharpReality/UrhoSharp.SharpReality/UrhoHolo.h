#pragma once
#include <agile.h>
#include <concrt.h>
#include <d2d1_2.h>
#include <d2d1effects_1.h>
#include <d3d11_4.h>
#include <DirectXColors.h>
#include <DirectXMath.h>
#include <dwrite_2.h>
#include <dxgi1_5.h>
#include <wincodec.h>
#include <WindowsNumerics.h>
#include <wrl.h>
#include <memory>
#include <map>
#include <mutex>
#include <DirectXPackedVector.h>
#include <Urho3D/Engine/Engine.h>
#include <Urho3D/Graphics/Camera.h>
#include <Urho3D/Scene/Node.h>
#include <experimental\resumable>
#include <pplawait.h>
#include <windows.graphics.directx.direct3d11.interop.h>
#include <ppltasks.h>
#include <SDL\SDL.h>
#include <functional>
#include <collection.h>
#include <streambuf>
#include <robuffer.h>
#include <wrl.h>

using namespace std::placeholders;
using namespace concurrency;
using namespace Platform;
using namespace Urho3D;
using namespace Microsoft::WRL;
using namespace DirectX;
using namespace DirectX::PackedVector;
using namespace Platform::Collections;
using namespace Windows::Storage;
using namespace Windows::Storage::Streams;
using namespace Windows::ApplicationModel;
using namespace Windows::ApplicationModel::Activation;
using namespace Windows::ApplicationModel::Core;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Numerics;
using namespace Windows::Perception::Spatial;
using namespace Windows::UI::Core;
using namespace Windows::Foundation::Collections;
using namespace Windows::System::Threading;
using namespace Windows::Graphics::Holographic;
using namespace Windows::Graphics::DirectX;
using namespace Windows::Graphics::DirectX::Direct3D11;
using namespace Windows::Perception::Spatial;
using namespace Windows::Perception::Spatial::Surfaces;
using namespace Windows::UI::Input::Spatial;
using namespace Windows::Media::SpeechRecognition;

namespace DX
{
	inline void ThrowIfFailed(HRESULT hr)
	{
		if (FAILED(hr))
		{
			// Set a breakpoint on this line to catch Win32 API errors.
			throw Platform::Exception::CreateException(hr);
		}
	}
}