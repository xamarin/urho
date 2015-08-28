namespace Urho
{
    public class CCJumpTo : CCJumpBy
    {
        #region Constructors

        public CCJumpTo (float duration, Vector3 position, float height, uint jumps) 
            : base (duration, position, height, jumps)
        {
        }

        #endregion Constructors

        protected internal override CCActionState StartAction(Node target)
        {
            return new CCJumpToState (this, target);

        }

    }

    public class CCJumpToState : CCJumpByState
    {

        public CCJumpToState (CCJumpBy action, Node target)
            : base (action, target)
        { 
            Delta = new Vector3 (Delta.X - StartPosition.X, Delta.Y - StartPosition.Y, Delta.Z - StartPosition.Z);
        }
    }

}