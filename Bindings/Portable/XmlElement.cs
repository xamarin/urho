using System;

namespace Urho.Resources
{
	partial class XmlElement
	{
		IntPtr handle;

		public IntPtr Handle => handle;

		[Preserve]
		public XmlElement(IntPtr handle)
		{
			this.handle = handle;
		}
	}
}
