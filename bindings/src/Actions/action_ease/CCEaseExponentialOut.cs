using System;

namespace Urho
{
    public class CCEaseExponentialOut : CCActionEase
    {
        #region Constructors

        public CCEaseExponentialOut (CCFiniteTimeAction action) : base (action)
        {
        }

        #endregion Constructors


        protected internal override CCActionState StartAction(Node target)
        {
            return new CCEaseExponentialOutState (this, target);
        }

        public override CCFiniteTimeAction Reverse ()
        {
            return new CCEaseExponentialIn ((CCFiniteTimeAction)InnerAction.Reverse ());
        }
    }


    #region Action state

    public class CCEaseExponentialOutState : CCActionEaseState
    {
        public CCEaseExponentialOutState (CCEaseExponentialOut action, Node target) : base (action, target)
        {
        }

        public override void Update (float time)
        {
            InnerActionState.Update (CCEaseMath.ExponentialOut (time));
        }
    }

    #endregion Action state
}