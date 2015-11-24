using System;

using Urho;
namespace Urho.Actions
{
	public class CallFuncN : CallFunc
	{
		public Action<Node> CallFunctionN { get; }

		#region Constructors

		public CallFuncN()
		{
		}

		public CallFuncN(Action<Node> selector) : base()
		{
			CallFunctionN = selector;
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new CallFuncNState (this, target);

		}
	}

	public class CallFuncNState : CallFuncState
	{
		protected Action<Node> CallFunctionN { get; set; }

		public CallFuncNState (CallFuncN action, Node target)
			: base(action, target)
		{   
			CallFunctionN = action.CallFunctionN;
		}

		public override void Execute()
		{
			CallFunctionN?.Invoke(Target);
		}
	}
}