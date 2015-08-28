using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Urho
{
	partial class Node
	{
		/// <summary>
		/// Runs a sequence of Actions so that it can be awaited.
		/// </summary>
		/// <param name="actions">An array of CCFiniteTimeAction objects.</param>
		public Task<CCActionState> RunActionsAsync(params CCFiniteTimeAction[] actions)
		{
			Debug.Assert(actions != null, "Argument must be non-nil");
			Debug.Assert(actions.Length > 0, "Paremeter: actions has length of zero. At least one action must be set to run.");

			var tcs = new TaskCompletionSource<CCActionState>();

			var numActions = actions.Length;
			var asyncActions = new CCFiniteTimeAction[actions.Length + 1];
			Array.Copy(actions, asyncActions, numActions);

			CCActionState state = null;
			asyncActions[numActions] = new CCCallFunc(() => tcs.TrySetResult(state));

			var asyncAction = asyncActions.Length > 1 ? new CCSequence(asyncActions) : asyncActions[0];

			state = ActionManager.AddAction(asyncAction, this, !IsRunning);
			
			return tcs.Task;
		}

		public CCActionManager ActionManager { get; set; } = new CCActionManager();

		public bool IsRunning => true; // TODO: handle platform's Pause, Resume etc..
	}
}
