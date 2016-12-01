using System;

namespace Urho.Shapes
{
	public class Torus : Shape
	{
		[Preserve]
		public Torus() : base() { }
		[Preserve]
		public Torus(IntPtr handle) : base(handle) { }

		protected override string ModelResource => "Models/Torus.mdl";
	}
}