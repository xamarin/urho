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
		}
	}

}