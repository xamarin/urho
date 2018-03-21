using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Urho
{
	internal class MainLoopDispatcher
	{
		static object staticSyncObj = new object();
		static List<Action> actionsToRun;
		static HashSet<DelayState> delayTasks;

		public static ConfiguredTaskAwaitable<bool> ToMainThreadAsync()
		{
			var tcs = new TaskCompletionSource<bool>();
			InvokeOnMain(() => tcs.TrySetResult(true));
			return tcs.Task.ConfigureAwait(false);
		}

		public static void InvokeOnMain(Action action)
		{
			if (!Urho.Application.HasCurrent || !Urho.Application.Current.IsActive)
			{
				throw new InvalidOperationException("InvokeOnMain should be called when Urho.Application is active.");
			}

			lock (staticSyncObj)
			{
				if (actionsToRun == null)
					actionsToRun = new List<Action>();
				actionsToRun.Add(action);
			}
		}

		public static Task<bool> InvokeOnMainAsync(Action action)
		{
			var tcs = new TaskCompletionSource<bool>();
			InvokeOnMain(() =>
				{
					action?.Invoke();
					tcs.TrySetResult(true);
				});
			return tcs.Task;
		}

		public static ConfiguredTaskAwaitable<bool> Delay(float seconds)
		{
			var tcs = new TaskCompletionSource<bool>();

			lock (staticSyncObj)
			{
				if (delayTasks == null)
					delayTasks = new HashSet<DelayState>();
				delayTasks.Add(new DelayState {Duration = seconds, Task = tcs});
			}

			return tcs.Task.ConfigureAwait(false);
		}

		public static ConfiguredTaskAwaitable<bool> Delay(TimeSpan timeSpan) => Delay((float)timeSpan.TotalSeconds);

		public static void HandleUpdate(float timeStep)
		{
			if (actionsToRun != null)
			{
				Action[] actions;
				lock (staticSyncObj)
				{
					actions = actionsToRun.ToArray();
					actionsToRun = null;
				}

				for (int i = 0; i < actions.Length; i++)
				{
					actions[i]?.Invoke();
				}
			}

			if (delayTasks != null)
			{
				DelayState[] delayActions;
				lock (staticSyncObj)
				{
					delayActions = delayTasks.ToArray();
				}

				for (int i = 0; i < delayActions.Length; i++)
				{
					var task = delayActions[i];
					task.Duration -= timeStep;
					if (task.Duration <= 0)
					{
						task.Task.TrySetResult(true);
						lock (staticSyncObj)
							delayTasks.Remove(task);
					}
				}
			}
		}

		class DelayState
		{
			public float Duration { get; set; }

			public TaskCompletionSource<bool> Task { get; set; }
		}
	}
}
