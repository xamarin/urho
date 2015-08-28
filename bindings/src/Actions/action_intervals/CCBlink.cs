namespace Urho
{
    public class CCBlink : CCFiniteTimeAction
    {
        public uint Times { get; private set; }


        #region Constructors

        public CCBlink (float duration, uint numOfBlinks) : base (duration)
        {
            Times = numOfBlinks;
        }

        #endregion Constructors


        protected internal override CCActionState StartAction(Node target)
        {
            return new CCBlinkState (this, target);

        }

        public override CCFiniteTimeAction Reverse ()
        {
            return new CCBlink (Duration, Times);
        }
    }

    public class CCBlinkState : CCFiniteTimeActionState
    {

        protected uint Times { get; set; }

        protected bool OriginalState { get; set; }

        public CCBlinkState (CCBlink action, Node target)
            : base (action, target)
        { 
            Times = action.Times;
            OriginalState = target.IsEnabled();
        }

        public override void Update (float time)
        {
            if (Target != null && !IsDone)
            {
                float slice = 1.0f / Times;
                // float m = fmodf(time, slice);
                float m = time % slice;
                Target.SetEnabled(m > (slice / 2));
            }
        }

        protected internal override void Stop ()
        {
            Target.SetEnabled(OriginalState);
            base.Stop ();
        }

    }
}