using Urho.Gui;
using Urho.Resources;
using Urho.Urho2D;

namespace Urho
{
	public static class CoreAssets
	{
		public static ResourceCache Cache => Application.Current.ResourceCache;

		public static class Materials
		{
			public static Material DefaultGrey => Cache.GetMaterial("Materials/DefaultGrey.xml");
		}

		public static class Models
		{
			public static Model Box => Cache.GetModel("Models/Box.mdl");
			public static Model Cone => Cache.GetModel("Models/Cone.mdl");
			public static Model Cylinder => Cache.GetModel("Models/Cylinder.mdl");
			public static Model Dome => Cache.GetModel("Models/Dome.mdl");
			public static Model Plane => Cache.GetModel("Models/Plane.mdl");
			public static Model Pyramid => Cache.GetModel("Models/Pyramid.mdl");
			public static Model Sphere => Cache.GetModel("Models/Sphere.mdl");
			public static Model Torus => Cache.GetModel("Models/Torus.mdl");

			/// <summary>
			/// Line primitives. Could be very useful models for debug, user interface etc.
			/// </summary>
			public static class LinePrimitives
			{
				public static Model Basis => Cache.GetModel("Models/LinePrimitives/Basis.mdl");
				public static Model Box1x1x1 => Cache.GetModel("Models/LinePrimitives/Box1x1x1.mdl");
				public static Model CubicBezier => Cache.GetModel("Models/LinePrimitives/CubicBezier.mdl");
				public static Model LinearBezier => Cache.GetModel("Models/LinePrimitives/LinearBezier.mdl");
				public static Model QuadraticBezier => Cache.GetModel("Models/LinePrimitives/QuadraticBezier.mdl");
				public static Model UnitX => Cache.GetModel("Models/LinePrimitives/UnitX.mdl");
				public static Model UnitY => Cache.GetModel("Models/LinePrimitives/UnitY.mdl");
				public static Model UnitZ => Cache.GetModel("Models/LinePrimitives/UnitZ.mdl");
			}
		}

		public static class Fonts
		{
			public static Font AnonymousPro => Cache.GetFont("Fonts/Anonymous Pro.ttf");
		}

		public static class RenderPaths
		{
			public static XmlFile Deferred => Cache.GetXmlFile("RenderPaths/Deferred.xml");
			public static XmlFile DeferredHWDepth => Cache.GetXmlFile("RenderPaths/DeferredHWDepth.xml");
			public static XmlFile Forward => Cache.GetXmlFile("RenderPaths/Forward.xml");
			public static XmlFile ForwardDepth => Cache.GetXmlFile("RenderPaths/ForwardDepth.xml");
			public static XmlFile ForwardHWDepth => Cache.GetXmlFile("RenderPaths/ForwardHWDepth.xml");
			public static XmlFile PBRDeferred => Cache.GetXmlFile("RenderPaths/PBRDeferred.xml");
			public static XmlFile PBRDeferredHWDepth => Cache.GetXmlFile("RenderPaths/PBRDeferredHWDepth.xml");
			public static XmlFile Prepass => Cache.GetXmlFile("RenderPaths/Prepass.xml");
			public static XmlFile PrepassHDR => Cache.GetXmlFile("RenderPaths/PrepassHDR.xml");
			public static XmlFile PrepassHWDepth => Cache.GetXmlFile("RenderPaths/PrepassHWDepth.xml");
		}

		public static class PostProcess
		{
			public static XmlFile FXAA2 => Cache.GetXmlFile("PostProcess/FXAA2.xml");
			public static XmlFile FXAA3 => Cache.GetXmlFile("PostProcess/FXAA3.xml");
			public static XmlFile Blur => Cache.GetXmlFile("PostProcess/Blur.xml");
			public static XmlFile AutoExposure => Cache.GetXmlFile("PostProcess/AutoExposure.xml");
			public static XmlFile Bloom => Cache.GetXmlFile("PostProcess/Bloom.xml");
			public static XmlFile BloomHDR => Cache.GetXmlFile("PostProcess/BloomHDR.xml");
			public static XmlFile ColorCorrection => Cache.GetXmlFile("PostProcess/ColorCorrection.xml");
			public static XmlFile GammaCorrection => Cache.GetXmlFile("PostProcess/GammaCorrection.xml");
			public static XmlFile GreyScale => Cache.GetXmlFile("PostProcess/GreyScale.xml");
			public static XmlFile Tonemap => Cache.GetXmlFile("PostProcess/Tonemap.xml");
		}

