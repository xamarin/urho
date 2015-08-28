namespace Urho
{
    public class CCPlace : CCActionInstant
    {
        public Vector3 Position { get; private set; }

        #region Constructors

        public CCPlace (Vector3 pos)
        {
            Position = pos;
        }

        public CCPlace (int posX, int posY, int posZ = 0)
        {
            Position = new Vector3(posX, posY, posZ);
        }

        #endregion Constructors

        protected internal override CCActionState StartAction(Node target)
        {
            return new CCPlaceState (this, target);
        }
    }

    public class CCPlaceState : CCActionInstantState
    {
        public CCPlaceState (CCPlace action, Node target)
            : base (action, target)
        { 
            Target.Position = action.Position;
        }
    }
}