using System;

namespace Urho
{
    public class CCEaseSineOut : CCActionEase
    {
        #region Constructors

        public CCEaseSineOut (CCFiniteTimeAction action) : base (action)
        {
        }

        #endregion Constructors


        protected internal override CCActionState StartAction(Node target)
        {
            return new CCEaseSineOutState (this, target);
        }

        public override CCFiniteTimeAction Reverse ()
        {
            return new CCEaseSineIn ((CCFiniteTimeAction)InnerAction.Reverse ());
        }
    }


    #region Action state

    public class CCEaseSineOutState : CCActionEaseState
    {
        public CCEaseSineOutState (CCEaseSineOut action, Node target) : base (action, target)
        {
        }

        public override void Update (float time)
        {
            InnerActionState.Update (CCEaseMath.SineOut (time));
        }
    }

    #endregion Action state
}