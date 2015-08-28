namespace Urho
{
    public class CCEaseBounceIn : CCActionEase
    {
        #region Constructors

        public CCEaseBounceIn (CCFiniteTimeAction action) : base (action)
        {
        }

        #endregion Constructors


        protected internal override CCActionState StartAction(Node target)
        {
            return new CCEaseBounceInState (this, target);
        }

        public override CCFiniteTimeAction Reverse ()
        {
            return new CCEaseBounceOut ((CCFiniteTimeAction)InnerAction.Reverse ());
        }
    }


    #region Action state

    public class CCEaseBounceInState : CCActionEaseState
    {
        public CCEaseBounceInState (CCEaseBounceIn action, Node target) : base (action, target)
        {
        }

        public override void Update (float time)
        {
            InnerActionState.Update (CCEaseMath.BounceIn (time));
        }
    }

    #endregion Action state
}