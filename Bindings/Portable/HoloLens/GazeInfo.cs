namespace Urho.HoloLens
{
	public class GazeInfo
	{
		public Vector3 Up { get; set; }
		public Vector3 Forward { get; set; }
		public Vector3 Position { get; set; }

		public GazeInfo(Vector3 up, Vector3 forward, Vector3 position)
		{
			Up = up;
			Forward = forward;
			Position = position;
		}

#if UWP_HOLO
		public static GazeInfo FromHeadPose(Windows.Perception.People.HeadPose pose)
		{
			if (pose == null)
				return null;

			var forwardDx = pose.ForwardDirection;
			var posDx = pose.Position;
			var upDx = pose.UpDirection;
			return new GazeInfo(
				new Vector3(upDx.X, upDx.Y, -upDx.Z),
				new Vector3(forwardDx.X, forwardDx.Y, -forwardDx.Z),
				new Vector3(posDx.X, posDx.Y, -posDx.Z));
		}
#endif
	}
}
