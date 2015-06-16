//
// Urho's Object C# sugar
//
// Authors:
//   Miguel de Icaza (miguel@xamarin.com)
//
// Copyrigh 2015 Xamarin INc
//

using System;
using System.Runtime.InteropServices;

namespace Urho {

	public partial class Object : RefCounted {

		//
		// GetSubsystem helpers
		//
		public ResourceCache ResourceCache => new Urho.ResourceCache (Object_GetSubsystem (handle, ResourceCache.TypeStatic));
		public Network Network => new Urho.Network (Object_GetSubsystem (handle, Network.TypeStatic));
		public Time Time => new Urho.Time (Object_GetSubsystem (handle, Time.TypeStatic));
		public WorkQueue WorkQueue => new Urho.WorkQueue (Object_GetSubsystem (handle, WorkQueue.TypeStatic));
		public Profiler Profiler => new Urho.Profiler (Object_GetSubsystem (handle, Profiler.TypeStatic));
		public FileSystem FileSystem => new Urho.FileSystem (Object_GetSubsystem (handle, FileSystem.TypeStatic));
		public Log Log => new Urho.Log (Object_GetSubsystem (handle, Log.TypeStatic));
		public Input Input => new Urho.Input (Object_GetSubsystem (handle, Input.TypeStatic));
		public Audio Audio => new Urho.Audio (Object_GetSubsystem (handle, Audio.TypeStatic));
		public UI UI => new Urho.UI (Object_GetSubsystem (handle, UI.TypeStatic));
		public Graphics Graphics => new Urho.Graphics (Object_GetSubsystem (handle, Graphics.TypeStatic));
		public Renderer Renderer => new Urho.Renderer (Object_GetSubsystem (handle, Renderer.TypeStatic));

		
	}
}
