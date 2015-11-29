using System;
using System.Collections.Generic;

namespace Urho
{
	internal class UrhoEventAdapter<TEventArgs>
	{
		readonly Dictionary<IntPtr, List<WeakReference<Action<TEventArgs>>>> managedSubscribersByObjects;
		readonly Dictionary<IntPtr, Subscription> nativeSubscriptionsForObjects;

		public UrhoEventAdapter()
		{
			managedSubscribersByObjects = new Dictionary<IntPtr, List<WeakReference<Action<TEventArgs>>>>();
			nativeSubscriptionsForObjects = new Dictionary<IntPtr, Subscription>();
		}

		public void AddManagedSubscriber(IntPtr handle, Action<TEventArgs> action, Func<Action<TEventArgs>, Subscription> nativeSubscriber)
		{
			List<WeakReference<Action<TEventArgs>>> listOfManagedSubscribers;
			if (!managedSubscribersByObjects.TryGetValue(handle, out listOfManagedSubscribers))
			{
				listOfManagedSubscribers = new List<WeakReference<Action<TEventArgs>>> { new WeakReference<Action<TEventArgs>>(action) };
				managedSubscribersByObjects[handle] = listOfManagedSubscribers;
				nativeSubscriptionsForObjects[handle] = nativeSubscriber(args => 
					{
						foreach (var managedSubscriber in listOfManagedSubscribers)
						{
							Action<TEventArgs> actionRef;
							if (managedSubscriber.TryGetTarget(out actionRef))
							{
								actionRef(args);
							}
						}
					});
			}
			else
			{
				//this handle is already subscribed to the native event - don't call native subscription again - just add it to the list.
				listOfManagedSubscribers.Add(new WeakReference<Action<TEventArgs>>(action));
			}
		}

		public void RemoveManagedSubscriber(IntPtr handle, Action<TEventArgs> action)
		{
			List<WeakReference<Action<TEventArgs>>> listOfManagedSubscribers;
			if (managedSubscribersByObjects.TryGetValue(handle, out listOfManagedSubscribers))
			{
				listOfManagedSubscribers.RemoveAll(weakRef =>
					{
						Action<TEventArgs> item;
						return weakRef.TryGetTarget(out item) && item == action;
					});

				if (listOfManagedSubscribers.Count < 1)
				{
					managedSubscribersByObjects.Remove(handle);
					nativeSubscriptionsForObjects[handle].Unsubscribe();
				}
			}
		}
	}
}
