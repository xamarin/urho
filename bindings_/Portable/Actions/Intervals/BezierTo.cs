using Urho;
namespace Urho.Actions
{
	public class BezierTo : BezierBy
	{
		#region Constructors

		public BezierTo (float t, BezierConfig c)
			: base (t, c)
		{
		}

		#endregion Constructors


		protected internal override ActionState StartAction(Node target)
		{
			return new BezierToState (this, target);
		}
	}

	public class BezierToState : BezierByState
	{

		public BezierToState (BezierBy action, Node target)
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