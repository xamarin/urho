using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Urho
{
	public partial class IndexBuffer
	{
		public void SetData (short [] vertexData)
		{
			Runtime.ValidateRefCounted(this);
			unsafe {
				fixed (short *p = &vertexData [0]){
					SetData ((void *) p);
				}
			}
		}

		public void SetData (uint [] vertexData)
		{
			Runtime.ValidateRefCounted(this);
			unsafe {
				fixed (uint* p = &vertexData [0]){
					SetData ((void *) p);
				}
			}
		}
	}
}
