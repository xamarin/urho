//
// Runtime C# support
//
// Authors:
//   Miguel de Icaza (miguel@xamarin.com)
//
// Copyrigh 2015 Xamarin INc
//
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Urho {
	
	public partial class Runtime {
		static Dictionary<IntPtr, ReferenceHolder<RefCounted>> knownObjects = new Dictionary<IntPtr, ReferenceHolder<RefCounted>> ();
		static Dictionary<System.Type, int> hashDict;
		static RefCountedEventCallback refCountedEventCallback;

		public enum RefCountedEvent { Delete, AddRef }

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void RefCountedEventCallback(IntPtr ptr, RefCountedEvent rcEvent);

		[DllImport("mono-urho", CallingConvention = CallingConvention.Cdecl)]
		extern static void SetRefCountedEventCallback(RefCountedEventCallback callback);

		/// <summary>
		/// Runtime initialization. 
		/// </summary>
		public static void Initialize()
		{
			refCountedEventCallback = refCountedEventCallback ?? OnRefCountedEvent;
			SetRefCountedEventCallback(refCountedEventCallback);
		}

		/// <summary>
		/// This method is a workaround for iOS that requires all callback methods to be marked with a special attribute [MonoPInvokeCallback]
		/// </summary>
		public static void SetCustomRefcountedEventCallback(RefCountedEventCallback callback)
		{
			refCountedEventCallback = callback;
		}

		/// <summary>
		/// This method is called by RefCounted::~RefCounted
		/// </summary>
		public static void OnRefCountedEvent(IntPtr ptr, RefCountedEvent rcEvent)
		{
			if (rcEvent == RefCountedEvent.Delete)
			{
				ReferenceHolder<RefCounted> value;
				if (knownObjects.TryGetValue(ptr, out value) && value != null)
				{
					var reference = value.Reference;
					if (reference == null)
						knownObjects.Remove(ptr);
					else
						reference.HandleNativeDelete();
				}
			}
			else if (rcEvent == RefCountedEvent.AddRef)
			{
				ReferenceHolder<RefCounted> refHolder;
				if (knownObjects.TryGetValue(ptr, out refHolder))
				{
					refHolder.MakeStrong(); 
				}
			}
		}

		public static T LookupRefCounted<T> (IntPtr ptr) where T:RefCounted
		{
			if (ptr == IntPtr.Zero)
				return null;

			// No locks are needed, because Urho code is only allowed to run in the 
			// UI thread, never anywhere else.
			ReferenceHolder<RefCounted> ret;
			if (knownObjects.TryGetValue (ptr, out ret)){
				var refernce = (T)ret.Reference;
				if (refernce != null)
					return refernce;
			}

			var o = (T)Activator.CreateInstance(typeof(T), ptr);
			knownObjects[ptr] = new ReferenceHolder<RefCounted>(o, weak: o.Refs() < 1);
			return o;
		}

		public static T LookupObject<T>(IntPtr ptr) where T : UrhoObject
		{
			if (ptr == IntPtr.Zero)
				return null;

			// No locks are needed, because Urho code is only allowed to run in the 
			// UI thread, never anywhere else.
			ReferenceHolder<RefCounted> ret;
			if (knownObjects.TryGetValue(ptr, out ret)){
				var refernce = (T) ret.Reference;
				if (refernce != null)
					return refernce;
			}

			var name = Marshal.PtrToStringAnsi(UrhoObject.UrhoObject_GetTypeName(ptr));

			var type = System.Type.GetType("Urho." + name) ?? System.Type.GetType("Urho.Urho" + name);
			var o = (T)Activator.CreateInstance(type, ptr);
			knownObjects[ptr] = new ReferenceHolder<RefCounted>(o, weak: o.Refs() < 1); //TODO: join GetTypeName and Refs into single pinvoke
			return o;
		}

		public static void UnregisterObject (IntPtr handle)
		{
			knownObjects.Remove(handle);
		}

		public static void RegisterObject (RefCounted r)
		{
			var rh = r.Handle;
			knownObjects [rh] = new ReferenceHolder<RefCounted>(r, weak: r.Refs() < 1); //TODO: seems Refs will be always 0 here
		}
		
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

	internal class ReferenceHolder<T> where T : class
	{
		public T StrongRef { get; private set; }
		public WeakReference<T> WeakRef { get; private set; }

		public ReferenceHolder(T obj, bool weak)
		{
			if (weak)
				WeakRef = new WeakReference<T>(obj);
			else
				StrongRef = obj;
		}

		public T Reference
		{
			get
			{
				if (StrongRef != null)
					return StrongRef;

				T wr;
				WeakRef.TryGetTarget(out wr);
				return wr;
			}
		}

		/// <summary>
		/// Change Weak to Strong 
		/// </summary>
		public bool MakeStrong()
		{
			if (StrongRef != null)
				return true;
			T strong = null;
			WeakRef?.TryGetTarget(out strong);

			StrongRef = strong;
			WeakRef = null;
			return StrongRef != null;
		}

		/// <summary>
		/// Change Strong to Weak
		/// </summary>
		public bool MakeWeak()
		{
			if (StrongRef != null)
			{
				WeakRef = new WeakReference<T>(StrongRef);
				StrongRef = null;
				return true;
			}
			return false;
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
