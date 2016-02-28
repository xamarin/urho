using System;

namespace Urho.Resources
{
	partial class XmlElement
	{
		IntPtr handle;

		public XmlElement(IntPtr handle)
		{
			this.handle = handle;
		}
	}
}
