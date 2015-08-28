using System;

namespace Urho
{
    public class CCEaseExponentialInOut : CCActionEase
    {
        #region Constructors

        public CCEaseExponentialInOut (CCFiniteTimeAction action) : base(action)
        {
        }

        #endregion Constructors


        protected internal override CCActionState StartAction(Node target)
        {
            return new CCEaseExponentialInOutState (this, target);
        }

        public override CCFiniteTimeAction Reverse ()
        {
            return new CCEaseExponentialInOut ((CCFiniteTimeAction)InnerAction.Reverse ());
        }
    }


    #region Action state

    public class CCEaseExponentialInOutState : CCActionEaseState
    {
        public CCEaseExponentialInOutState (CCEaseExponentialInOut action, Node target) : base (action, target)
        {
        }

        public override void Update (float time)
        {
            InnerActionState.Update (CCEaseMath.ExponentialInOut (time));
        }
    }

    #endregion Action state
}