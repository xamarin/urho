using Urho;

namespace $safeprojectname$
{
	class Program
	{
		static void Main(string[] args)
		{
			UrhoEngine.Init();
			new MyGame(new Context()).Run();
		}
	}
}
