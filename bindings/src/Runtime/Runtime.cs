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

namespace Urho
{
	public partial class Runtime
	{
		static readonly RefCountedCache RefCountedCache = new RefCountedCache();
		static Dictionary<System.Type, int> hashDict;
		static RefCountedEventCallback refCountedEventCallback;

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
		/// This method is called by RefCounted::~RefCounted or RefCounted::AddRef
		/// </summary>
		public static void OnRefCountedEvent(IntPtr ptr, RefCountedEvent rcEvent)
		{
			if (rcEvent == RefCountedEvent.Delete)
			{
				var referenceHolder = RefCountedCache.Get(ptr);
				if (referenceHolder == null)
					return; //we don't have this object in the cache so let's just skip it

				var reference = referenceHolder.Reference;
				if (reference == null)
				{
					// seems like the reference was Weak and GC has removed it - remove item from the dictionary
					RefCountedCache.Remove(ptr);
				}
				else
				{
					reference.HandleNativeDelete();
				}
			}
			else if (rcEvent == RefCountedEvent.Addref)
			{
				//if we have an object with this handle and it's reference is weak - then change it to strong.
				var referenceHolder = RefCountedCache.Get(ptr);
				referenceHolder?.MakeStrong();
			}
		}

		public static T LookupRefCounted<T> (IntPtr ptr) where T:RefCounted
		{
			if (ptr == IntPtr.Zero)
				return null;

			var reference = RefCountedCache.Get(ptr)?.Reference;
			if (reference is T)
				return (T) reference;

			var refCounted = (T)Activator.CreateInstance(typeof(T), ptr);
			return refCounted;
		}

		public static T LookupObject<T>(IntPtr ptr) where T : UrhoObject
		{
			if (ptr == IntPtr.Zero)
				return null;

			var referenceHolder = RefCountedCache.Get(ptr);
			var reference = referenceHolder?.Reference;
			if (reference is T)
				return (T)reference;

			var name = Marshal.PtrToStringAnsi(UrhoObject.UrhoObject_GetTypeName(ptr));
			var type = System.Type.GetType("Urho." + name) ?? System.Type.GetType("Urho.Urho" + name);
			var urhoObject = (T)Activator.CreateInstance(type, ptr);
			return urhoObject;
		}

		public static void UnregisterObject (IntPtr handle)
		{
			RefCountedCache.Remove(handle);
		}

		public static void RegisterObject (RefCounted refCounted)
		{
			RefCountedCache.Add(refCounted);
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

		// for Debug purposes
		static internal int KnownObjectsCount => RefCountedCache.Count;
	}
}
