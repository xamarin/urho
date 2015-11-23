using Urho;
namespace Urho.Actions
{
	public class ScaleBy : ScaleTo
	{
		#region Constructors


		public ScaleBy (float duration, float scale) : base (duration, scale)
		{
		}

		public ScaleBy (float duration, float scaleX, float scaleY, float scaleZ) : base (duration, scaleX, scaleY, scaleZ)
		{
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new ScaleByState (this, target);

		}

		public override FiniteTimeAction Reverse ()
		{
			return new ScaleBy (Duration, 1 / EndScaleX, 1 / EndScaleY, 1 / EndScaleZ);
		}

	}

	public class ScaleByState : ScaleToState
	{

		public ScaleByState (ScaleTo action, Node target)
			: base (action, target)
		{ 
			DeltaX = StartScaleX * EndScaleX - StartScaleX;
			DeltaY = StartScaleY * EndScaleY - StartScaleY;
			DeltaZ = StartScaleZ * EndScaleZ - StartScaleZ;
		}
	}
}