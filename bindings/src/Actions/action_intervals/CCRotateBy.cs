namespace Urho
{
    public class CCRotateBy : CCFiniteTimeAction
    {
        public float AngleX { get; }
        public float AngleY { get; }
		public float AngleZ { get; }

		#region Constructors

		public CCRotateBy (float duration, float deltaAngleX, float deltaAngleY, float deltaAngleZ) : base (duration)
        {
            AngleX = deltaAngleX;
            AngleY = deltaAngleY;
	        AngleZ = deltaAngleZ;
        }

        public CCRotateBy (float duration, float deltaAngle) : this (duration, deltaAngle, deltaAngle, deltaAngle)
        {
        }

        #endregion Constructors

        protected internal override CCActionState StartAction(Node target)
        {
            return new CCRotateByState (this, target);

        }

        public override CCFiniteTimeAction Reverse ()
        {
            return new CCRotateBy (Duration, -AngleX, -AngleY, -AngleZ);
        }
    }

    public class CCRotateByState : CCFiniteTimeActionState
    {
        protected float AngleX { get; set; }

        protected float AngleY { get; set; }

		protected float AngleZ { get; set; }

		protected float StartAngleX { get; set; }

        protected float StartAngleY { get; set; }

		protected float StartAngleZ { get; set; }

		public CCRotateByState (CCRotateBy action, Node target)
            : base (action, target)
        { 
            AngleX = action.AngleX;
            AngleY = action.AngleY;
			AngleZ = action.AngleZ;

			var rotation = target.Rotation;
            StartAngleX = rotation.X;
            StartAngleY = rotation.Y;
			StartAngleZ = rotation.Z;

        }

        public override void Update (float time)
        {
	        Target?.Rotate(new Quaternion(StartAngleX + AngleX * time, StartAngleY + AngleX * time, StartAngleZ + AngleX * time), TransformSpace.Local);
        }
    }
}