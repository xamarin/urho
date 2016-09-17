namespace Urho
{	
	public partial class VertexBuffer
	{
		public void SetData(CustomGeometryVertex[] vertexData)
		{
			unsafe
			{
				fixed (CustomGeometryVertex* p = &vertexData[0])
				{
					SetData((void*)p);
				}
			}
		}

		public void SetData (float [] vertexData)
		{
			unsafe
			{
				fixed (float* p = &vertexData[0])
				{
					SetData((void*) p);
				}
			}
		}
		
	}
}
