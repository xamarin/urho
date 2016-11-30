using System;
using System.Runtime.InteropServices;

namespace Urho.Urho2D
{
	partial class Texture2D
	{
		public bool SetSize(int width, int height, uint format, TextureUsage usage = TextureUsage.Static)
		{
			return SetSize(width, height, format, usage, 1, true);
		}
	}
}
