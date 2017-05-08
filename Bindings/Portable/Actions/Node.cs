using System;
using System.Threading.Tasks;
using Urho.Actions;

namespace Urho
{
	partial class Node
	{
		/// <summary>
		/// Runs an Action that can be awaited.
		/// </summary>
		/// <param name="action">A FiniteTimeAction.</param>
		public Task<ActionState> RunActionsAsync(FiniteTimeAction action)
		{
			if (!EnsureAppIsActive())
				return Task.FromResult<ActionState>(null);

			var tcs = new TaskCompletionSource<ActionState>();
			var asyncAction = new Sequence(action, new TaskSource(tcs)) { CancelAction = s => tcs.TrySetCanceled() };
			Application.Current.ActionManager.AddAction(asyncAction, this);
			return tcs.Task;
		}

		/// <summary>
		/// Runs a sequence of Actions so that it can be awaited.
		/// </summary>
		/// <param name="actions">An array of FiniteTimeAction objects.</param>
		public Task<ActionState> RunActionsAsync(params FiniteTimeAction[] actions)
		{
			if (actions.Length == 0)
				return Task.FromResult<ActionState>(null);

			if (!EnsureAppIsActive())
				return Task.FromResult<ActionState>(null);

			var tcs = new TaskCompletionSource<ActionState>();
			FiniteTimeAction completion = new TaskSource(tcs);
			var asyncAction = actions.Length > 0 ? new Sequence(actions, completion) { CancelAction = s => tcs.TrySetCanceled() } : completion;

			Application.Current.ActionManager.AddAction(asyncAction, this);
			return tcs.Task;
		}

		public void RunActions(params FiniteTimeAction[] actions)
		{
			var action = actions.Length > 1 ? new Sequence(actions) : actions[0];
			Application.Current.ActionManager.AddAction(action, this);
		}

		public void RemoveAction(ActionState state)
		{
			Application.Current.ActionManager.RemoveAction(state);
		}

		public void RemoveAction(BaseAction action)
		{
			Application.Current.ActionManager.RemoveAction(action, this);
		}

		public void RemoveAllActions()
		{
			Application.Current.ActionManager.RemoveAllActionsFromTarget(this);
		}

		public void PauseAllActions()
		{
			Application.Current.ActionManager.PauseTarget(this);
		}

		public void ResumeAllActions()
		{
			Application.Current.ActionManager.ResumeTarget(this);
		}

		bool EnsureAppIsActive()
		{
			if (IsDeleted || !Application.HasCurrent || !Application.Current.IsActive)
			{
				if (Application.CancelActiveActionsOnStop)
					throw new OperationCanceledException("Urho.Application is not created or was closed.");
				return false;
			}
			return true;
		} 
	}
}
