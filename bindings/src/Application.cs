//
// Support for bubbling up to C# the virtual methods calls for Setup, Start and Stop in Application
//
// This is done by using an ApplicationProxy in C++ that bubbles up
//
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Urho {
	
	public partial class Application {
		readonly ActionIntPtr setup;
		readonly ActionIntPtr start;
		readonly ActionIntPtr stop;
		static readonly object invokerLock = new object();
		static readonly List<System.Action> invokeOnMain = new List<System.Action> ();

		static bool subsribedToUpdate;
        static readonly Dictionary<ExecutionContextWithId, List<Action<UpdateEventArgs>>> UpdateSubscribers = new Dictionary<ExecutionContextWithId, List<Action<UpdateEventArgs>>>();

		static bool subsribedToSceneUpdate;
		static readonly Dictionary<ExecutionContextWithId, List<Action<SceneUpdateEventArgs>>> SceneUpdateSubscribers = new Dictionary<ExecutionContextWithId, List<Action<SceneUpdateEventArgs>>>();

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void ActionIntPtr (IntPtr value);
		
		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		extern static IntPtr ApplicationProxy_ApplicationProxy (IntPtr contextHandle, ActionIntPtr setup, ActionIntPtr start, ActionIntPtr stop);

		public static Application Current { get; private set; }

		/// <summary>
		/// Supports the simple style with callbacks
		/// </summary>
		public Application (Context context, ActionIntPtr setup, ActionIntPtr start, ActionIntPtr stop) : base (UrhoObjectFlag.Empty)
		{
			//keep references to callbacks as long as the App is alive
			this.setup = setup;
			this.start = start;
			this.stop = stop;

			if (context == null)
				throw new ArgumentNullException (nameof(context));

			handle = ApplicationProxy_ApplicationProxy (context.Handle, setup, start, stop);
			Runtime.RegisterObject (this);
			Current = this;
        }

		public Application(Context context) : this(context, ProxySetup, ProxyStart, ProxyStop) { }

		public static event Action<UpdateEventArgs> Update
		{
			add
			{
				UpdateSubscribers.AddToValueList(ExecutionContextWithId.FromCurrent(), value);
				if (!subsribedToUpdate)
				{
					//subscribe once on first top-level subscription
					Current.SubscribeToUpdate(args => ExecuteEventsInCapturedContexts(UpdateSubscribers, args));
					subsribedToUpdate = true;
				}
			}
			remove { UpdateSubscribers.RemoveFromAllValueLists(value); }
			//O(n*k) complexity, but we have O(k) for invocation and O(1) for insertion
		}

		public static event Action<SceneUpdateEventArgs> SceneUpdate
		{
			add
			{
				SceneUpdateSubscribers.AddToValueList(ExecutionContextWithId.FromCurrent(), value);
				if (!subsribedToSceneUpdate)
				{
					Current.SubscribeToSceneUpdate(args => ExecuteEventsInCapturedContexts(SceneUpdateSubscribers, args));
					subsribedToSceneUpdate = true;
				}
			}
			remove { SceneUpdateSubscribers.RemoveFromAllValueLists(value); }
		}

		/// <summary>
		/// The event will be dispatched to all subscribers in the context they used on subscription
		/// if multiply subscribers use the same context - event invocation will be aggregated
		/// </summary>
		private static void ExecuteEventsInCapturedContexts<TEventArgs>(Dictionary<ExecutionContextWithId, List<Action<TEventArgs>>> subscribers, TEventArgs args)
		{
			foreach (var subscriber in subscribers)
			{
				var originalContext = subscriber.Key;
				originalContext.Dispatch(() => subscriber.Value.ForEach(sub => sub(args)));
			}
		}

		static public void InvokeOnMain (System.Action action)
		{
			lock (invokerLock)
				invokeOnMain.Add (action);
		}

		void RunOnMainThread ()
		{
			lock (invokerLock){
				var count = invokeOnMain.Count;
				if (count > 0){
					foreach (var a in invokeOnMain)
						a ();
					invokeOnMain.Clear ();
				}
			}
		}
		

		static Application GetApp (IntPtr h)
		{
			return Runtime.LookupObject<Application> (h);
		}
		
		static void ProxySetup (IntPtr h)
		{
			GetApp (h).Setup ();
		}

		static void ProxyStart (IntPtr h)
		{
			GetApp (h).Start ();
		}

		static void ProxyStop (IntPtr h)
		{
			GetApp (h).Stop ();
		}
		
		public virtual void Setup ()
		{
			System.Console.WriteLine ("Your application does not override the Setup method, where you configure the engine");
		}

		public virtual void Start ()
		{
			System.Console.WriteLine ("Your application does not override the Start method, where you have access to UI elements");
		}

		public virtual void Stop ()
		{
		}
		

		//
		// GetSubsystem helpers
		//
		ResourceCache resourceCache;
		public ResourceCache ResourceCache {
			get {
				if (resourceCache == null)
					resourceCache = new Urho.ResourceCache (UrhoObject_GetSubsystem (handle, ResourceCache.TypeStatic.Code));
				return resourceCache;
			}
		}

		UrhoConsole console;
		public UrhoConsole Console {
			get {
				if (console == null)
					console = new Urho.UrhoConsole (UrhoObject_GetSubsystem (handle, UrhoConsole.TypeStatic.Code));
				return console;
			}
		}
		
		Network network;
		public Network Network {
			get {
				if (network == null)
					network = new Urho.Network (UrhoObject_GetSubsystem (handle, Network.TypeStatic.Code));
				return network;
			}
		}
		
		Time time;
		public Time Time {
			get {
				if (time == null)
					time = new Urho.Time (UrhoObject_GetSubsystem (handle, Time.TypeStatic.Code));
				return time;
			}
		}
		
		WorkQueue workQueue;
		public WorkQueue WorkQueue {
			get {
				if (workQueue == null)
					workQueue = new Urho.WorkQueue (UrhoObject_GetSubsystem (handle, WorkQueue.TypeStatic.Code));
				return workQueue;
			}
		}
		
		Profiler profiler;
		public Profiler Profiler {
			get {
				if (profiler == null)
					profiler = new Urho.Profiler (UrhoObject_GetSubsystem (handle, Profiler.TypeStatic.Code));
				return profiler;
			}
		}
		
		FileSystem fileSystem;
		public FileSystem FileSystem {
			get {
				if (fileSystem == null)
					fileSystem = new Urho.FileSystem (UrhoObject_GetSubsystem (handle, FileSystem.TypeStatic.Code));
				return fileSystem;
			}
		}
		
		Log log;
		public Log Log {
			get {
				if (log == null)
					log = new Urho.Log (UrhoObject_GetSubsystem (handle, Log.TypeStatic.Code));
				return log;
			}
		}
		
		Input input;
		public Input Input {
			get {
				if (input == null)
					input = new Urho.Input (UrhoObject_GetSubsystem (handle, Input.TypeStatic.Code));
				return input;
			}
		}
		
		Audio audio;
		public Audio Audio {
			get {
				if (audio == null)
					audio = new Urho.Audio (UrhoObject_GetSubsystem (handle, Audio.TypeStatic.Code));
				return audio;
			}
		}
		
		UI uI;
		public UI UI {
			get {
				if (uI == null)
					uI = new Urho.UI (UrhoObject_GetSubsystem (handle, UI.TypeStatic.Code));
				return uI;
			}
		}
		
		Graphics graphics;
		public Graphics Graphics {
			get {
				if (graphics == null)
					graphics = new Urho.Graphics (UrhoObject_GetSubsystem (handle, Graphics.TypeStatic.Code));
				return graphics;
			}
		}
		
		Renderer renderer;
		public Renderer Renderer {
			get {
				if (renderer == null)
					renderer = new Urho.Renderer (UrhoObject_GetSubsystem (handle, Renderer.TypeStatic.Code));
				return renderer;
			}
		}

		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		extern static IntPtr Application_GetEngine (IntPtr handle);
		Engine engine;
		public Engine Engine {
			get {
				if (engine == null)
					engine = new Urho.Engine (Application_GetEngine (handle));
				return engine;
			}
		}
	}
}
