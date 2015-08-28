using System;

namespace Urho
{
	public class FadeIn : FiniteTimeAction
	{
		#region Constructors

		public FadeIn (float durataion) : base (durataion)
		{
		}

		#endregion Constructors

		protected internal override ActionState StartAction(Node target)
		{
			return new FadeInState (this, target);

		}

		public override FiniteTimeAction Reverse ()
		{
			return new FadeOut (Duration);
		}
	}

	public class FadeInState : FiniteTimeActionState
	{
		StaticSprite2D staticSprite;

		protected uint Times { get; set; }

		protected bool OriginalState { get; set; }

		public FadeInState (FadeIn action, Node target)
			: base (action, target)
		{
			staticSprite = Target.GetComponent<StaticSprite2D>();
			if (staticSprite == null)
			{
				throw new NotSupportedException("The node should have StaticSprite2D");
			}
		}

		public override void Update (float time)
		{
			staticSprite.Alpha = time;
		}
	}
}