		public static class UIs
		{
			public static XmlFile DefaultStyle => Cache.GetXmlFile("UI/DefaultStyle.xml");
			public static XmlFile MessageBox => Cache.GetXmlFile("UI/MessageBox.xml");
			public static XmlFile ScreenJoystick => Cache.GetXmlFile("UI/ScreenJoystick.xml");
			public static XmlFile ScreenJoystick2 => Cache.GetXmlFile("UI/ScreenJoystick2.xml");
		}

		public static class Techniques
		{
			public static Technique BasicVColUnlitAlpha => Cache.GetTechnique("Techniques/BasicVColUnlitAlpha.xml");
			public static Technique Diff => Cache.GetTechnique("Techniques/Diff.xml");
			public static Technique DiffAdd => Cache.GetTechnique("Techniques/DiffAdd.xml");
			public static Technique DiffAddAlpha => Cache.GetTechnique("Techniques/DiffAddAlpha.xml");
			public static Technique DiffAlpha => Cache.GetTechnique("Techniques/DiffAlpha.xml");
			public static Technique DiffAlphaTranslucent => Cache.GetTechnique("Techniques/DiffAlphaTranslucent.xml");
			public static Technique DiffAO => Cache.GetTechnique("Techniques/DiffAO.xml");
			public static Technique DiffAOAlpha => Cache.GetTechnique("Techniques/DiffAOAlpha.xml");
			public static Technique DiffEmissive => Cache.GetTechnique("Techniques/DiffEmissive.xml");
			public static Technique DiffEmissiveAlpha => Cache.GetTechnique("Techniques/DiffEmissiveAlpha.xml");
			public static Technique DiffEnvCube => Cache.GetTechnique("Techniques/DiffEnvCube.xml");
			public static Technique DiffEnvCubeAlpha => Cache.GetTechnique("Techniques/DiffEnvCubeAlpha.xml");
			public static Technique DiffEnvCubeAO => Cache.GetTechnique("Techniques/DiffEnvCubeAO.xml");
			public static Technique DiffEnvCubeAOAlpha => Cache.GetTechnique("Techniques/DiffEnvCubeAOAlpha.xml");
			public static Technique DiffLightMap => Cache.GetTechnique("Techniques/DiffLightMap.xml");
			public static Technique DiffLightMapAlpha => Cache.GetTechnique("Techniques/DiffLightMapAlpha.xml");
			public static Technique DiffLitParticleAlpha => Cache.GetTechnique("Techniques/DiffLitParticleAlpha.xml");
			public static Technique DiffMultiply => Cache.GetTechnique("Techniques/DiffMultiply.xml");
			public static Technique DiffNormal => Cache.GetTechnique("Techniques/DiffNormal.xml");
			public static Technique DiffNormalAlpha => Cache.GetTechnique("Techniques/DiffNormalAlpha.xml");
			public static Technique DiffNormalAlphaTranslucent => Cache.GetTechnique("Techniques/DiffNormalAlphaTranslucent.xml");
			public static Technique DiffNormalAO => Cache.GetTechnique("Techniques/DiffNormalAO.xml");
			public static Technique DiffNormalAOAlpha => Cache.GetTechnique("Techniques/DiffNormalAOAlpha.xml");
			public static Technique DiffNormalEmissive => Cache.GetTechnique("Techniques/DiffNormalEmissive.xml");
			public static Technique DiffNormalEmissiveAlpha => Cache.GetTechnique("Techniques/DiffNormalEmissiveAlpha.xml");
			public static Technique DiffNormalEnvCube => Cache.GetTechnique("Techniques/DiffNormalEnvCube.xml");
			public static Technique DiffNormalEnvCubeAlpha => Cache.GetTechnique("Techniques/DiffNormalEnvCubeAlpha.xml");
			public static Technique DiffNormalSpec => Cache.GetTechnique("Techniques/DiffNormalSpec.xml");
			public static Technique DiffNormalSpecAlpha => Cache.GetTechnique("Techniques/DiffNormalSpecAlpha.xml");
			public static Technique DiffNormalSpecAO => Cache.GetTechnique("Techniques/DiffNormalSpecAO.xml");
			public static Technique DiffNormalSpecAOAlpha => Cache.GetTechnique("Techniques/DiffNormalSpecAOAlpha.xml");
			public static Technique DiffNormalSpecEmissive => Cache.GetTechnique("Techniques/DiffNormalSpecEmissive.xml");
			public static Technique DiffNormalSpecEmissiveAlpha => Cache.GetTechnique("Techniques/DiffNormalSpecEmissiveAlpha.xml");
			public static Technique DiffOverlay => Cache.GetTechnique("Techniques/DiffOverlay.xml");
			public static Technique DiffSkybox => Cache.GetTechnique("Techniques/DiffSkybox.xml");
			public static Technique DiffSkyboxHDRScale => Cache.GetTechnique("Techniques/DiffSkyboxHDRScale.xml");
			public static Technique DiffSkydome => Cache.GetTechnique("Techniques/DiffSkydome.xml");
			public static Technique DiffSkyplane => Cache.GetTechnique("Techniques/DiffSkyplane.xml");
			public static Technique DiffSpec => Cache.GetTechnique("Techniques/DiffSpec.xml");
			public static Technique DiffSpecAlpha => Cache.GetTechnique("Techniques/DiffSpecAlpha.xml");
			public static Technique DiffUnlit => Cache.GetTechnique("Techniques/DiffUnlit.xml");
			public static Technique DiffUnlitAlpha => Cache.GetTechnique("Techniques/DiffUnlitAlpha.xml");
			public static Technique DiffVCol => Cache.GetTechnique("Techniques/DiffVCol.xml");
			public static Technique DiffVColAdd => Cache.GetTechnique("Techniques/DiffVColAdd.xml");
			public static Technique DiffVColAddAlpha => Cache.GetTechnique("Techniques/DiffVColAddAlpha.xml");
			public static Technique DiffVColMultiply => Cache.GetTechnique("Techniques/DiffVColMultiply.xml");
			public static Technique DiffVColUnlitAlpha => Cache.GetTechnique("Techniques/DiffVColUnlitAlpha.xml");
			public static Technique NoTexture => Cache.GetTechnique("Techniques/NoTexture.xml");
			public static Technique NoTextureAdd => Cache.GetTechnique("Techniques/NoTextureAdd.xml");
			public static Technique NoTextureAddAlpha => Cache.GetTechnique("Techniques/NoTextureAddAlpha.xml");
			public static Technique NoTextureAlpha => Cache.GetTechnique("Techniques/NoTextureAlpha.xml");
			public static Technique NoTextureAO => Cache.GetTechnique("Techniques/NoTextureAO.xml");
			public static Technique NoTextureAOAlpha => Cache.GetTechnique("Techniques/NoTextureAOAlpha.xml");
			public static Technique NoTextureEnvCube => Cache.GetTechnique("Techniques/NoTextureEnvCube.xml");
			public static Technique NoTextureEnvCubeAlpha => Cache.GetTechnique("Techniques/NoTextureEnvCubeAlpha.xml");
			public static Technique NoTextureEnvCubeAO => Cache.GetTechnique("Techniques/NoTextureEnvCubeAO.xml");
			public static Technique NoTextureEnvCubeAOAlpha => Cache.GetTechnique("Techniques/NoTextureEnvCubeAOAlpha.xml");
			public static Technique NoTextureMultiply => Cache.GetTechnique("Techniques/NoTextureMultiply.xml");
			public static Technique NoTextureNormal => Cache.GetTechnique("Techniques/NoTextureNormal.xml");
			public static Technique NoTextureNormalAlpha => Cache.GetTechnique("Techniques/NoTextureNormalAlpha.xml");
			public static Technique NoTextureOverlay => Cache.GetTechnique("Techniques/NoTextureOverlay.xml");
			public static Technique NoTextureUnlit => Cache.GetTechnique("Techniques/NoTextureUnlit.xml");
			public static Technique NoTextureUnlitAlpha => Cache.GetTechnique("Techniques/NoTextureUnlitAlpha.xml");
			public static Technique NoTextureUnlitVCol => Cache.GetTechnique("Techniques/NoTextureUnlitVCol.xml");
			public static Technique NoTextureVCol => Cache.GetTechnique("Techniques/NoTextureVCol.xml");
			public static Technique NoTextureVColAdd => Cache.GetTechnique("Techniques/NoTextureVColAdd.xml");
			public static Technique NoTextureVColAddAlpha => Cache.GetTechnique("Techniques/NoTextureVColAddAlpha.xml");
			public static Technique NoTextureVColMultiply => Cache.GetTechnique("Techniques/NoTextureVColMultiply.xml");
			public static Technique TerrainBlend => Cache.GetTechnique("Techniques/TerrainBlend.xml");
			public static Technique VegetationDiff => Cache.GetTechnique("Techniques/VegetationDiff.xml");
			public static Technique VegetationDiffUnlit => Cache.GetTechnique("Techniques/VegetationDiffUnlit.xml");
			public static Technique Water => Cache.GetTechnique("Techniques/Water.xml");
			public static Technique DiffLitParticleAlphaSoft => Cache.GetTechnique("Techniques/DiffLitParticleAlphaSoft.xml");
			public static Technique DiffLitParticleAlphaSoftExpand => Cache.GetTechnique("Techniques/DiffLitParticleAlphaSoftExpand.xml");
			public static Technique DiffUnlitParticleAdd => Cache.GetTechnique("Techniques/DiffUnlitParticleAdd.xml");
			public static Technique DiffUnlitParticleAddSoft => Cache.GetTechnique("Techniques/DiffUnlitParticleAddSoft.xml");
			public static Technique DiffUnlitParticleAlpha => Cache.GetTechnique("Techniques/DiffUnlitParticleAlpha.xml");
			public static Technique DiffUnlitParticleAlphaSoft => Cache.GetTechnique("Techniques/DiffUnlitParticleAlphaSoft.xml");
			public static Technique DiffUnlitParticleAlphaSoftExpand => Cache.GetTechnique("Techniques/DiffUnlitParticleAlphaSoftExpand.xml");

