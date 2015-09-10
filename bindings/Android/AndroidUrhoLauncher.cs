using System;
using Android.Content;

namespace Urho.Droid
{
	public static class AndroidUrhoLauncher
	{
		public static void Run(Func<Application> appCreator)
		{
			Urho.Application.RegisterSdlLauncher(appCreator);
			var context = Android.App.Application.Context;
			var intent = new Intent(context, typeof(GameActivity));
			intent.AddFlags(ActivityFlags.NewTask);
			context.StartActivity(intent);
		}
	}
}