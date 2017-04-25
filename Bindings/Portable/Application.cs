//
// Support for bubbling up to C# the virtual methods calls for Setup, Start and Stop in Application
//
// This is done by using an ApplicationProxy in C++ that bubbles up
//
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Urho.IO;
using Urho.Audio;
using Urho.Resources;
using Urho.Actions;
using Urho.Gui;
using System.Threading;

namespace Urho
{

	[PreserveAttribute(AllMembers = true)]
	public partial class Application
	{
		// references needed to prevent GC from collecting callbacks passed to native code
		static ActionIntPtr setupCallback;
		static ActionIntPtr startCallback;
		static ActionIntPtr stopCallback;

		static TaskCompletionSource<bool> exitTask;
		static TaskCompletionSource<bool> waitFrameEndTaskSource;
		static int renderThreadId = -1;
		static bool isExiting = false;
		static List<Action> staticActionsToDispatch;
		static List<DelayState> delayTasks;
		List<Action> actionsToDispatch = new List<Action>();

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ActionIntPtr(IntPtr value);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr ApplicationProxy_ApplicationProxy(IntPtr contextHandle, ActionIntPtr setup, ActionIntPtr start, ActionIntPtr stop, string args, IntPtr externalWindow);

		static Application current;
		public static Application Current
		{
			get
			{
				if (current == null)
					throw new InvalidOperationException("The application is not configured yet");
				return current;
			}
			private set { current = value; }
		}

		public static bool HasCurrent => current != null;

		static Context currentContext;
		public static Context CurrentContext
		{
			get
			{
				if (currentContext == null)
					throw new InvalidOperationException("The application is not configured yet");
				return currentContext;
			}
			private set { currentContext = value; }
		}

		// see Drawable2D.h:66
		public const float PixelSize = 0.01f;

		[Preserve]
		public Application(ApplicationOptions options) : this(new Context(), options) { }

		Application(Context context, ApplicationOptions options = null) : base(UrhoObjectFlag.Empty)
		{
			//Workbooks specific:
			CancelActiveActionsOnStop = this is SimpleApplication;
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			//keep references to callbacks (supposed to be passed to native code) as long as the App is alive
			setupCallback = ProxySetup;
			startCallback = ProxyStart;
			stopCallback = ProxyStop;

#if !ANDROID
			if (context.Refs() < 1)
				context.AddRef();
#endif

			Options = options ?? new ApplicationOptions(assetsFolder: null);
			handle = ApplicationProxy_ApplicationProxy(context.Handle, setupCallback, startCallback, stopCallback, Options.ToString(), Options.ExternalWindow);
			Runtime.RegisterObject(this);
		}

		public bool IsClosed { get; private set; }

		public IntPtr Handle => handle;

		public IUrhoSurface UrhoSurface { get; internal set; }

		/// <summary>
		/// Application options
		/// </summary>
		public ApplicationOptions Options { get; private set; }

		/// <summary>
		/// True means Urho3D is somewhere between E_BEGINFRAME and E_ENDFRAME in Engine::RunFrame()
		/// </summary>
		public bool IsFrameRendering { get; private set; }

		/// <summary>
		/// Frame update event
		/// </summary>
		public event Action<UpdateEventArgs> Update;

		/// <summary>
		/// Invoke actions in the Main Thread (the next Update call)
		/// </summary>
		public static void InvokeOnMain(Action action)
		{
			if (!HasCurrent)
			{
				if (staticActionsToDispatch == null)
					staticActionsToDispatch = new List<Action>();
				lock (staticActionsToDispatch)
					staticActionsToDispatch.Add(action);

				return;
			}

			var actions = Current.actionsToDispatch;
			lock (actions)
			{
				actions.Add(action);
			}
		}

		/// <summary>
		/// Dispatch to OnUpdate
		/// </summary>
		public static ConfiguredTaskAwaitable<bool> ToMainThreadAsync()
		{
			var tcs = new TaskCompletionSource<bool>();
			InvokeOnMain(() => tcs.TrySetResult(true));
			return tcs.Task.ConfigureAwait(false);
		}

