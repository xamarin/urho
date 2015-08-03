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

		[DllImport ("mono-urho", EntryPoint="Urho_GetPlatform", CallingConvention=CallingConvention.Cdecl)]
		extern static string GetPlatform();

		static string platform;
		public static string Platform {
			get {
				if (platform == null)
					platform = GetPlatform ();
				return platform;
			}
		}

		static internal IList<T> CreateVectorSharedPtrProxy<T> (IntPtr handle) where T : RefCounted
		{
			return new Vectors.Proxy<T> (handle);
		}
	}

	internal static class Vectors {
		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		internal extern static int VectorSharedPtr_Count (IntPtr h);

		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		internal extern static IntPtr VectorSharedPtr_GetIdx (IntPtr h, int idx);

		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		internal extern static void VectorSharedPtr_SetIdx (IntPtr h, int idx, IntPtr v);
		
	
		internal class Proxy<T> : IList<T> where T : RefCounted
		{
			IntPtr handle;
			public Proxy (IntPtr handle)
			{
				this.handle = handle;
			}
			
		
			public T this [int idx] {
				get {
					return Runtime.LookupObject<T> (VectorSharedPtr_GetIdx (handle, idx));
				}
				set {
					// Mhm, how do we get the SharedPtr from an existing object?
					throw new NotImplementedException ("Proxy has not implemented this yet");
				}
			}
				
			public int IndexOf (T v)
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");
			}
			
			public void Insert (int idx, T v)
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");
			}
			
			public void RemoveAt (int idx)
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");
			}
			
			public bool Remove (T val)
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");
			}
			
			public void CopyTo (T [] target, int p)
			{
				int c = Count;
				for (int i = 0; i < c; i++)
					target [i+p] = this[i];
			}
			
			public int Count {
				get {
					return VectorSharedPtr_Count (handle);
				}
			}
			
			public bool IsReadOnly => false;
			
			public void Add (T v)
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");			
			}
			
			public void Clear ()
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");			
			}
			
			public bool Contains (T v)
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");			
			}
			
			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");			
			}
			
			public IEnumerator<T> GetEnumerator ()
			{
				throw new NotImplementedException ("Proxy has not implemented this yet");			
			}
		}
	}

	public class Subscription {
		internal GCHandle gch;
		internal IntPtr   UnmanagedProxy;

		internal Subscription (Action<IntPtr> proxy)
		{
			gch = GCHandle.Alloc (proxy);
		}

		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		extern static void urho_unsubscribe (IntPtr notificationProxy);
		
		public void Unsubscribe ()
		{
			if (UnmanagedProxy == IntPtr.Zero)
				return;
			
			urho_unsubscribe (UnmanagedProxy);
			UnmanagedProxy = IntPtr.Zero;
			gch.Free ();
		}
	}
}