			public static class PBR
			{
				public static Technique DiffNormalSpecEmissive => Cache.GetTechnique("Techniques/PBR/DiffNormalSpecEmissive.xml");
				public static Technique DiffNormalSpecEmissiveAlpha => Cache.GetTechnique("Techniques/PBR/DiffNormalSpecEmissiveAlpha.xml");
				public static Technique PBRDiff => Cache.GetTechnique("Techniques/PBR/PBRDiff.xml");
				public static Technique PBRDiffAlpha => Cache.GetTechnique("Techniques/PBR/PBRDiffAlpha.xml");
				public static Technique PBRDiffNormal => Cache.GetTechnique("Techniques/PBR/PBRDiffNormal.xml");
				public static Technique PBRDiffNormalAlpha => Cache.GetTechnique("Techniques/PBR/PBRDiffNormalAlpha.xml");
				public static Technique PBRDiffNormalEmissive => Cache.GetTechnique("Techniques/PBR/PBRDiffNormalEmissive.xml");
				public static Technique PBRDiffNormalEmissiveAlpha => Cache.GetTechnique("Techniques/PBR/PBRDiffNormalEmissiveAlpha.xml");
				public static Technique PBRMetallicRoughDiffNormalSpec => Cache.GetTechnique("Techniques/PBR/PBRMetallicRoughDiffNormalSpec.xml");
				public static Technique PBRMetallicRoughDiffNormalSpecEmissive => Cache.GetTechnique("Techniques/PBR/PBRMetallicRoughDiffNormalSpecEmissive.xml");
				public static Technique PBRMetallicRoughDiffNormalSpecEmissiveAlpha => Cache.GetTechnique("Techniques/PBR/PBRMetallicRoughDiffNormalSpecEmissiveAlpha.xml");
				public static Technique PBRMetallicRoughDiffSpec => Cache.GetTechnique("Techniques/PBR/PBRMetallicRoughDiffSpec.xml");
				public static Technique PBRMetallicRoughDiffSpecAlpha => Cache.GetTechnique("Techniques/PBR/PBRMetallicRoughDiffSpecAlpha.xml");
				public static Technique PBRNoTexture => Cache.GetTechnique("Techniques/PBR/PBRNoTexture.xml");
				public static Technique PBRNoTextureAlpha => Cache.GetTechnique("Techniques/PBR/PBRNoTextureAlpha.xml");
			}
		}

