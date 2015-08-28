using System;

namespace Urho
{
    public partial class CCEaseCustom : CCActionEase
    {
        public Func<float, float> EaseFunc { get; private set; }


        #region Constructors

        public CCEaseCustom (CCFiniteTimeAction action, Func<float, float> easeFunc) : base (action)
        {
            EaseFunc = easeFunc;
        }

        #endregion Constructors


        protected internal override CCActionState StartAction(Node target)
        {
            return new CCEaseCustomState (this, target);
        }

        public override CCFiniteTimeAction Reverse ()
        {
            return new CCReverseTime (this);
        }
    }


    #region Action state

    public class CCEaseCustomState : CCActionEaseState
    {
        protected Func<float, float> EaseFunc { get; private set; }

        public CCEaseCustomState (CCEaseCustom action, Node target) : base (action, target)
        {
            EaseFunc = action.EaseFunc;
        }

        public override void Update (float time)
        {
            InnerActionState.Update (EaseFunc (time));
        }
    }

    #endregion Action state
}