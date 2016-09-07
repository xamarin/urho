using Urho;
using Urho.Physics;

namespace Urho.HoloLens
{
	public class RoomSimulation : Component
	{
		public override void OnAttachedToNode(Node node)
		{
			base.OnAttachedToNode(node);

			var floorNode = Node.CreateChild();
			var floor = floorNode.CreateComponent<StaticModel>();
			var body = floorNode.CreateComponent<RigidBody>();
			body.RollingFriction = 0.15f;
			var shape = floorNode.CreateComponent<CollisionShape>();
			shape.SetBox(Vector3.Zero, Vector3.Zero, Quaternion.Identity);

			floor.Model = CoreAssets.Models.Plane;
			floorNode.Scale = new Vector3(10, 1, 10);
			floorNode.Position = new Vector3(0, -2f, 0);

			var ceilingNode = floorNode.Clone(CreateMode.Replicated);
			ceilingNode.Position = new Vector3(0, 1, 0);
			ceilingNode.Rotation = new Quaternion(-180, 0, 0);

			//TODO: remove magic numbers
			CreateWall(new Vector2(10, 4), new Vector2( 0,-4), new Quaternion( 90, 0,   0));
			CreateWall(new Vector2(10, 4), new Vector2( 0, 4), new Quaternion(-90, 0,   0));
			CreateWall(new Vector2(4, 10), new Vector2( 4, 0), new Quaternion(  0, 0,  90));
			CreateWall(new Vector2(4, 10), new Vector2(-4, 0), new Quaternion(  0, 0, -90));

			var sofaNode = Node.CreateChild();
			var sofa = sofaNode.CreateComponent<StaticModel>();
			sofa.Model = CoreAssets.Models.Box;
			sofaNode.Scale = new Vector3(1, 0.4f, 2f);
			sofaNode.Position = new Vector3(3, -1.6f, 2);
		}

		void CreateWall(Vector2 scale, Vector2 position, Quaternion rotation)
		{
			var wallNode = Node.CreateChild();
			var wall = wallNode.CreateComponent<StaticModel>();
			var body = wallNode.CreateComponent<RigidBody>();
			body.RollingFriction = 0.15f;
			var shape = wallNode.CreateComponent<CollisionShape>();
			shape.SetBox(Vector3.Zero, Vector3.Zero, Quaternion.Identity);

			wall.Model = CoreAssets.Models.Plane;
			wallNode.Rotation = rotation;
			wallNode.Scale = new Vector3(scale.X, 1, scale.Y);
			wallNode.Position = new Vector3(position.X, 0, position.Y);
		}
	}
}
