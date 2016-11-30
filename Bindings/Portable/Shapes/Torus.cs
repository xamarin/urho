using System;

namespace Urho.Shapes
{
	public class Torus : Shape
	{
		public Torus() : base() { }
		public Torus(IntPtr handle) : base(handle) { }

		protected override string ModelResource => "Models/Torus.mdl";
	}
}