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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Urho.IO;
using Urho.Resources;

namespace Urho
{
	internal class Runtime
	{
		static readonly RefCountedCache RefCountedCache = new RefCountedCache();
		static Dictionary<Type, int> hashDict;

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate void MonoRefCountedCallback(IntPtr ptr, RefCountedEvent rcEvent);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate void MonoComponentCallback(IntPtr componentPtr, IntPtr xmlElementPtr, IntPtr scenePtr, MonoComponentCallbackType eventType);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern void RegisterMonoRefCountedCallback(MonoRefCountedCallback callback);

		[DllImport(Consts.NativeImport, CallingConvention = CallingConvention.Cdecl)]
		static extern void RegisterMonoComponentCallback(MonoComponentCallback callback);

		static MonoRefCountedCallback monoRefCountedCallback; //keep references to native callbacks (protect from GC)
		static MonoComponentCallback monoComponentCallback;

		internal static bool IsClosing { get; private set; }

		internal static void Start()
		{
			IsClosing = false;
			RegisterMonoRefCountedCallback(monoRefCountedCallback = OnRefCountedEvent);
			RegisterMonoComponentCallback(monoComponentCallback = OnComponentEvent);
		}

		internal static void Setup()
		{
		}

		/// <summary>
		/// This method is called by RefCounted::~RefCounted or RefCounted::AddRef
		/// </summary>
		[MonoPInvokeCallback(typeof(MonoRefCountedCallback))]
		static void OnRefCountedEvent(IntPtr ptr, RefCountedEvent rcEvent)
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

		[MonoPInvokeCallback(typeof(MonoComponentCallback))]
		static void OnComponentEvent(IntPtr componentPtr, IntPtr xmlElementPtr, IntPtr scenePtr, MonoComponentCallbackType eventType)
		{
			const string typeNameKey = "SharpTypeName";
			var xmlElement = new XmlElement(xmlElementPtr);
			if (eventType == MonoComponentCallbackType.SaveXml)
			{
				var component = LookupObject<Component>(componentPtr, false);
				if (component != null && component.TypeName != component.GetType().Name)
				{
					xmlElement.SetString(typeNameKey, component.GetType().AssemblyQualifiedName);
					component.OnSerialize(new XmlComponentSerializer(xmlElement));
				}
			}
			else if (eventType == MonoComponentCallbackType.LoadXml)
			{
				var name = xmlElement.GetAttribute(typeNameKey);
				if (!string.IsNullOrEmpty(name))
				{
					Component component;
					try
					{
						component = (Component) Activator.CreateInstance(Type.GetType(name), componentPtr);
					}
					catch (Exception exc)
					{
						throw new InvalidOperationException($"{name} doesn't override constructor Component(IntPtr handle).", exc);
					}
					component.OnDeserialize(new XmlComponentSerializer(xmlElement));
					if (component.Node != null)
					{
						component.AttachedToNode(component.Node);
					}
				}
			}
			else if (eventType == MonoComponentCallbackType.AttachedToNode)
			{
				var component = LookupObject<Component>(componentPtr, false);
				component?.OnAttachedToNode(component.Node);
			}
			else if (eventType == MonoComponentCallbackType.SceneSet)
			{
				var component = LookupObject<Component>(componentPtr, false);
				component?.OnSceneSet(LookupObject<Scene>(scenePtr, false));
			}
		}

		public static T LookupRefCounted<T> (IntPtr ptr, bool createIfNotFound = true) where T:RefCounted
		{
			if (ptr == IntPtr.Zero)
				return null;

			var reference = RefCountedCache.Get(ptr)?.Reference;
			if (reference is T)
				return (T) reference;

			if (!createIfNotFound)
				return null;

			var refCounted = (T)Activator.CreateInstance(typeof(T), ptr);
			return refCounted;
		}

