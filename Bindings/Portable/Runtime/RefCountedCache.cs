using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Urho
{
	/// <summary>
	/// </summary>
	internal class RefCountedCache
	{
		Dictionary<IntPtr, ReferenceHolder<RefCounted>> knownObjects = new Dictionary<IntPtr, ReferenceHolder<RefCounted>>(256); //based on samples (average)

		public int Count => knownObjects.Count;

		public void Add(RefCounted refCounted)
		{
			lock (knownObjects)
			{
				ReferenceHolder<RefCounted> knownObject;
				if (knownObjects.TryGetValue(refCounted.Handle, out knownObject))
				{
					knownObject?.Reference?.Dispose();
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
			//TODO:
			return defaulPriority;
		}

		bool StrongRefByDefault(RefCounted refCounted)
		{
			if (refCounted is Scene)
				return true;
			return false;
		}
	}
}
