namespace Urho
{
	partial class Viewport
	{
		public Viewport(Scene scene, Camera camera, RenderPath renderPath) 
			: this(Application.CurrentContext, scene, camera, renderPath) {}

		public Viewport(Scene scene, Camera camera, Urho.IntRect rect, RenderPath renderPath) 
			: this(Application.CurrentContext, scene, camera, rect, renderPath) {}

		public unsafe void SetClearColor(Color color)
		{
			Runtime.ValidateRefCounted(this);
			var rp = RenderPath;
			for (int i = 0; i < rp.NumCommands; i++)
			{
				var cmd = rp.GetCommand((uint)i);
				if (cmd->Type == RenderCommandType.Clear)
				{
					cmd->UseFogColor = 0;
					cmd->ClearColor = color;
				}
			}
		}
	}
}
