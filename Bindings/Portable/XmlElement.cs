using System;

namespace Urho.Resources
{
	partial class XmlElement
	{
		IntPtr handle;

		[Preserve]
		public XmlElement(IntPtr handle)
		{
			this.handle = handle;
		}
	}
}
