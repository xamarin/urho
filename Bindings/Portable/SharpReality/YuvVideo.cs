using System;
using Urho.Urho2D;

namespace Urho.SharpReality
{
	public class YuvVideo : Component
	{
		[Preserve]
		public YuvVideo() { }

		[Preserve]
		public YuvVideo(IntPtr handle) : base(handle) { }

		public Texture2D CameraYtexture { get; private set; }
		public Texture2D CameraUVtexture { get; private set; }
		public RenderPathCommand RenderPathCommand { get; private set; }
		public bool TexturesInited { get; private set; }

		public unsafe void SetData(
			IntPtr yTexturePtr, IntPtr uvTexturePtr, 
			int yWidth, int yHeight, 
			int uvWidth, int uvHeight, 
			int nativeBoundsWidth, int nativeBoundsHeight)
		{
			if (!TexturesInited)
			{
				TexturesInited = true;
				CreateTextures(yWidth, yHeight, uvWidth, uvHeight, nativeBoundsWidth, nativeBoundsHeight);
			}

			if (yTexturePtr == IntPtr.Zero || uvTexturePtr == IntPtr.Zero)
				return;

			CameraYtexture.SetData(0, 0, 0, yWidth, yHeight, (void*)yTexturePtr);
			CameraUVtexture.SetData(0, 0, 0, uvWidth, uvHeight, (void*)uvTexturePtr);
		}

		unsafe void CreateTextures(
			int yWidth, int yHeight, 
			int uvWidth, int uvHeight,
			int nativeBoundsWidth, int nativeBoundsHeight)
		{
			var cache = Application.ResourceCache;

			// texture for Y-plane;
			CameraYtexture = new Texture2D();
			CameraYtexture.SetNumLevels(1);
			CameraYtexture.FilterMode = TextureFilterMode.Bilinear;
			CameraYtexture.SetAddressMode(TextureCoordinate.U, TextureAddressMode.Clamp);
			CameraYtexture.SetAddressMode(TextureCoordinate.V, TextureAddressMode.Clamp);
			CameraYtexture.SetSize(yWidth, yHeight, Graphics.LuminanceFormat, TextureUsage.Dynamic);
			CameraYtexture.Name = nameof(CameraYtexture);
			cache.AddManualResource(CameraYtexture);

			// texture for UV-plane;
			CameraUVtexture = new Texture2D();
			CameraUVtexture.SetNumLevels(1);
			CameraUVtexture.SetSize(uvWidth, uvHeight, Graphics.LuminanceAlphaFormat, TextureUsage.Dynamic);
			CameraUVtexture.FilterMode = TextureFilterMode.Bilinear;
			CameraUVtexture.SetAddressMode(TextureCoordinate.U, TextureAddressMode.Clamp);
			CameraUVtexture.SetAddressMode(TextureCoordinate.V, TextureAddressMode.Clamp);
			CameraUVtexture.Name = nameof(CameraUVtexture);
			cache.AddManualResource(CameraUVtexture);
			
			float imageAspect = (float)yWidth / yHeight;
			float screenAspect = (float)nativeBoundsHeight / nativeBoundsWidth;

			var rpc = new RenderPathCommand(RenderCommandType.Quad);
			rpc.SetTextureName(TextureUnit.Diffuse, CameraYtexture.Name);
			rpc.SetTextureName(TextureUnit.Normal, CameraUVtexture.Name);
			rpc.Type = RenderCommandType.Quad;
			rpc.VertexShaderName = (UrhoString)"ARKit";
			rpc.PixelShaderName = (UrhoString)"ARKit";
			rpc.SetOutput(0, "viewport");
			rpc.SetShaderParameter("CameraScale", screenAspect / imageAspect);
			RenderPathCommand = rpc;

			Application.Renderer.GetViewport(0).RenderPath.InsertCommand(1, rpc);
		}
	}
}
