//
// Runtime C# support
//
// Authors:
//   Miguel de Icaza (miguel@xamarin.com)
//
// Copyrigh 2015 Xamarin INc
//
using System;
using static System.Console;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Urho {
	
	public partial class Runtime {
		static Dictionary<IntPtr,WeakReference<RefCounted>> knownObjects = new Dictionary<IntPtr,WeakReference<RefCounted>> ();
		static IntPtr expecting;
		
		public static T LookupObject<T> (IntPtr ptr) where T:RefCounted
		{
			if (ptr == IntPtr.Zero)
				return null;

			// No locks are needed, because Urho code is only allowed to run in the 
			// UI thread, never anywhere else.
			WeakReference<RefCounted> wr;
			if (knownObjects.TryGetValue (ptr, out wr)){
				RefCounted ret;
				
				if (wr.TryGetTarget (out ret))
					return (T) ret;
			}
			
			try {
				// TODO: if this is a UrhoObject, instead of a plain RefCounted,
				// we should find the most derived type, by calling the Type method
				// on the provided instance, and then creating the most derived value.
				//
				// We just need to statically create the table of all those types
				
				// Create the instance.  We use "expecting" to avoid creating two
				// WeakReferences (one via this method, one via the Refcounted(IntPtr) constructor
				// which is our constructor that auto registers classes initialized manually from
				// pointers
				expecting = ptr;
				var o =  (T)Activator.CreateInstance (
					typeof(T), BindingFlags.Instance|BindingFlags.NonPublic, null, new object [] { ptr }, null);
				if (expecting != IntPtr.Zero)
					knownObjects [ptr] = new WeakReference<RefCounted> (o);
				expecting = IntPtr.Zero;
				return o;
			} catch {
				Console.WriteLine ($"Failed to instantiate a {typeof (T)} from {Environment.StackTrace}");
				throw;
			}
		}

		public static void UnregisterObject (IntPtr handle)
		{
			knownObjects.Remove (handle);
		}

		public static void RegisterObject (RefCounted r)
		{
			var rh = r.Handle;
			if (expecting == r.Handle)
				expecting = IntPtr.Zero;
			
			knownObjects [rh] = new WeakReference<RefCounted> (r);
		}

		static Dictionary<System.Type,int> hashDict;
		
		public static StringHash LookupStringHash (System.Type t)
		{
			if (hashDict == null)
				hashDict = new Dictionary<System.Type, int> ();

			int c;
			if (hashDict.TryGetValue (t, out c))
				return new StringHash (c);
			var m = t.GetMethod ("GetTypeStatic", BindingFlags.Static | BindingFlags.NonPublic);
			var hash = (StringHash) m.Invoke (null, null);
			hashDict [t] = hash.Code;
			return hash;
		}

		[DllImport ("mono-urho", EntryPoint="Urho_GetPlatform")]
		extern static string GetPlatform();

		static string platform;
		public static string Platform {
			get {
				if (platform == null)
					platform = GetPlatform ();
				return platform;
			}
		}
	}
}
