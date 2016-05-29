using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System.Threading;
using Windows.UI.Xaml.Controls;

namespace Urho.UWP
{
	public class UrhoSurface : SwapChainPanel
	{
		bool paused;
		bool stop;
		bool inited;

		/// <param name="customAssetsPak">all assets must be *.pak files (use PackageTool.exe).</param>
		public TGame Run<TGame>(string customAssetsPak = null) where TGame : Urho.Application
		{
			return (TGame) Run(typeof(TGame), customAssetsPak);
		}

		/// <param name="customAssetsPak">all assets must be *.pak files (use PackageTool.exe).</param>
		public Application Run(Type appType, string customAssetsPak = null)
		{
			stop = false;
			paused = false;
			inited = false;
			Sdl.SetMainReady();
			ApplicationOptions options = new ApplicationOptions(assetsFolder: null);
			if (!string.IsNullOrEmpty(customAssetsPak))
			{
				if (!customAssetsPak.StartsWith("ms-"))
					customAssetsPak = "ms-appx:///" + customAssetsPak;
				
				options.ResourcePackagesPaths = new[] { Path.GetFileName(customAssetsPak) };
				CopyContentFileToLocalFolder(customAssetsPak);
			}
			CopyEmbeddedResourceToLocalFolder("Urho.CoreData.pak", "CoreData.pak");
			var app = (Application)Activator.CreateInstance(appType, options);
			app.Run();
			Sdl.SendWindowEvent(SdlWindowEvent.SDL_WINDOWEVENT_RESIZED, (int)this.ActualWidth, (int)this.ActualHeight);
			inited = true;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			ThreadPool.RunAsync(_ =>
				{
					while (stopRenderingTask == null)
					{
						if (!paused && !app.IsExiting)
						{
							app.Engine.RunFrame();
						}
					}
					stopRenderingTask.TrySetResult(true);
					stopRenderingTask = null;
				});
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			return app;
		}

		internal static TaskCompletionSource<bool> stopRenderingTask;
		internal static Task StopRendering()
		{
			stopRenderingTask = new TaskCompletionSource<bool>();
			return stopRenderingTask.Task;
		}

		public void Pause()
		{
			paused = true;
		}

		public void Resume()
		{
			paused = false;
		}

		public static void CopyEmbeddedResourceToLocalFolder(string embeddedResource, string destFileName)
		{
			if (FileExistsInLocalFolder(destFileName))
				return;

			var file = ApplicationData.Current.LocalFolder.CreateFileAsync(destFileName).GetAwaiter().GetResult();
			using (var fileStream = file.OpenStreamForWriteAsync().GetAwaiter().GetResult())
			using (var embeddedSteam = typeof(UrhoSurface).GetTypeInfo().Assembly.GetManifestResourceStream(embeddedResource))
			{
				embeddedSteam.CopyTo(fileStream);
			}
		}

		public static void CopyContentFileToLocalFolder(string msappx)
		{
			string file = Path.GetFileName(msappx);
			if (FileExistsInLocalFolder(file))
				return;

			try
			{
				var storageFile = StorageFile.GetFileFromApplicationUriAsync(new Uri(msappx)).GetAwaiter().GetResult();
				storageFile.CopyAsync(ApplicationData.Current.LocalFolder).GetAwaiter().GetResult();
			}
			catch (Exception exc)
			{
				throw new Exception($"Assets pak '{msappx}' not found, make sure Build Action set to 'Content'", exc);
			}
		}

		static bool FileExistsInLocalFolder(string file)
		{
			try
			{
				ApplicationData.Current.LocalFolder.GetFileAsync(file).GetAwaiter().GetResult();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
