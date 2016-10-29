using System;
namespace Urho
{
	public class MemoryBuffer : IDeserializer, ISerializer
	{
		public MemoryBuffer()
		{
		}

		public IntPtr Handle { get; private set; }
	}
}
