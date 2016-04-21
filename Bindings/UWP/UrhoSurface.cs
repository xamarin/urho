using System;
using System.IO;
using System.Runtime.InteropServices;
using Windows.Storage;
using Windows.System.Threading;
using Windows.UI.Xaml.Controls;

namespace Urho.UWP
{
	public class UrhoSurface : SwapChainPanel
	{
		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern void SDL_SetMainReady();

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern int SDL_SendWindowEvent(IntPtr window, Byte windowevent, int data1, int data2);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr Graphics_GetSdlWindow(IntPtr graphics);

		bool paused;
		bool stop;
		bool inited;
		
		public TGame Run<TGame>(string customAssetsPak = null) where TGame : Urho.Application
		{
			stop = false;
			paused = false;
			inited = false;
			SDL_SetMainReady();
			ApplicationOptions options = new ApplicationOptions(assetsFolder: "Data");
			if (!string.IsNullOrEmpty(customAssetsPak))
			{
				options.ResourcePackagesPaths = new[] { Path.GetFileName(customAssetsPak) };
				CopyContentFileToLocalFolder(customAssetsPak);
			}
			CopyContentFileToLocalFolder("ms-appx:///Urho/CoreData.pak");
			var app = (TGame)Activator.CreateInstance(typeof(TGame), options);
			app.Run();
			var sdlWnd = Graphics_GetSdlWindow(app.Graphics.Handle);
			SDL_SendWindowEvent(sdlWnd, 5, (int)this.ActualWidth, (int)this.ActualHeight);
			inited = true;
			ThreadPool.RunAsync(_ =>
				{
					while (!app.Engine.Exiting)
					{
						if (!paused)
						{
							app.Engine.RunFrame();
						}
					}
				});
			return app;
		}

		public void Pause()
		{
			paused = true;
		}

		public void Resume()
		{
			paused = false;
		}

		public void Stop()
		{
			stop = true;
		}

		public static void CopyContentFileToLocalFolder(string msappx)
		{
			string file = Path.GetFileName(msappx);
			try
			{
				//check if file exists
				ApplicationData.Current.LocalFolder.GetFileAsync(file).GetAwaiter().GetResult();
				return;
			}
			catch
			{
			}

			var storageFile = StorageFile.GetFileFromApplicationUriAsync(new Uri(msappx)).GetAwaiter().GetResult();
			storageFile.CopyAsync(ApplicationData.Current.LocalFolder).GetAwaiter().GetResult();
		}
	}
}
