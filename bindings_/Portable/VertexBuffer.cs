using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Urho
{	
	public partial class VertexBuffer
	{
		public void SetData (float [] vertexData)
		{
			unsafe {
			fixed (float *p = &vertexData [0]){
				SetData ((void *) p);
			}
			}
		}
		
	}
}
