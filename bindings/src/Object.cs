using System;
using System.Runtime.InteropServices;

namespace Urho {

	public partial class Object : RefCounted {

		//
		// GetSubsystem helpers
		//
		public ResourceCache ResourceCache => GetSubsystem (ResourceCache.GetTypeStatic ());
		public Network Network => GetSubsystem (Netowkr.GetTypeStatic ());
		public Time Time => GetSubsystem (Time.GetTypeStatic ());
		public WorkQueue WorkQueue => GetSubsystem (WorkQueue.GetTypeStatic ());
		public Profiler Profiler => GetSubsystem (Profiler.GetTypeStatic ());
		public FileSystem FileSystem => GetSubsystem (FileSystem.GetTypeStatic ());
		public Log Log => GetSubsystem (Log.GetTypeStatic ());
		public Input Input => GetSubsystem (Input.GetTypeStatic ());
		public Audio Audio => GetSubsystem (Audio.GetTypeStatic ());
		public UI UI => GetSubsystem (UI.GetTypeStatic ());
		public Graphics Graphics => GetSubsystem (Graphics.GetTypeStatic ());
		public Renderer Renderer => GetSubsystem (Renderer.GetTypeStatic ());


	}
}
