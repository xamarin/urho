namespace Urho
{
    public class CCToggleVisibility : CCActionInstant
    {
        #region Constructors

        public CCToggleVisibility ()
        {
        }

        #endregion Constructors


        protected internal override CCActionState StartAction(Node target)
        {
            return new CCToggleVisibilityState (this, target);

        }
    }

    public class CCToggleVisibilityState : CCActionInstantState
    {

        public CCToggleVisibilityState (CCToggleVisibility action, Node target)
            : base (action, target)
        {   
			target.SetEnabled(!target.IsEnabled());
        }
    }
}