		/// <summary>
		/// Invoke actions in the Main Thread (the next Update call)
		/// </summary>
		public static Task<bool> InvokeOnMainAsync(Action action)
		{
			var tcs = new TaskCompletionSource<bool>();
			InvokeOnMain(() =>
				{
					action?.Invoke();
					tcs.TrySetResult(true);
				});
			return tcs.Task;
		}

		public Task Delay(float seconds)
		{
			var tcs = new TaskCompletionSource<bool>();
			if (delayTasks == null)
				delayTasks = new List<DelayState>();
			delayTasks.Add(new DelayState { Duration = seconds, Task = tcs });
			return tcs.Task;
		}

		public Task Delay(TimeSpan timeSpan) => Delay((float)timeSpan.TotalSeconds);

		static Application GetApp(IntPtr h) => Runtime.LookupObject<Application>(h);

		void Time_FrameStarted(FrameStartedEventArgs args)
		{
			IsFrameRendering = true;
		}

		void Time_FrameEnded(FrameEndedEventArgs args)
		{
			IsFrameRendering = false;
			if (waitFrameEndTaskSource != null)
				waitFrameEndTaskSource?.TrySetResult(true);
		}

		void HandleUpdate(UpdateEventArgs args)
		{
			var timeStep = args.TimeStep;
			Update?.Invoke(args);
			ActionManager.Update(timeStep);
			OnUpdate(timeStep);

			if (staticActionsToDispatch != null)
			{
				lock (staticActionsToDispatch)
					foreach (var action in staticActionsToDispatch)
						action();
				staticActionsToDispatch = null;
			}

			if (actionsToDispatch.Count > 0)
			{
				lock (actionsToDispatch)
				{
					foreach (var action in actionsToDispatch)
						action();
					actionsToDispatch.Clear();
				}
			}

			if (delayTasks != null)
			{
				for (int i = 0; i < delayTasks.Count; i++)
				{
					var task = delayTasks[i];
					task.Duration -= timeStep;
					if (task.Duration <= 0)
					{
						task.Task.TrySetResult(true);
						delayTasks.RemoveAt(i);
					}
				}
			}
		}

		[MonoPInvokeCallback(typeof(ActionIntPtr))]
		static void ProxySetup(IntPtr h)
		{
			isExiting = false;
			Runtime.Setup();
			Current = GetApp(h);
			CurrentContext = Current.Context;
			Current.Setup();
		}

		[MonoPInvokeCallback(typeof(ActionIntPtr))]
		static void ProxyStart(IntPtr h)
		{
			Runtime.Start();
			Current = GetApp(h);
			Current.SubscribeToAppEvents();
#if WINDOWS_UWP
			// UWP temp workaround:
			var text = new Text();
			text.SetFont(CoreAssets.Fonts.AnonymousPro, 1);
			text.Value = " ";
			Current.UI.Root.AddChild(text);
#endif
			Current.Start();
			Started?.Invoke();

#if ANDROID
			renderThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
		}

		public static bool CancelActiveActionsOnStop { get; set; }

		[MonoPInvokeCallback(typeof(ActionIntPtr))]
		static void ProxyStop(IntPtr h)
		{
			isExiting = true;
			if (CancelActiveActionsOnStop)
				Current.ActionManager.CancelActiveActions();
			LogSharp.Debug("ProxyStop");
			UrhoPlatformInitializer.Initialized = false;
			var context = Current.Context;
			var app = GetApp(h);
			app.IsClosed = true;
			app.Stop();
			LogSharp.Debug("ProxyStop: Runtime.Cleanup");
			Runtime.Cleanup();
			LogSharp.Debug("ProxyStop: Disposing context");
			context.Dispose();
			Current = null;
			Stopped?.Invoke();
			LogSharp.Debug("ProxyStop: end");
			exitTask?.TrySetResult(true);
		}

		void SubscribeToAppEvents()
		{
			Engine.SubscribeToUpdate(HandleUpdate);
			Time.FrameStarted += Time_FrameStarted;
			Time.FrameEnded += Time_FrameEnded;
		}

		SemaphoreSlim stopSemaphore = new SemaphoreSlim(1);

