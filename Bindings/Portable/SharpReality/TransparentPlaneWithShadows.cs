using System;

namespace Urho.SharpReality
{
	public class TransparentPlaneWithShadows : Component
	{
		[Preserve]
		public TransparentPlaneWithShadows() {}

		[Preserve]
		public TransparentPlaneWithShadows(IntPtr handle) : base(handle) {}

		public override void OnAttachedToNode(Node node)
		{
			Application.Renderer.ReuseShadowMaps = false;
			Technique technique = new Technique();
			var pass = technique.CreatePass("litalpha");
			pass.DepthWrite = false;
			pass.BlendMode = BlendMode.Multiply;
			pass.PixelShader = "LitSolid";
			pass.VertexShader = "LitSolid";
			pass.VertexShaderDefines = "NOUV";
			Material material = new Material();
			material.SetTechnique(0, technique);
			material.SetShaderParameter(CoreAssets.ShaderParameters.MatDiffColor, Color.White);
			material.SetShaderParameter(CoreAssets.ShaderParameters.MatSpecColor, Color.White);
			StaticModel model = Node.CreateComponent<StaticModel>();
			model.Model = CoreAssets.Models.Plane;
			model.Material = material;
		}
	}
}
