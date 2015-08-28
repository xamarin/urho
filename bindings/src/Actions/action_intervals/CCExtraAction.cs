namespace Urho
{
    // Extra action for making a CCSequence or CCSpawn when only adding one action to it.
    internal class CCExtraAction : CCFiniteTimeAction
    {
        public override CCFiniteTimeAction Reverse ()
        {
            return new CCExtraAction ();
        }

        protected internal override CCActionState StartAction(Node target)
        {
            return new CCExtraActionState (this, target);

        }

        #region Action State

        public class CCExtraActionState : CCFiniteTimeActionState
        {

            public CCExtraActionState (CCExtraAction action, Node target)
                : base (action, target)
            {
            }

            protected internal override void Step (float dt)
            {
            }

            public override void Update (float time)
            {
            }
        }

        #endregion Action State
    }
}