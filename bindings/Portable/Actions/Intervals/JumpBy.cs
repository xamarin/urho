using Urho;

namespace Urho.Actions
{
	public class JumpBy : FiniteTimeAction
	{   
		#region Properties

		public uint Jumps { get; protected set; }
		public float Height { get; protected set; }
		public Vector3 Position { get; protected set; }

		#endregion Properties


		#region Constructors

		public JumpBy (float duration, Vector3 position, float height, uint jumps) : base (duration)
		{
			Position = position;
			Height = height;
			Jumps = jumps;
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new JumpByState (this, target);
		}

		public override FiniteTimeAction Reverse ()
		{
			return new JumpBy (Duration, new Vector3 (-Position.X, -Position.Y, -Position.Z), Height, Jumps);
		}
	}

	public class JumpByState : FiniteTimeActionState
	{
		protected Vector3 Delta;
		protected float Height;
		protected uint Jumps;
		protected Vector3 StartPosition;
		protected Vector3 P;

		public JumpByState (JumpBy action, Node target)
			: base (action, target)
		{ 
			Delta = action.Position;
			Height = action.Height;
			Jumps = action.Jumps;
			P = StartPosition = target.Position;
		}

		public override void Update (float time)
		{
			if (Target != null)
			{
				// Is % equal to fmodf()???
				float frac = (time * Jumps) % 1f;
				float y = Height * 4f * frac * (1f - frac);
				y += Delta.Y * time;
				float x = Delta.X * time;
				float z = Delta.Z * time;

				Vector3 currentPos = Target.Position;

				Vector3 diff = currentPos - P;
				StartPosition = diff + StartPosition;

				Vector3 newPos = StartPosition + new Vector3 (x, y, z);
				Target.Position = newPos;

				P = newPos;
			}
		}
	}

}