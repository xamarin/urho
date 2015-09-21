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
		/// <param name="actions">An array of FiniteTimeAction objects.</param>
		public Task<ActionState> RunActionsAsync(params FiniteTimeAction[] actions)
		{
			var tcs = new TaskCompletionSource<ActionState>();

			var numActions = actions.Length;
			var asyncActions = new FiniteTimeAction[actions.Length + 1];
			Array.Copy(actions, asyncActions, numActions);

			ActionState state = null;
			asyncActions[numActions] = new CallFunc(() => tcs.TrySetResult(state));

			var asyncAction = asyncActions.Length > 1 ? new Sequence(asyncActions) : asyncActions[0];

			state = Application.Current.ActionManager.AddAction(asyncAction, this, !IsRunning);
			
			return tcs.Task;
		}

		public bool IsRunning => true;
// TODO: handle platform's Pause, Resume etc..
	}
}
