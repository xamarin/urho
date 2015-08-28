using System;

namespace Urho
{
    public class CCCallFuncND : CCCallFuncN
    {
        public Action<Node, object> CallFunctionND { get; private set; }
        public object Data { get; private set; }


        #region Constructors

        public CCCallFuncND(Action<Node, object> selector, object d) : base()
        {
            Data = d;
            CallFunctionND = selector;
        }

        #endregion Constructors


        protected internal override CCActionState StartAction(Node target)
        {
            return new CCCallFuncNDState (this, target);

        }
    }

    public class CCCallFuncNDState : CCCallFuncState
    {
        protected Action<Node, object> CallFunctionND { get; set; }
        protected object Data { get; set; }

        public CCCallFuncNDState (CCCallFuncND action, Node target)
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