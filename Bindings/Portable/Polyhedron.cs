using System;

namespace Urho
{
	partial class Polyhedron
	{
		IntPtr handle;

		public IntPtr Handle => handle;

		[Preserve]
		public Polyhedron(IntPtr handle)
		{
			this.handle = handle;
		}
	}
}
