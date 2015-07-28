using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Urho
{	
	public partial class IndexBuffer
	{
		public void SetData (short [] vertexData)
		{
			unsafe {
			fixed (float *p = &vertexData [0]){
				SetData ((void *) p);
			}
			}
		}
		
	}
}