		public static class Textures
		{
			public static Texture2D LUTIdentity => Cache.GetTexture2D("Textures/LUTIdentity.png");
			public static Texture2D LUTIdentityXml => Cache.GetTexture2D("Textures/LUTIdentity.xml");
			public static Texture2D Ramp => Cache.GetTexture2D("Textures/Ramp.png");
			public static Texture2D RampXml => Cache.GetTexture2D("Textures/Ramp.xml");
			public static Texture2D RampExtreme => Cache.GetTexture2D("Textures/RampExtreme.png");
			public static Texture2D RampExtremeXml => Cache.GetTexture2D("Textures/RampExtreme.xml");
			public static Texture2D RampWide => Cache.GetTexture2D("Textures/RampWide.png");
			public static Texture2D RampWideXml => Cache.GetTexture2D("Textures/RampWide.xml");
			public static Texture2D Spot => Cache.GetTexture2D("Textures/Spot.png");
			public static Texture2D SpotXml => Cache.GetTexture2D("Textures/Spot.xml");
			public static Texture2D SpotWide => Cache.GetTexture2D("Textures/SpotWide.png");
			public static Texture2D SpotWideXml => Cache.GetTexture2D("Textures/SpotWide.xml");
			public static Texture2D PlaneTile => Cache.GetTexture2D("Textures/PlaneTile.png");
		}

