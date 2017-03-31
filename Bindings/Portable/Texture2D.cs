namespace Urho.Urho2D
{
	partial class Texture2D
	{
		public bool SetSize(int width, int height, uint format, TextureUsage usage = TextureUsage.Static)
		{
			return SetSize(width, height, format, usage, 1, true);
		}

		public unsafe bool SetData(uint level, int x, int y, int width, int height, byte[] data)
		{
			fixed (byte* ptr = data)
			{
				return SetData(level, x, y, width, height, ptr);
			}
		}
	}
}
