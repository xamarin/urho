using System;
using System.Threading;

namespace Urho
{
	/// <summary>
	/// A pair of ExecutionContext and its Id
	/// </summary>
	public struct ExecutionContextWithId
	{
		public int ContextId { get; }
		public ExecutionContext Context { get; }

		public ExecutionContextWithId(int contextId, ExecutionContext context)
		{
			this.ContextId = contextId;
			this.Context = context;
		}

		public static ExecutionContextWithId FromCurrent()
		{
			return new ExecutionContextWithId(
				Thread.CurrentContext.ContextID, ExecutionContext.Capture());
		}

		public void Dispatch(System.Action action)
		{
			if (Thread.CurrentContext.ContextID != ContextId)
			{
				ExecutionContext.Run(Context, _ => action(), null);
			}
			else
			{
				//do not dispatch if we are in the same context
				action();
			}
		}

		public override int GetHashCode()
		{
			return ContextId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return obj is ExecutionContextWithId && ((ExecutionContextWithId)obj).ContextId == ContextId;
		}
	}
}
