namespace Urho
{
    public class CCBezierTo : CCBezierBy
    {
        #region Constructors

        public CCBezierTo (float t, CCBezierConfig c)
            : base (t, c)
        {
        }

        #endregion Constructors


        protected internal override CCActionState StartAction(Node target)
        {
            return new CCBezierToState (this, target);

        }

    }

    public class CCBezierToState : CCBezierByState
    {

        public CCBezierToState (CCBezierBy action, Node target)
            : base (action, target)
        { 
            var config = BezierConfig;

            config.ControlPoint1 -= StartPosition;
            config.ControlPoint2 -= StartPosition;
            config.EndPosition -= StartPosition;

            BezierConfig = config;
        }

    }

}