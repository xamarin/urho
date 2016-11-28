using System;
using System.Collections.Generic;

namespace Urho
{
	internal class UrhoEventAdapter<TEventArgs>
	{
		readonly Dictionary<IntPtr, List<Action<TEventArgs>>> managedSubscribersByObjects;
		readonly Dictionary<IntPtr, Subscription> nativeSubscriptionsForObjects;

		public UrhoEventAdapter()
		{
			managedSubscribersByObjects = new Dictionary<IntPtr, List<Action<TEventArgs>>>(IntPtrEqualityComparer.Instance);
			nativeSubscriptionsForObjects = new Dictionary<IntPtr, Subscription>(IntPtrEqualityComparer.Instance);
		}

		public void AddManagedSubscriber(IntPtr handle, Action<TEventArgs> action, Func<Action<TEventArgs>, Subscription> nativeSubscriber)
		{
			List<Action<TEventArgs>> listOfManagedSubscribers;
			if (!managedSubscribersByObjects.TryGetValue(handle, out listOfManagedSubscribers))
			{
				listOfManagedSubscribers = new List<Action<TEventArgs>> { action };
				managedSubscribersByObjects[handle] = listOfManagedSubscribers;
				nativeSubscriptionsForObjects[handle] = nativeSubscriber(args => 
					{
						foreach (var managedSubscriber in listOfManagedSubscribers)
						{
							managedSubscriber(args);
						}
					});
			}
			else
			{
				//this handle is already subscribed to the native event - don't call native subscription again - just add it to the list.
				listOfManagedSubscribers.Add(action);
			}
		}

		public void RemoveManagedSubscriber(IntPtr handle, Action<TEventArgs> action)
		{
			List<Action<TEventArgs>> listOfManagedSubscribers;
			if (managedSubscribersByObjects.TryGetValue(handle, out listOfManagedSubscribers))
			{
				listOfManagedSubscribers.RemoveAll(a => a == action);

				if (listOfManagedSubscribers.Count < 1)
				{
					managedSubscribersByObjects.Remove(handle);
					nativeSubscriptionsForObjects[handle].Unsubscribe();
				}
			}
		}
	}
}
