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
			material.SetTechnique(0, cache.GetTechnique("Techniques/Diff.xml"), 0, 0);
			return material;
		}
	}
}
