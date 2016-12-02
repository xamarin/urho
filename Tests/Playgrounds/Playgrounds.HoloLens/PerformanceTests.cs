using Urho;
using Urho.HoloLens;
using Urho.Resources;
using Urho.Shapes;

namespace Playgrounds.HoloLens
{
	public class PerformanceTests : HoloApplication
	{
		private DebugHud hud;

		public PerformanceTests(ApplicationOptions opts) : base(opts)
		{
		}

		protected override void Start()
		{
			base.Start();
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					for (int k = 0; k < 5; k++)
					{
						var child = Scene.CreateChild();
						child.SetScale(0.05f);
						var sphere = child.CreateComponent<Box>();
						//sphere.Model = ResourceCache.GetModel("Sphere.mdl");

						var mat = Material.FromColor(Randoms.NextColor());
						sphere.SetMaterial(mat);
						child.Position = new Vector3(i * 0.12f, j * 0.12f, 1 + k * 0.12f);
					}
				}
			}
			Time.FrameEnded += Time_FrameEnded;
			new MonoDebugHud(this) {FpsOnly = true}.Show(Color.Green, 72);

			hud = Engine.CreateDebugHud();
			hud.ToggleAll();
		}

		private void Time_FrameEnded(FrameEndedEventArgs obj)
		{
			var mem1 = hud.MemoryText.Value;
			var mem2 = hud.ModeText.Value;
			var mem3 = hud.ProfilerText.Value;
			var mem4 = hud.StatsText.Value;
		}

		protected override void OnUpdate(float timeStep)
		{


			base.OnUpdate(timeStep);
		}
	}
}
