using System;

namespace Urho
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
