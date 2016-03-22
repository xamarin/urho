using Urho.Gui;
using Urho.Resources;

namespace Urho
{
	//TODO: generate this class using T4 from CoreData folder
	public static class CoreAssets
	{
		public static ResourceCache Cache => Application.Current.ResourceCache;

		public static class Models
		{
			public static Model Box => Cache.GetModel("Models/Box.mdl");
			public static Model Cone => Cache.GetModel("Models/Cone.mdl");
			public static Model Cylinder => Cache.GetModel("Models/Cylinder.mdl");
			public static Model Plane => Cache.GetModel("Models/Plane.mdl");
			public static Model Pyramid => Cache.GetModel("Models/Pyramid.mdl");
			public static Model Sphere => Cache.GetModel("Models/Sphere.mdl");
			public static Model Torus => Cache.GetModel("Models/Torus.mdl");
		}

		public static class Materials
		{
			public static Material DefaultGrey => Cache.GetMaterial("Materials/DefaultGrey.xml");
		}

		public static class Fonts
		{
			public static Font AnonymousPro => Cache.GetFont("Fonts/Anonymous Pro.ttf");
		}

		public static class RenderPaths
		{
		}

		public static class Shaders
		{
		}

		public static class Techniques
		{
		}
	}
}