		public static class ShaderParameters
		{
			/// <summary>
			/// Type: vec3, Defined in Uniforms.glsl:L10
			/// </summary>
			public const string AmbientStartColor = "AmbientStartColor";

			/// <summary>
			/// Type: vec3, Defined in Uniforms.glsl:L11
			/// </summary>
			public const string AmbientEndColor = "AmbientEndColor";

			/// <summary>
			/// Type: mat3, Defined in Uniforms.glsl:L12
			/// </summary>
			public const string BillboardRot = "BillboardRot";

			/// <summary>
			/// Type: vec3, Defined in Uniforms.glsl:L13
			/// </summary>
			public const string CameraPos = "CameraPos";

			/// <summary>
			/// Type: float, Defined in Uniforms.glsl:L14
			/// </summary>
			public const string NearClip = "NearClip";

			/// <summary>
			/// Type: float, Defined in Uniforms.glsl:L15
			/// </summary>
			public const string FarClip = "FarClip";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L16
			/// </summary>
			public const string DepthMode = "DepthMode";

			/// <summary>
			/// Type: vec3, Defined in Uniforms.glsl:L17
			/// </summary>
			public const string FrustumSize = "FrustumSize";

			/// <summary>
			/// Type: float, Defined in Uniforms.glsl:L18
			/// </summary>
			public const string DeltaTime = "DeltaTime";

			/// <summary>
			/// Type: float, Defined in Uniforms.glsl:L19
			/// </summary>
			public const string ElapsedTime = "ElapsedTime";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L20
			/// </summary>
			public const string GBufferOffsets = "GBufferOffsets";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L21
			/// </summary>
			public const string LightPos = "LightPos";

			/// <summary>
			/// Type: vec3, Defined in Uniforms.glsl:L22
			/// </summary>
			public const string LightDir = "LightDir";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L23
			/// </summary>
			public const string NormalOffsetScale = "NormalOffsetScale";

			/// <summary>
			/// Type: mat4, Defined in Uniforms.glsl:L24
			/// </summary>
			public const string Model = "Model";

			/// <summary>
			/// Type: mat4, Defined in Uniforms.glsl:L25
			/// </summary>
			public const string View = "View";

			/// <summary>
			/// Type: mat4, Defined in Uniforms.glsl:L26
			/// </summary>
			public const string ViewInv = "ViewInv";

			/// <summary>
			/// Type: mat4, Defined in Uniforms.glsl:L27
			/// </summary>
			public const string ViewProj = "ViewProj";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L28
			/// </summary>
			public const string UOffset = "UOffset";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L29
			/// </summary>
			public const string VOffset = "VOffset";