		internal static async Task StopCurrent()
		{
			if (current == null && !current.IsActive)
				return;

			//Current.Engine.PauseMinimized = true;
			Current.Input.Enabled = false;

			isExiting = true;
#if IOS
			iOS.UrhoSurface.StopRendering(current);
#endif

#if WINDOWS_UWP && !UWP_HOLO
			UWP.UrhoSurface.StopRendering().Wait();
#endif
#if ANDROID
			if (Current.UrhoSurface?.IsAlive == false)
			{
				Current.Engine.Exit();
			}
			else if (System.Threading.Thread.CurrentThread.ManagedThreadId != renderThreadId)
			{
				exitTask = new TaskCompletionSource<bool>();
				InvokeOnMainAsync(() => Current.Engine.Exit()).Wait();
				Current.UrhoSurface?.Remove();
				Current.UrhoSurface = null;
				await exitTask.Task;//.Wait();
			}
			else
			{
				Current.Engine.Exit();
				new Android.OS.Handler(Android.OS.Looper.MainLooper).PostAtFrontOfQueue(() => {
					Current.UrhoSurface?.Remove();
					Current.UrhoSurface = null;
				});
			}
#else
			LogSharp.Debug($"StopCurrent: Current.IsFrameRendering={Current.IsFrameRendering}");
			if (Current.IsFrameRendering)// && !Current.Engine.PauseMinimized)
			{
				waitFrameEndTaskSource = new TaskCompletionSource<bool>();
				await waitFrameEndTaskSource.Task;
				LogSharp.Debug($"StopCurrent: waitFrameEndTaskSource awaited");
				waitFrameEndTaskSource = null;
			}
			LogSharp.Debug($"StopCurrent: Engine.Exit");
			Current.Engine.Exit();
#endif
#if IOS || WINDOWS_UWP
#if DESKTOP
		if (Current.Options.DelayedStart)
#endif
			ProxyStop(Current.Handle);
#endif
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
		}

		public bool IsExiting => isExiting || Runtime.IsClosing || Engine.Exiting;

		public bool IsActive => !IsClosed && !IsDeleted && !Engine.IsDeleted && !IsExiting;

		public async Task Exit()
		{
			try
			{
				await stopSemaphore.WaitAsync();
				if (!IsActive)
					return;

				await StopCurrent();
			}
			finally
			{
				stopSemaphore.Release();
			}
		}

		protected override bool AllowNativeDelete => false;

		protected virtual void Setup() { }

		public static event Action Started;
		protected virtual void Start() { }

		public static event Action Stopped;
		protected virtual void Stop() { }

		protected virtual void OnUpdate(float timeStep) { }

		internal ActionManager ActionManager { get; } = new ActionManager();


