using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

		public Application Run(Type appType, string customAssetsPak = null)
		{
			return Run(appType, new[] { customAssetsPak });
		}

		/// <param name="customAssetsPak">all assets must be *.pak files (use PackageTool.exe).</param>
		public Application Run(Type appType, string[] customAssetsPaks = null, ApplicationOptions opt = null)
		{
			stop = false;
			paused = false;
			inited = false;
			Sdl.SetMainReady();

			if (opt == null)
				opt = new ApplicationOptions(assetsFolder: null);

			var pakFiles = new List<string>();
			foreach (var resourcePath in customAssetsPaks ?? new string[0])
			{
				string url = "";
				if (!resourcePath.StartsWith("ms-"))
					url = "ms-appx:///" + resourcePath;
				CopyContentFileToLocalFolder(url);
				pakFiles.Add(Path.GetFileNameWithoutExtension(url));
			}
			opt.ResourcePaths = pakFiles.ToArray();
			opt.ResourcePrefixPaths = new[] { ApplicationData.Current.LocalFolder.Path  };

			CopyEmbeddedResourceToLocalFolder("Urho.CoreData.pak", "CoreData.pak");
			var app = (Application)Activator.CreateInstance(appType, opt);
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
