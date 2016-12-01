using System;

namespace Urho.Shapes
{
	public class Pyramid : Shape
	{
		[Preserve]
		public Pyramid() : base() { }
		[Preserve]
		public Pyramid(IntPtr handle) : base(handle) { }

		protected override string ModelResource => "Models/Pyramid.mdl";
	}
}