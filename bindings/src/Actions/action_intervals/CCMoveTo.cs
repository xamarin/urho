namespace Urho
{
    public class CCMoveTo : CCMoveBy
    {
        protected Vector3 EndPosition;

        #region Constructors

        public CCMoveTo (float duration, Vector3 position) : base (duration, position)
        {
            EndPosition = position;
        }

        #endregion Constructors

        public Vector3 PositionEnd {
            get { return EndPosition; }
        }

        protected internal override CCActionState StartAction(Node target)
        {
            return new CCMoveToState (this, target);

        }
    }

    public class CCMoveToState : CCMoveByState
    {

        public CCMoveToState (CCMoveTo action, Node target)
            : base (action, target)
        { 
            StartPosition = target.Position;
            PositionDelta = action.PositionEnd - target.Position;
        }

        public override void Update (float time)
        {
            if (Target != null)
            {
				Vector3 currentPos = Target.Position;

				Vector3 newPos = StartPosition + PositionDelta * time;
                Target.Position = newPos;
                PreviousPosition = newPos;
            }
        }
    }

}