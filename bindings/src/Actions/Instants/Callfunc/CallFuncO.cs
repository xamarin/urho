using System;

using Urho;
namespace Urho.Actions
{
	public class CallFuncO : CallFunc
	{
		public Action<object> CallFunctionO { get; }
		public object Object { get; }

		#region Constructors

		public CallFuncO()
		{
			this.Object = null;
			this.CallFunctionO = null;
		}

		public CallFuncO(Action<object> selector, object pObject) : this()
		{
			this.Object = pObject;
			this.CallFunctionO = selector;
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new CallFuncOState (this, target);
		}
	}

	public class CallFuncOState : CallFuncState
	{
		protected Action<object> CallFunctionO { get; set; }
		protected object Object { get; set; }

		public CallFuncOState (CallFuncO action, Node target)
			: base(action, target)
		{   
			CallFunctionO = action.CallFunctionO;
			Object = action.Object;
		}

		public override void Execute()
		{
			CallFunctionO?.Invoke(Object);
		}
	}
}