		public static T LookupObject<T>(IntPtr ptr, bool createIfNotFound = true) where T : UrhoObject
		{
			if (ptr == IntPtr.Zero)
				return null;

			var referenceHolder = RefCountedCache.Get(ptr);
			var reference = referenceHolder?.Reference;
			if (reference is T) //possible collisions
				return (T)reference;

			if (!createIfNotFound)
				return null;

			var name = Marshal.PtrToStringAnsi(UrhoObject.UrhoObject_GetTypeName(ptr));
			var type = FindTypeByName(name);
			var typeInfo = type.GetTypeInfo();
			if (typeInfo.IsSubclassOf(typeof(Component)) || type == typeof(Component))
			{
				//TODO: special case, handle managed subclasses
			}

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
		
		public static StringHash LookupStringHash (Type t)
		{
			if (hashDict == null)
				hashDict = new Dictionary<Type, int> ();

			int c;
			if (hashDict.TryGetValue (t, out c))
				return new StringHash (c);
			var hash = GetTypeStatic(t);
			hashDict [t] = hash.Code;
			return hash;
		}

		static StringHash GetTypeStatic(Type type)
		{
			var typeStatic = type.GetRuntimeProperty("TypeStatic");
			while (typeStatic == null)
			{
				type = type.GetTypeInfo().BaseType;
				if (type == typeof(object))
					throw new InvalidOperationException("The type doesn't have static TypeStatic property");
				typeStatic = type.GetRuntimeProperty("TypeStatic");
			}
			return (StringHash)typeStatic.GetValue(null);
		}

		// for RefCounted, UrhoObjects
		internal static void ValidateRefCounted<T>(T obj, [CallerMemberName] string name = "") where T : RefCounted
		{
			if (IsClosing)
			{
				var errorText = $"{typeof(T).Name}.{name} (Handle={obj.Handle}) was invoked after Application.Stop";
				LogSharp.Error(errorText);
				throw new InvalidOperationException(errorText);
			}
			if (obj.IsDeleted) //IsDeleted is set to True when we receive a native callback from RefCounted::~RefCounted
			{
				var errorText = $"Underlying native object was deleted for Handle={obj.Handle}. {typeof(T).Name}.{name}";
				LogSharp.Error(errorText);
				throw new InvalidOperationException(errorText);
			}
			//if (obj.Handle == IntPtr.Zero)
			//{
			//}
			//TODO: check current thread?
		}

		// non-RefCounted classes
		internal static void ValidateObject<T>(T obj, [CallerMemberName] string name = "") where T : class
		{
			if (IsClosing)
			{
				var errorText = $"{typeof(T).Name}.{name} was invoked after Application.Stop";
				LogSharp.Error(errorText);
				throw new InvalidOperationException(errorText);
			}
		}

		// constructors, static methods, value types
		internal static void Validate(Type type, [CallerMemberName] string name = "")
		{
			if (IsClosing)
			{
				var errorText = $"{type.Name}.{name} was invoked after Application.Stop";
				LogSharp.Error(errorText);
				throw new InvalidOperationException(errorText);
			}
		}

		internal static IReadOnlyList<T> CreateVectorSharedPtrProxy<T> (IntPtr handle) where T : UrhoObject
		{
			return new Vectors.ProxyUrhoObject<T> (handle);
		}

		internal static IReadOnlyList<T> CreateVectorSharedPtrRefcountedProxy<T>(IntPtr handle) where T : RefCounted
		{
			return new Vectors.ProxyRefCounted<T>(handle);
		}

		internal static void Cleanup()
		{
			IsClosing = true;
			RefCountedCache.Clean();
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
		}

		// for Debug purposes
		static internal int KnownObjectsCount => RefCountedCache.Count;

		static Dictionary<string, Type> typesByNativeNames;
		// special cases: (TODO: share this code with SharpieBinder somehow)
		static Dictionary<string, string> typeNamesMap = new Dictionary<string, string>
			{
				{nameof(UrhoObject),  "Object"},
				{nameof(UrhoConsole), "Console"},
				{nameof(XmlFile),     "XMLFile"},
				{nameof(JsonFile),    "JSONFile"},
			};

		static Type FindTypeByName(string name)
		{
			if (typesByNativeNames == null)
			{
				typesByNativeNames = new Dictionary<string, Type>(200);
				foreach (var type in typeof(Runtime).GetTypeInfo().Assembly.ExportedTypes)
				{
					if (!type.GetTypeInfo().IsSubclassOf(typeof(RefCounted)))
						continue;

					string remappedName;
					if (!typeNamesMap.TryGetValue(type.Name, out remappedName))
						remappedName = type.Name;

					typesByNativeNames[remappedName] = type;
				}
			}
			Type result;
			if (!typesByNativeNames.TryGetValue(name, out result))
				throw new Exception($"Type {name} not found.");

			return result;
		}
	}
}
