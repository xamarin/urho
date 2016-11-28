using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Urho
{
	/// <summary>
	/// </summary>
	internal class RefCountedCache
	{
		Dictionary<IntPtr, ReferenceHolder<RefCounted>> knownObjects = 
			new Dictionary<IntPtr, ReferenceHolder<RefCounted>>(IntPtrEqualityComparer.Instance);

		public int Count => knownObjects.Count;

		public string GetCacheStatus()
		{
			lock (knownObjects)
			{
				var topMcw = knownObjects
					.Select(t => t.Value.Reference?.GetType())
					.Where(t => t != null)
					.GroupBy(k => k.Name)
					.OrderByDescending(t => t.Count())
					.Take(10)
					.Select(t => $"{t.Key}:  {t.Count()}");
				return $"Size: {Count}\nTypes: {string.Join("\n", topMcw)}";
			}
		}

		public void Add(RefCounted refCounted)
		{
			lock (knownObjects)
			{
				ReferenceHolder<RefCounted> knownObject;
				if (knownObjects.TryGetValue(refCounted.Handle, out knownObject))
				{
					var existingObj = knownObject?.Reference;
					if (existingObj != null && !IsInHierarchy(existingObj.GetType(), refCounted.GetType()))
						throw new InvalidOperationException($"Handle '{refCounted.Handle}' is in use by '{existingObj.GetType().Name}' (IsDeleted={existingObj.IsDeleted}). {refCounted.GetType()}");
				}

				knownObjects[refCounted.Handle] = new ReferenceHolder<RefCounted>(refCounted, weak: refCounted.Refs() < 1 && !StrongRefByDefault(refCounted));
			}
		}

		public bool Remove(IntPtr ptr)
		{
			lock (knownObjects)
			{
				return knownObjects.Remove(ptr);
			}
		}

		public ReferenceHolder<RefCounted> Get(IntPtr ptr)
		{
			lock (knownObjects)
			{
				ReferenceHolder<RefCounted> refCounted;
				knownObjects.TryGetValue(ptr, out refCounted);
				return refCounted;
			}
		}

		public void Clean()
		{
			IntPtr[] handles;

			lock (knownObjects)
				handles = knownObjects.OrderBy(t => GetDisposePriority(t.Value)).Select(t => t.Key).ToArray();

			foreach (var handle in handles)
			{
				ReferenceHolder<RefCounted> refHolder;
				lock (knownObjects)
					knownObjects.TryGetValue(handle, out refHolder);
				refHolder?.Reference?.Dispose();
			}
			LogSharp.Warn($"RefCountedCache objects alive: {knownObjects.Count}");

			//knownObjects.Clear();
		}

		int GetDisposePriority(ReferenceHolder<RefCounted> refHolder)
		{
			const int defaulPriority = 1000;
			var obj = refHolder?.Reference;
			if (obj == null)
				return defaulPriority;
			if (obj is Scene)
				return 1;
			if (obj is Context)
				return int.MaxValue;
			//TODO:
			return defaulPriority;
		}

		bool StrongRefByDefault(RefCounted refCounted)
		{
			if (refCounted is Scene) return true;
			if (refCounted is Context) return true;
			return false;
		}

		bool IsInHierarchy(Type t1, Type t2)
		{
			if (t1 == t2) return true;
			if (t1.GetTypeInfo().IsSubclassOf(t2)) return true;
			if (t2.GetTypeInfo().IsSubclassOf(t1)) return true;
			return false;
		}
	}
}
