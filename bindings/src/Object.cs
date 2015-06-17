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

	public partial class UrhoObject : RefCounted {

		//
		// GetSubsystem helpers
		//
		public ResourceCache ResourceCache => new Urho.ResourceCache (UrhoObject_GetSubsystem (handle, ResourceCache.TypeStatic));
		public Network Network => new Urho.Network (UrhoObject_GetSubsystem (handle, Network.TypeStatic));
		public Time Time => new Urho.Time (UrhoObject_GetSubsystem (handle, Time.TypeStatic));
		public WorkQueue WorkQueue => new Urho.WorkQueue (UrhoObject_GetSubsystem (handle, WorkQueue.TypeStatic));
		public Profiler Profiler => new Urho.Profiler (UrhoObject_GetSubsystem (handle, Profiler.TypeStatic));
		public FileSystem FileSystem => new Urho.FileSystem (UrhoObject_GetSubsystem (handle, FileSystem.TypeStatic));
		public Log Log => new Urho.Log (UrhoObject_GetSubsystem (handle, Log.TypeStatic));
		public Input Input => new Urho.Input (UrhoObject_GetSubsystem (handle, Input.TypeStatic));
		public Audio Audio => new Urho.Audio (UrhoObject_GetSubsystem (handle, Audio.TypeStatic));
		public UI UI => new Urho.UI (UrhoObject_GetSubsystem (handle, UI.TypeStatic));
		public Graphics Graphics => new Urho.Graphics (UrhoObject_GetSubsystem (handle, Graphics.TypeStatic));
		public Renderer Renderer => new Urho.Renderer (UrhoObject_GetSubsystem (handle, Renderer.TypeStatic));


		// Invoked by the subscribe methods
		static void ObjectCallback (IntPtr data, int stringHash, IntPtr variantMap)
		{
			GCHandle gch = GCHandle.FromIntPtr(data);
			Action<IntPtr> a = (Action<IntPtr>) gch.Target;
			a (variantMap);
		}
	}
}
