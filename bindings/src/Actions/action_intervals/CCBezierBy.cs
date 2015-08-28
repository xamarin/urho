using System;

namespace Urho
{
    public class CCBezierBy : CCFiniteTimeAction
    {
        public CCBezierConfig BezierConfig { get; private set; }


        #region Constructors

        public CCBezierBy (float t, CCBezierConfig config) : base (t)
        {
            BezierConfig = config;
        }

        #endregion Constructors


        protected internal override CCActionState StartAction(Node target)
        {
            return new CCBezierByState (this, target);

        }

        public override CCFiniteTimeAction Reverse ()
        {
            CCBezierConfig r;

            r.EndPosition = -BezierConfig.EndPosition;
            r.ControlPoint1 = BezierConfig.ControlPoint2 + -BezierConfig.EndPosition;
            r.ControlPoint2 = BezierConfig.ControlPoint1 + -BezierConfig.EndPosition;

            var action = new CCBezierBy (Duration, r);
            return action;
        }
    }

    public class CCBezierByState : CCFiniteTimeActionState
    {
        protected CCBezierConfig BezierConfig { get; set; }

        protected Vector3 StartPosition { get; set; }

        protected Vector3 PreviousPosition { get; set; }


        public CCBezierByState (CCBezierBy action, Node target)
            : base (action, target)
        { 
            BezierConfig = action.BezierConfig;
            PreviousPosition = StartPosition = target.Position;
        }

        public override void Update (float time)
        {
            if (Target != null)
            {
                float xa = 0;
                float xb = BezierConfig.ControlPoint1.X;
                float xc = BezierConfig.ControlPoint2.X;
                float xd = BezierConfig.EndPosition.X;

                float ya = 0;
                float yb = BezierConfig.ControlPoint1.Y;
                float yc = BezierConfig.ControlPoint2.Y;
                float yd = BezierConfig.EndPosition.Y;

                float x = CCSplineMath.CubicBezier (xa, xb, xc, xd, time);
                float y = CCSplineMath.CubicBezier (ya, yb, yc, yd, time);

                Vector3 currentPos = Target.Position;
                Vector3 diff = currentPos - PreviousPosition;
                StartPosition = StartPosition + diff;

                Vector3 newPos = StartPosition + new Vector3 (x, y, StartPosition.Z);
                Target.Position = newPos;

                PreviousPosition = newPos;
            }
        }

    }

    public struct CCBezierConfig
    {
        public Vector3 ControlPoint1;
        public Vector3 ControlPoint2;
        public Vector3 EndPosition;
    }
}