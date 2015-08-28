namespace Urho
{
    public class CCJumpBy : CCFiniteTimeAction
    {   
        #region Properties

        public uint Jumps { get; protected set; }
        public float Height { get; protected set; }
        public Vector3 Position { get; protected set; }

        #endregion Properties


        #region Constructors

        public CCJumpBy (float duration, Vector3 position, float height, uint jumps) : base (duration)
        {
            Position = position;
            Height = height;
            Jumps = jumps;
        }

        #endregion Constructors

        protected internal override CCActionState StartAction(Node target)
        {
            return new CCJumpByState (this, target);
        }

        public override CCFiniteTimeAction Reverse ()
        {
            return new CCJumpBy (Duration, new Vector3 (-Position.X, -Position.Y, -Position.Z), Height, Jumps);
        }
    }

    public class CCJumpByState : CCFiniteTimeActionState
    {
        protected Vector3 Delta;
        protected float Height;
        protected uint Jumps;
        protected Vector3 StartPosition;
        protected Vector3 P;

        public CCJumpByState (CCJumpBy action, Node target)
            : base (action, target)
        { 
            Delta = action.Position;
            Height = action.Height;
            Jumps = action.Jumps;
            P = StartPosition = target.Position;
        }

        public override void Update (float time)
        {
            if (Target != null)
            {
                // Is % equal to fmodf()???
                float frac = (time * Jumps) % 1f;
                float y = Height * 4f * frac * (1f - frac);
                y += Delta.Y * time;
                float x = Delta.X * time;

                Vector3 currentPos = Target.Position;

                Vector3 diff = currentPos - P;
                StartPosition = diff + StartPosition;

                Vector3 newPos = StartPosition + new Vector3 (x, y, currentPos.Z);
                Target.Position = newPos;

                P = newPos;
            }
        }
    }

}