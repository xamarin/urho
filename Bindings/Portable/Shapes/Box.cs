using System;

namespace Urho.Shapes
{
	public class Box : Shape
	{
		[Preserve]
		public Box() : base() { }
		[Preserve]
		public Box(IntPtr handle) : base(handle) { }

		protected override string ModelResource => "Models/Box.mdl";
	}
}
