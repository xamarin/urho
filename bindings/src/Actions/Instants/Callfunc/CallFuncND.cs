using System;

using Urho;
namespace Urho.Actions
{
	public class CallFuncND : CallFuncN
	{
		public Action<Node, object> CallFunctionND { get; }
		public object Data { get; }

		#region Constructors

		public CallFuncND(Action<Node, object> selector, object d) : base()
		{
			Data = d;
			CallFunctionND = selector;
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new CallFuncNDState (this, target);
		}
	}

	public class CallFuncNDState : CallFuncState
	{
		protected Action<Node, object> CallFunctionND { get; set; }
		protected object Data { get; set; }

		public CallFuncNDState (CallFuncND action, Node target)
			: base(action, target)
		{   
			CallFunctionND = action.CallFunctionND;
			Data = action.Data;
		}

		public override void Execute()
		{
			if (null != CallFunctionND)
			{
				CallFunctionND(Target, Data);
			}
		}
	}
}