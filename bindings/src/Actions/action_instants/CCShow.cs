namespace Urho
{
    public class CCShow : CCActionInstant
    {
        #region Constructors

        public CCShow ()
        {
        }

        #endregion Constructors

        protected internal override CCActionState StartAction(Node target)
        {
            return new CCShowState (this, target);

        }

        public override CCFiniteTimeAction Reverse ()
        {
            return (new CCHide ());
        }

    }

    public class CCShowState : CCActionInstantState
    {

        public CCShowState (CCShow action, Node target)
            : base (action, target)
        {   
            target.SetEnabled(true);
        }

    }

}