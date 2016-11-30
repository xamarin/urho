using System;

namespace Urho.Shapes
{
	public class Box : Shape
	{
		public Box() : base() { }
		public Box(IntPtr handle) : base(handle) { }

		protected override string ModelResource => "Models/Box.mdl";
	}
}
