namespace Urho
{
    public class CCScaleTo : CCFiniteTimeAction
    {
        public float EndScaleX { get; }
        public float EndScaleY { get; }
		public float EndScaleZ { get; }


		#region Constructors

		public CCScaleTo (float duration, float scale) : this (duration, scale, scale, scale)
        {
        }

        public CCScaleTo (float duration, float scaleX, float scaleY, float scaleZ) : base (duration)
        {
            EndScaleX = scaleX;
            EndScaleY = scaleY;
			EndScaleZ = scaleZ;
		}

        #endregion Constructors

        public override CCFiniteTimeAction Reverse()
        {
            throw new System.NotImplementedException ();
        }

        protected internal override CCActionState StartAction(Node target)
        {
            return new CCScaleToState (this, target);
        }
    }

    public class CCScaleToState : CCFiniteTimeActionState
    {
        protected float DeltaX;
        protected float DeltaY;
		protected float DeltaZ;
		protected float EndScaleX;
        protected float EndScaleY;
		protected float EndScaleZ;
		protected float StartScaleX;
        protected float StartScaleY;
		protected float StartScaleZ;

		public CCScaleToState (CCScaleTo action, Node target)
            : base (action, target)
        {
	        var scale = target.Scale;
            StartScaleX = scale.X;
            StartScaleY = scale.Y;
			StartScaleZ = scale.Z;
			EndScaleX = action.EndScaleX;
            EndScaleY = action.EndScaleY;
			EndScaleZ = action.EndScaleZ;
			DeltaX = EndScaleX - StartScaleX;
            DeltaY = EndScaleY - StartScaleY;
			DeltaZ = EndScaleZ - StartScaleZ;
		}

        public override void Update (float time)
        {
            if (Target != null)
            {
				Target.Scale = new Vector3(StartScaleX + DeltaX * time, StartScaleY + DeltaY * time, StartScaleZ + DeltaZ * time);
            }
        }
    }
}