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
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Urho.IO;
using Urho.Audio;
using Urho.Resources;
using Urho.Actions;
using Urho.Gui;

namespace Urho {
	
	public partial class Application {

		// references needed to prevent GC from collecting callbacks passed to native code
		static ActionIntPtr setupCallback;
		static ActionIntPtr startCallback;
		static ActionIntPtr stopCallback;

		static readonly List<Action> actionsToDipatch = new List<Action>();

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ActionIntPtr (IntPtr value);

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		static extern IntPtr ApplicationProxy_ApplicationProxy (IntPtr contextHandle, ActionIntPtr setup, ActionIntPtr start, ActionIntPtr stop, string args, IntPtr externalWindow);

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

		public Application() : this(new Context(), null) {}

		public Application(ApplicationOptions options) : this(new Context(), options) {}

		/// <summary>
		/// Supports the simple style with callbacks
		/// </summary>
		Application (Context context, ApplicationOptions options = null) : base (UrhoObjectFlag.Empty)
		{
			if (context == null)
				throw new ArgumentNullException (nameof(context));

			if (context.Refs() < 1)
				context.AddRef();

			//keep references to callbacks (supposed to be passed to native code) as long as the App is alive
			setupCallback = ProxySetup;
			startCallback = ProxyStart;
			stopCallback = ProxyStop;

			Options = options ?? new ApplicationOptions(assetsFolder: null);
			handle = ApplicationProxy_ApplicationProxy (context.Handle, setupCallback, startCallback, stopCallback, Options.ToString(), Options.ExternalWindow);
			Runtime.RegisterObject (this);

			Engine.SubscribeToUpdate(HandleUpdate);
		}

		public IntPtr Handle => handle;

		/// <summary>
		/// Application options
		/// </summary>
		public ApplicationOptions Options { get; private set; }

		/// <summary>
		/// Frame update event
		/// </summary>
		public event Action<UpdateEventArgs> Update;

		/// <summary>
		/// Waits given _game_ time.
		/// </summary>
		public static Task Delay(float durationSec)
		{
			var tcs = new TaskCompletionSource<bool>();
			var state = Current.ActionManager.AddAction(new Sequence(new DelayTime(durationSec), new CallFunc(() => tcs.TrySetResult(true))), null);
			return tcs.Task;
		}

		/// <summary>
		/// Invoke actions in the Main Thread (the next Update call)
		/// </summary>
		public static void InvokeOnMain(Action action)
		{
			lock (actionsToDipatch)
			{
				actionsToDipatch.Add(action);
			}
		}

		static Application GetApp(IntPtr h) => Runtime.LookupObject<Application>(h);

		void HandleUpdate(UpdateEventArgs args)
		{
			var timeStep = args.TimeStep;
			Update?.Invoke(args);
			ActionManager.Update(timeStep);
			OnUpdate(timeStep);

			if (actionsToDipatch.Count > 0)
			{
				lock (actionsToDipatch)
				{
					foreach (var action in actionsToDipatch)
						action();
					actionsToDipatch.Clear();
				}
			}
		}

		[MonoPInvokeCallback(typeof(ActionIntPtr))]
		static void ProxySetup (IntPtr h)
		{
			Current = GetApp(h);
			CurrentContext = Current.Context;
			Current.Setup ();
		}

		[MonoPInvokeCallback(typeof(ActionIntPtr))]
		static void ProxyStart (IntPtr h)
		{
			Runtime.Initialize();
			GetApp (h).Start ();
			Started?.Invoke();
			MakeOnTop();
		}

		private static async void MakeOnTop()
		{
#if DESKTOP
			await Task.Delay(1000);
			var title = Current.Graphics.WindowTitle;
			var handle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
			SetForegroundWindow((int)handle);
#endif
		}

		[DllImport("User32.dll")]
		public static extern Int32 SetForegroundWindow(int hWnd);

		[MonoPInvokeCallback(typeof(ActionIntPtr))]
		static void ProxyStop (IntPtr h)
		{
			UrhoPlatformInitializer.Initialized = false;
			var context = Current.Context;
			GetApp (h).Stop ();
			Runtime.Cleanup();
#if !IOS && !DESKTOP
			if (context.Refs() > 0)
				context.ReleaseRef();
#endif
			context.Dispose();
			Current = null;
			Stoped?.Invoke();
		}

		public static void StopCurrent()
		{
			Current.Engine.Exit ();
#if IOS
			ProxyStop(Current.Handle);
#endif
		}

		protected virtual void Setup () {}

		public static event Action Started;
		protected virtual void Start () {}

		public static event Action Stoped;
		protected virtual void Stop () {}

		protected virtual void OnUpdate(float timeStep) { }

		internal ActionManager ActionManager { get; } = new ActionManager();


