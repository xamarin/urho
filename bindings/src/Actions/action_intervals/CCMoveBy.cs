namespace Urho
{
    public class CCMoveBy : CCFiniteTimeAction
    {
        #region Constructors

        public CCMoveBy (float duration, Vector3 position) : base (duration)
        {
            PositionDelta = position;
        }

        #endregion Constructors

        public Vector3 PositionDelta { get; private set; }

        protected internal override CCActionState StartAction(Node target)
        {
            return new CCMoveByState (this, target);
        }

        public override CCFiniteTimeAction Reverse ()
        {
            return new CCMoveBy (Duration, new Vector3(-PositionDelta.X, -PositionDelta.Y, -PositionDelta.Z));
        }
    }

    public class CCMoveByState : CCFiniteTimeActionState
    {
        protected Vector3 PositionDelta;
        protected Vector3 EndPosition;
        protected Vector3 StartPosition;
        protected Vector3 PreviousPosition;

        public CCMoveByState (CCMoveBy action, Node target)
            : base (action, target)
        { 
            PositionDelta = action.PositionDelta;
            PreviousPosition = StartPosition = target.Position;
        }

        public override void Update (float time)
        {
            if (Target == null)
                return;

            var currentPos = Target.Position;
            var diff = currentPos - PreviousPosition;
            StartPosition = StartPosition + diff;
			Vector3 newPos = StartPosition + PositionDelta * time;
            Target.Position = newPos;
            PreviousPosition = newPos;
        }
    }

}