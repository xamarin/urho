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
		static object invokerLock;
		static List<Action> invokeOnMain = new List<Action> ();
		public delegate void ActionIntPtr (IntPtr value);
		
		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		extern static IntPtr ApplicationProxy_ApplicationProxy (IntPtr contextHandle, ActionIntPtr setup, ActionIntPtr start, ActionIntPtr stop);

		//
		// Supports the simple style with callbacks
		//
		public Application (Context context, ActionIntPtr setup, ActionIntPtr start, ActionIntPtr stop) : base (UrhoObjectFlag.Empty)
		{
			if (context == null)
				throw new ArgumentNullException ("context");
			
			handle = ApplicationProxy_ApplicationProxy (context.Handle, setup, start, stop);
			Runtime.RegisterObject (this);
		}

		static public void InvokeOnMain (Action action)
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
		
		public Application (Context context) : base (UrhoObjectFlag.Empty)
		{
			if (context == null)
				throw new ArgumentNullException ("context");
			handle = ApplicationProxy_ApplicationProxy (context.Handle, ProxySetup, ProxyStart, ProxyStop);
			Runtime.RegisterObject (this);
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
