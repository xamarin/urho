using System;

namespace Urho
{
	public static class Randoms
	{
		static readonly Random random = new Random();

		public static float Next()
		{
			return (float)random.NextDouble();
		}

		public static float Next(float min, float max)
		{
			return (float)((random.NextDouble() * (max - min)) + min);
		}

		public static float NextNormal()
		{
			float result = 0;
			const int samples = 6;//12?
			for (int i = 0; i < samples; i++)
			{
				result += (float)(random.NextDouble() / samples);
			}
			return result;
		}

		public static float NextNormal(float min, float max)
		{
			return NextNormal() * (max - min) + min;
		}

		public static Color NextColor()
		{
			return new Color(Next(), Next(), Next());
		}

		public static Color NextNormalColor()
		{
			return new Color(NextNormal(), NextNormal(), NextNormal());
		}
	}
}
