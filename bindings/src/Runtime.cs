//
// Runtime C# support
//
// Authors:
//   Miguel de Icaza (miguel@xamarin.com)
//
// Copyrigh 2015 Xamarin INc
//
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Urho {
	
	public partial class Runtime {
		static Dictionary<IntPtr, RefCounted> knownObjects = new Dictionary<IntPtr, RefCounted> ();
		static IntPtr expecting;
		static RefCountedDestructorCallback destructorCallback;

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void RefCountedDestructorCallback(IntPtr ptr);

		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		extern static void SetRefCountedDeleteCallback(RefCountedDestructorCallback callback);

		public static void Initialize()
		{
			//keep reference to prevent collection by GC
			destructorCallback = OnRefCountedNativeDelete;
            SetRefCountedDeleteCallback(destructorCallback);
        }

		/// <summary>
		/// This method is called by RefCounted::~RefCounted
		/// </summary>
		private static void OnRefCountedNativeDelete(IntPtr ptr)
		{
			RefCounted value;
			if (knownObjects.TryGetValue(ptr, out value) && value != null)
			{
				value.OnDeleted();
				knownObjects.Remove(ptr); //Dictionary::Remove doesn't return value so we have to do TryGetValue + Remove
			}
		}

		public static T LookupRefCounted<T> (IntPtr ptr) where T:RefCounted
		{
			if (ptr == IntPtr.Zero)
				return null;

			// No locks are needed, because Urho code is only allowed to run in the 
			// UI thread, never anywhere else.
			RefCounted ret;
			if (knownObjects.TryGetValue (ptr, out ret)){
				return (T) ret;
			}

			expecting = ptr;
			var o = (T)Activator.CreateInstance(typeof(T), ptr);
			if (expecting != IntPtr.Zero)
				knownObjects[ptr] = o;
			expecting = IntPtr.Zero;
			return o;
		}

		public static T LookupObject<T>(IntPtr ptr) where T : UrhoObject
		{
			if (ptr == IntPtr.Zero)
				return null;

			// No locks are needed, because Urho code is only allowed to run in the 
			// UI thread, never anywhere else.
			RefCounted ret;
			if (knownObjects.TryGetValue(ptr, out ret))
			{
				return (T)ret;
			}

			var name = Marshal.PtrToStringAnsi(UrhoObject.UrhoObject_GetTypeName(ptr));
			expecting = ptr;
			var type = System.Type.GetType("Urho." + name) ?? System.Type.GetType("Urho.Urho" + name);
			var o = (T)Activator.CreateInstance(type, ptr);
			if (expecting != IntPtr.Zero)
				knownObjects[ptr] = o;
			expecting = IntPtr.Zero;
			return o;
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
			
			knownObjects [rh] = r;
		}

		static Dictionary<System.Type,int> hashDict;
		
		public static StringHash LookupStringHash (System.Type t)
		{
			if (hashDict == null)
				hashDict = new Dictionary<System.Type, int> ();

			int c;
			if (hashDict.TryGetValue (t, out c))
				return new StringHash (c);
			var typeStatic = t.GetRuntimeProperty("TypeStatic");
			if (typeStatic == null)
				throw new InvalidOperationException("The type doesn't have static TypeStatic property");

			var hash = (StringHash)typeStatic.GetValue(null);
			hashDict [t] = hash.Code;
			return hash;
		}

		[DllImport ("mono-urho", EntryPoint="Urho_GetPlatform", CallingConvention=CallingConvention.Cdecl)]
		extern static IntPtr GetPlatform();

		static string platform;
		public static string Platform {
			get {
				if (platform == null)
					platform = Marshal.PtrToStringAnsi(GetPlatform ());
				return platform;
			}
		}

		static internal IList<T> CreateVectorSharedPtrProxy<T> (IntPtr handle) where T : UrhoObject
		{
			return new Vectors.ProxyUrhoObject<T> (handle);
		}

		static internal IList<T> CreateVectorSharedPtrRefcountedProxy<T>(IntPtr handle) where T : RefCounted
		{
			return new Vectors.ProxyRefCounted<T>(handle);
		}
	}

	internal static class Vectors {
		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		internal extern static int VectorSharedPtr_Count (IntPtr h);

		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		internal extern static IntPtr VectorSharedPtr_GetIdx (IntPtr h, int idx);

		[DllImport ("mono-urho", CallingConvention=CallingConvention.Cdecl)]
		internal extern static void VectorSharedPtr_SetIdx (IntPtr h, int idx, IntPtr v);

		internal class ProxyUrhoObject<T> : ProxyRefCounted<T> where T : UrhoObject
		{
			public ProxyUrhoObject(IntPtr handle) : base(handle) { }

			public override T this[int idx]
			{
				get {
					return Runtime.LookupRefCounted<T>(VectorSharedPtr_GetIdx(handle, idx));
				}
				set { throw new NotImplementedException("Proxy has not implemented this yet"); }
			}
		}

		internal class ProxyRefCounted<T> : IList<T> where T : RefCounted
		{
			protected IntPtr handle;
			public ProxyRefCounted(IntPtr handle)
			{
				this.handle = handle;
			}
		
			public virtual T this [int idx] {
				get {
					return Runtime.LookupRefCounted<T> (VectorSharedPtr_GetIdx (handle, idx));
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
