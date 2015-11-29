using System;
using System.Collections.Generic;
using System.Threading;

namespace Urho
{
	public class ListBasedUpdateSynchronizationContext : SynchronizationContext
	{
		readonly IList<Action> list;

		public ListBasedUpdateSynchronizationContext(IList<Action> list)
		{
			this.list = list;
		}

		public override SynchronizationContext CreateCopy()
		{
			return new ListBasedUpdateSynchronizationContext(list);
		}

		public override void Post(SendOrPostCallback d, object state)
		{
			lock (list)
			{
				list.Add(() => d(state));
			}
		}

		public override void Send(SendOrPostCallback d, object state)
		{
			//seems there is no difference between Post and Send for our case.
			lock (list)
			{
				list.Add(() => d(state));
			}
		}

		public void PumpActions()
		{
			List<Action> copy;
			lock (list)
			{
				copy = new List<Action>(list);
				list.Clear();
			}

			foreach (var action in copy)
				action();
		}
	}
}