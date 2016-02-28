using Urho;
namespace Urho.Actions
{
	public class Place : ActionInstant
	{
		public Vector3 Position { get; }

		#region Constructors

		public Place (Vector3 pos)
		{
			Position = pos;
		}

		public Place (int posX, int posY, int posZ = 0)
		{
			Position = new Vector3(posX, posY, posZ);
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new PlaceState (this, target);
		}
	}

	public class PlaceState : ActionInstantState
	{
		public PlaceState (Place action, Node target)
			: base (action, target)
		{ 
			Target.Position = action.Position;
		}
	}
}