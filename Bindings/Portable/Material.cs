using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urho
{
	partial class Material
	{
		public static Material FromImage(string image)
		{
			var cache = Application.Current.ResourceCache;
			var material = new Material();
			material.SetTexture(TextureUnit.Diffuse, cache.GetTexture2D(image));
			material.SetTechnique(0, CoreAssets.Techniques.Diff, 0, 0);
			return material;
		}

		public static Material FromColor(Color color)
		{
			var material = new Material();
			var cache = Application.Current.ResourceCache;
			material.SetTechnique(0, color.A == 1 ? CoreAssets.Techniques.NoTexture : CoreAssets.Techniques.NoTextureAlpha, 1, 1);
			material.SetShaderParameter("MatDiffColor", color);
			return material;
		}

		public static Material SkyboxFromImages(
			string imagePositiveX,
			string imageNegativeX,
			string imagePositiveY,
			string imageNegativeY,
			string imagePositiveZ,
			string imageNegativeZ)
		{
			var cache = Application.Current.ResourceCache;
			var material = new Material();
			TextureCube cube = new TextureCube();
			cube.SetData(CubeMapFace.PositiveX, cache.GetFile(imagePositiveX, false));
			cube.SetData(CubeMapFace.NegativeX, cache.GetFile(imageNegativeX, false));
			cube.SetData(CubeMapFace.PositiveY, cache.GetFile(imagePositiveY, false));
			cube.SetData(CubeMapFace.NegativeY, cache.GetFile(imageNegativeY, false));
			cube.SetData(CubeMapFace.PositiveZ, cache.GetFile(imagePositiveZ, false));
			cube.SetData(CubeMapFace.NegativeZ, cache.GetFile(imageNegativeZ, false));
			material.SetTexture(TextureUnit.Diffuse, cube);
			material.SetTechnique(0, CoreAssets.Techniques.DiffSkybox, 0, 0);
			material.CullMode = CullMode.None;
			return material;
		}

		public static Material SkyboxFromImage(string image)
		{
			return SkyboxFromImages(image, image, image, image, image, image);
		}
	}
}
