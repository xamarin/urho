namespace Urho
{
	partial class Viewport
	{
		public Viewport(Scene scene, Camera camera, RenderPath renderPath) 
			: this(Application.CurrentContext, scene, camera, renderPath) {}

		public Viewport(Scene scene, Camera camera, Urho.IntRect rect, RenderPath renderPath) 
			: this(Application.CurrentContext, scene, camera, rect, renderPath) {}
	}
}
