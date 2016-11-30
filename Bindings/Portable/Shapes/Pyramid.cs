using System;

namespace Urho.Shapes
{
	public class Pyramid : Shape
	{
		public Pyramid() : base() { }
		public Pyramid(IntPtr handle) : base(handle) { }

		protected override string ModelResource => "Models/Pyramid.mdl";
	}
}