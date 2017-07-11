using System;
using System.Threading.Tasks;

namespace Urho.Tests.Bootstrap
{
	public static class TestApp
	{
		public static void RunSimpleApp(Func<SimpleTestApplication, Task> onStartHandler, ApplicationOptions options = null)
		{
			var app = new SimpleTestApplication(options, onStartHandler);
			app.Run();
			if (app.LastException != null)
				throw app.LastException;
		}
	}

	public class SimpleTestApplication : SimpleApplication
	{
		public Exception LastException { get; private set; }

		readonly Func<SimpleTestApplication, Task> onStartHandler;

		public SimpleTestApplication(ApplicationOptions options,
			Func<SimpleTestApplication, Task> onStartHandler) : base(options)
		{
			this.onStartHandler = onStartHandler;
			UnhandledException += (sender, args) => LastException = args.Exception;
		}

		protected override async void Start()
		{
			base.Start();
			try
			{
				await onStartHandler(this);
			}
			catch (Exception e)
			{
				await Exit();
				LastException = e;
			}
		}
	}
}
