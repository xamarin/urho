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
			var handles = knownObjects.Keys.ToArray();
			foreach (var handle in handles)
			{
				ReferenceHolder<RefCounted> refHolder;
				lock (knownObjects)
					knownObjects.TryGetValue(handle, out refHolder);
				refHolder?.Reference?.Dispose();
			}
			//knownObjects.Clear();
		}

		bool StrongRefByDefault(RefCounted refCounted)
		{
			if (refCounted is Scene)
				return true;
			return false;
		}
	}
}