		[DllImport(Consts.NativeImport, EntryPoint = "Urho_GetPlatform", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr GetPlatform();

		static Platforms platform;

		public static Platforms Platform
		{
			get
			{
				Runtime.Validate(typeof(Application));
				if (platform == Platforms.Unknown)
					platform = PlatformsMap.FromString(Marshal.PtrToStringAnsi(GetPlatform()));
				return platform;
			}
		}

		//
		// GetSubsystem helpers
		//
		ResourceCache resourceCache;
		public ResourceCache ResourceCache
		{
			get
			{
				Runtime.Validate(typeof(Application));
				if (resourceCache == null)
					resourceCache = new ResourceCache(UrhoObject_GetSubsystem(handle, ResourceCache.TypeStatic.Code));
				return resourceCache;
			}
		}

		UrhoConsole console;
		public UrhoConsole Console
		{
			get
			{
				Runtime.Validate(typeof(Application));
				if (console == null)
					console = new UrhoConsole(UrhoObject_GetSubsystem(handle, UrhoConsole.TypeStatic.Code));
				return console;
			}
		}

		Urho.Network.Network network;
		public Urho.Network.Network Network
		{
			get
			{
				Runtime.Validate(typeof(Application));
				if (network == null)
					network = new Urho.Network.Network(UrhoObject_GetSubsystem(handle, Urho.Network.Network.TypeStatic.Code));
				return network;
			}
		}

		Time time;
		public Time Time
		{
			get
			{
				Runtime.Validate(typeof(Application));
				if (time == null)
					time = new Time(UrhoObject_GetSubsystem(handle, Time.TypeStatic.Code));
				return time;
			}
		}

		WorkQueue workQueue;
		public WorkQueue WorkQueue
		{
			get
			{
				Runtime.Validate(typeof(Application));
				if (workQueue == null)
					workQueue = new WorkQueue(UrhoObject_GetSubsystem(handle, WorkQueue.TypeStatic.Code));
				return workQueue;
			}
		}

		Profiler profiler;
		public Profiler Profiler
		{
			get
			{
				Runtime.Validate(typeof(Application));
				if (profiler == null)
					profiler = new Profiler(UrhoObject_GetSubsystem(handle, Profiler.TypeStatic.Code));
				return profiler;
			}
		}

		FileSystem fileSystem;
		public FileSystem FileSystem
		{
			get
			{
				Runtime.Validate(typeof(Application));
				if (fileSystem == null)
					fileSystem = new FileSystem(UrhoObject_GetSubsystem(handle, FileSystem.TypeStatic.Code));
				return fileSystem;
			}
		}

		Log log;
		public Log Log
		{
			get
			{
				Runtime.Validate(typeof(Application));
				if (log == null)
					log = new Log(UrhoObject_GetSubsystem(handle, Log.TypeStatic.Code));
				return log;
			}
		}

		Input input;
		public Input Input
		{
			get
			{
				Runtime.Validate(typeof(Application));
				if (input == null)
					input = new Input(UrhoObject_GetSubsystem(handle, Input.TypeStatic.Code));
				return input;
			}
		}

		Urho.Audio.Audio audio;
		public Urho.Audio.Audio Audio
		{
			get
			{
				Runtime.Validate(typeof(Application));
				if (audio == null)
					audio = new Audio.Audio(UrhoObject_GetSubsystem(handle, Urho.Audio.Audio.TypeStatic.Code));
				return audio;
			}
		}

		UI uI;
		public UI UI
		{
			get
			{
				Runtime.Validate(typeof(Application));
				if (uI == null)
					uI = new UI(UrhoObject_GetSubsystem(handle, UI.TypeStatic.Code));
				return uI;
			}
		}

		Graphics graphics;
		public Graphics Graphics
		{
			get
			{
				Runtime.Validate(typeof(Application));
				if (graphics == null)
					graphics = new Graphics(UrhoObject_GetSubsystem(handle, Graphics.TypeStatic.Code));
				return graphics;
			}
		}

		Renderer renderer;
		public Renderer Renderer
		{
			get
			{
				Runtime.Validate(typeof(Application));
				if (renderer == null)
					renderer = new Renderer(UrhoObject_GetSubsystem(handle, Renderer.TypeStatic.Code));
				return renderer;
			}
		}

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		extern static IntPtr Application_GetEngine(IntPtr handle);
		Engine engine;

		public Engine Engine
		{
			get
			{
				if (engine == null)
					engine = new Engine(Application_GetEngine(handle));
				return engine;
			}
		}

		public static T CreateInstance<T>(ApplicationOptions options = null) where T : Application
		{
			return (T)CreateInstance(typeof(T), options);
		}

		public static Application CreateInstance(Type applicationType, ApplicationOptions options = null)
		{
			try
			{
				return (Application)Activator.CreateInstance(applicationType, options);
			}
			catch (Exception exc)
			{
				throw new InvalidOperationException($"Constructor {applicationType}(ApplicationOptions) was not found.", exc);
			}
		}

		internal static void ThrowUnhandledException(Exception exc)
		{
			string[] errorsToSkip =
				{
					"Could not initialize audio output.",
					"Failed to create input layout for shader",
					"Failed to create texture"
				};
			foreach (var item in errorsToSkip)
				if (exc.Message.StartsWith(item))
					return;

			var args = new UnhandledExceptionEventArgs(exc);
			UnhandledException?.Invoke(null, args);
			if (!args.Handled)
				throw exc;
		}

		public static event EventHandler<UnhandledExceptionEventArgs> UnhandledException;

		class DelayState
		{
			public float Duration { get; set; }
			public TaskCompletionSource<bool> Task { get; set; }
		}
	}

	public interface IUrhoSurface
	{
		void Remove();
		bool IsAlive { get; }
	}

	public class UnhandledExceptionEventArgs : EventArgs
	{
		public Exception Exception { get; private set; }
		public bool Handled { get; set; }

		public UnhandledExceptionEventArgs(Exception exception)
		{
			Exception = exception;
		}
	}
}
