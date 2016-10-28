using System;

using Urho;
namespace Urho.Actions
{
	class AsyncCompletionCallFunc : CallFunc
	{
		#region Constructors

		public AsyncCompletionCallFunc(Action selector) : base(selector)
		{
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new AsyncCompletionCallFuncState(this, target);
		}
	}

	class AsyncCompletionCallFuncState : CallFuncState
	{
		public AsyncCompletionCallFuncState(CallFunc action, Node target)
			: base(action, target)
		{
		}

		internal protected override void Stop()
		{
			CallFunction?.Invoke();
		}
	}
}