			/// <summary>
			/// Type: mat4, Defined in Uniforms.glsl:L30
			/// </summary>
			public const string Zone = "Zone";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L43
			/// </summary>
			public const string ClipPlane = "ClipPlane";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L54
			/// </summary>
			public const string AmbientColor = "AmbientColor";

			/// <summary>
			/// Type: vec3, Defined in Uniforms.glsl:L55
			/// </summary>
			public const string CameraPosPS = "CameraPosPS";

			/// <summary>
			/// Type: float, Defined in Uniforms.glsl:L56
			/// </summary>
			public const string DeltaTimePS = "DeltaTimePS";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L57
			/// </summary>
			public const string DepthReconstruct = "DepthReconstruct";

			/// <summary>
			/// Type: float, Defined in Uniforms.glsl:L58
			/// </summary>
			public const string ElapsedTimePS = "ElapsedTimePS";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L59
			/// </summary>
			public const string FogParams = "FogParams";

			/// <summary>
			/// Type: vec3, Defined in Uniforms.glsl:L60
			/// </summary>
			public const string FogColor = "FogColor";

			/// <summary>
			/// Type: vec2, Defined in Uniforms.glsl:L61
			/// </summary>
			public const string GBufferInvSize = "GBufferInvSize";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L62
			/// </summary>
			public const string LightColor = "LightColor";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L63
			/// </summary>
			public const string LightPosPS = "LightPosPS";

			/// <summary>
			/// Type: vec3, Defined in Uniforms.glsl:L64
			/// </summary>
			public const string LightDirPS = "LightDirPS";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L65
			/// </summary>
			public const string NormalOffsetScalePS = "NormalOffsetScalePS";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L66
			/// </summary>
			public const string MatDiffColor = "MatDiffColor";

			/// <summary>
			/// Type: vec3, Defined in Uniforms.glsl:L67
			/// </summary>
			public const string MatEmissiveColor = "MatEmissiveColor";

			/// <summary>
			/// Type: vec3, Defined in Uniforms.glsl:L68
			/// </summary>
			public const string MatEnvMapColor = "MatEnvMapColor";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L69
			/// </summary>
			public const string MatSpecColor = "MatSpecColor";

			public static class PBR
			{
				/// <summary>
				/// Type: float, Defined in Uniforms.glsl:L71
				/// </summary>
				public const string Roughness = "Roughness";

				/// <summary>
				/// Type: float, Defined in Uniforms.glsl:L72
				/// </summary>
				public const string Metallic = "Metallic";

				/// <summary>
				/// Type: float, Defined in Uniforms.glsl:L73
				/// </summary>
				public const string LightRad = "LightRad";

				/// <summary>
				/// Type: float, Defined in Uniforms.glsl:L74
				/// </summary>
				public const string LightLength = "LightLength";
			}

			/// <summary>
			/// Type: vec3, Defined in Uniforms.glsl:L76
			/// </summary>
			public const string ZoneMin = "ZoneMin";

			/// <summary>
			/// Type: vec3, Defined in Uniforms.glsl:L77
			/// </summary>
			public const string ZoneMax = "ZoneMax";

			/// <summary>
			/// Type: float, Defined in Uniforms.glsl:L78
			/// </summary>
			public const string NearClipPS = "NearClipPS";

			/// <summary>
			/// Type: float, Defined in Uniforms.glsl:L79
			/// </summary>
			public const string FarClipPS = "FarClipPS";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L80
			/// </summary>
			public const string ShadowCubeAdjust = "ShadowCubeAdjust";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L81
			/// </summary>
			public const string ShadowDepthFade = "ShadowDepthFade";

			/// <summary>
			/// Type: vec2, Defined in Uniforms.glsl:L82
			/// </summary>
			public const string ShadowIntensity = "ShadowIntensity";

			/// <summary>
			/// Type: vec2, Defined in Uniforms.glsl:L83
			/// </summary>
			public const string ShadowMapInvSize = "ShadowMapInvSize";

			/// <summary>
			/// Type: vec4, Defined in Uniforms.glsl:L84
			/// </summary>
			public const string ShadowSplits = "ShadowSplits";

			/// <summary>
			/// Type: vec2, Defined in Uniforms.glsl:L87
			/// </summary>
			public const string VSMShadowParams = "VSMShadowParams";

		}
	}
}