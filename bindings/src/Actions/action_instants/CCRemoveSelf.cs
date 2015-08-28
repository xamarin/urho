using System;

namespace Urho
{
    public class CCRemoveSelf : CCActionInstant
    {
        protected internal override CCActionState StartAction(Node target)
        {
            return new CCRemoveSelfState (this, target);

        }

        public override CCFiniteTimeAction Reverse ()
        {
            throw new NotSupportedException();
        }
    }

    public class CCRemoveSelfState : CCActionInstantState
    {
        public CCRemoveSelfState (CCRemoveSelf action, Node target)
            : base (action, target)
        {   
        }

        public override void Update (float time)
        {
			Target.Parent.RemoveChild(Target);
        }
    }
}