		[DllImport(Consts.NativeImport, EntryPoint = "Urho_GetPlatform", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr GetPlatform();

		static Platforms platform;

		public static Platforms Platform {
			get {
				if (platform == Platforms.Unknown)
					platform = PlatformsMap.FromString(Marshal.PtrToStringAnsi(GetPlatform()));
				return platform;
			}
		}

		//
		// GetSubsystem helpers
		//
		ResourceCache resourceCache;
		public ResourceCache ResourceCache {
			get {
				if (resourceCache == null)
					resourceCache = new ResourceCache (UrhoObject_GetSubsystem (handle, ResourceCache.TypeStatic.Code));
				return resourceCache;
			}
		}

		UrhoConsole console;
		public UrhoConsole Console {
			get {
				if (console == null)
					console = new UrhoConsole (UrhoObject_GetSubsystem (handle, UrhoConsole.TypeStatic.Code));
				return console;
			}
		}
		
		Urho.Network.Network network;
		public Urho.Network.Network Network {
			get {
				if (network == null)
					network = new Urho.Network.Network (UrhoObject_GetSubsystem (handle, Urho.Network.Network.TypeStatic.Code));
				return network;
			}
		}
		
		Time time;
		public Time Time {
			get {
				if (time == null)
					time = new Time (UrhoObject_GetSubsystem (handle, Time.TypeStatic.Code));
				return time;
			}
		}
		
		WorkQueue workQueue;
		public WorkQueue WorkQueue {
			get {
				if (workQueue == null)
					workQueue = new WorkQueue (UrhoObject_GetSubsystem (handle, WorkQueue.TypeStatic.Code));
				return workQueue;
			}
		}
		
		Profiler profiler;
		public Profiler Profiler {
			get {
				if (profiler == null)
					profiler = new Profiler (UrhoObject_GetSubsystem (handle, Profiler.TypeStatic.Code));
				return profiler;
			}
		}
		
		FileSystem fileSystem;
		public FileSystem FileSystem {
			get {
				if (fileSystem == null)
					fileSystem = new FileSystem (UrhoObject_GetSubsystem (handle, FileSystem.TypeStatic.Code));
				return fileSystem;
			}
		}
		
		Log log;
		public Log Log {
			get {
				if (log == null)
					log = new Log (UrhoObject_GetSubsystem (handle, Log.TypeStatic.Code));
				return log;
			}
		}
		
		Input input;
		public Input Input {
			get {
				if (input == null)
					input = new Input (UrhoObject_GetSubsystem (handle, Input.TypeStatic.Code));
				return input;
			}
		}
		
		Urho.Audio.Audio audio;
		public Urho.Audio.Audio Audio {
			get {
				if (audio == null)
					audio = new Audio.Audio (UrhoObject_GetSubsystem (handle, Urho.Audio.Audio.TypeStatic.Code));
				return audio;
			}
		}
		
		UI uI;
		public UI UI {
			get {
				if (uI == null)
					uI = new UI (UrhoObject_GetSubsystem (handle, UI.TypeStatic.Code));
				return uI;
			}
		}
		
		Graphics graphics;
		public Graphics Graphics {
			get {
				if (graphics == null)
					graphics = new Graphics (UrhoObject_GetSubsystem (handle, Graphics.TypeStatic.Code));
				return graphics;
			}
		}
		
		Renderer renderer;
		public Renderer Renderer {
			get {
				if (renderer == null)
					renderer = new Renderer (UrhoObject_GetSubsystem (handle, Renderer.TypeStatic.Code));
				return renderer;
			}
		}

		[DllImport (Consts.NativeImport, CallingConvention=CallingConvention.Cdecl)]
		extern static IntPtr Application_GetEngine (IntPtr handle);
		Engine engine;

		public Engine Engine {
			get {
				if (engine == null)
					engine = new Engine (Application_GetEngine (handle));
				return engine;
			}
		}

		public static T CreateInstance<T>(ApplicationOptions options = null) where T : Application
		{
			return (T)CreateInstance(typeof (T), options);
		}

		public static Application CreateInstance(Type applicationType, ApplicationOptions options = null)
		{
			var ctors = applicationType.GetTypeInfo().DeclaredConstructors.ToArray();

			var ctorWithOptions = ctors.FirstOrDefault(c => c.GetParameters().Length == 1 && c.GetParameters()[0].ParameterType == typeof (ApplicationOptions));
			if (ctorWithOptions != null)
			{
				return (Application) Activator.CreateInstance(applicationType, options);
			}

			var ctorDefault = ctors.FirstOrDefault(c => c.GetParameters().Length == 0);
			if (ctorDefault != null)
			{
				return (Application) Activator.CreateInstance(applicationType);
			}

			throw new InvalidOperationException($"{applicationType} doesn't have parameterless constructor.");
		}
	}
}
