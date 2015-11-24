using Urho;
namespace Urho.Actions
{
	public class ScaleTo : FiniteTimeAction
	{
		public float EndScaleX { get; }
		public float EndScaleY { get; }
		public float EndScaleZ { get; }


		#region Constructors

		public ScaleTo (float duration, float scale) : this (duration, scale, scale, scale)
		{
		}

		public ScaleTo (float duration, float scaleX, float scaleY, float scaleZ) : base (duration)
		{
			EndScaleX = scaleX;
			EndScaleY = scaleY;
			EndScaleZ = scaleZ;
		}

		#endregion Constructors

		public override FiniteTimeAction Reverse()
		{
			throw new System.NotImplementedException ();
		}

		protected internal override ActionState StartAction(Node target)
		{
			return new ScaleToState (this, target);
		}
	}

	public class ScaleToState : FiniteTimeActionState
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

		public ScaleToState (ScaleTo action, Node target)
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