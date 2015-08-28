using System;

namespace Urho
{
    public class CCCallFuncN : CCCallFunc
    {
        public Action<Node> CallFunctionN { get; private set; }

        #region Constructors

        public CCCallFuncN() : base()
        {
        }

        public CCCallFuncN(Action<Node> selector) : base()
        {
            CallFunctionN = selector;
        }

        #endregion Constructors


        protected internal override CCActionState StartAction(Node target)
        {
            return new CCCallFuncNState (this, target);

        }

    }

    public class CCCallFuncNState : CCCallFuncState
    {

        protected Action<Node> CallFunctionN { get; set; }

        public CCCallFuncNState (CCCallFuncN action, Node target)
            : base(action, target)
        {   
            CallFunctionN = action.CallFunctionN;
        }

        public override void Execute()
        {
            if (null != CallFunctionN)
            {
                CallFunctionN(Target);
            }
            //if (m_nScriptHandler) {
            //    CCScriptEngineManager::sharedManager()->getScriptEngine()->executeFunctionWithobject(m_nScriptHandler, m_pTarget, "Node");
            //}
        }

    }
}