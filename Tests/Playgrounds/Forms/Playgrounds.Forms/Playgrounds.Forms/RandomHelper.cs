using System;

namespace Playgrounds.Forms
{
	public static class RandomHelper
	{
		static readonly Random random = new Random();

		/// <summary>
		/// Return a random float between 0.0 (inclusive) and 1.0 (exclusive.)
		/// </summary>
		public static float NextRandom() { return (float)random.NextDouble(); }

		/// <summary>
		/// Return a random float between 0.0 and range, inclusive from both ends.
		/// </summary>
		public static float NextRandom(float range) { return (float)random.NextDouble() * range; }

		/// <summary>
		/// Return a random float between min and max, inclusive from both ends.
		/// </summary>
		public static float NextRandom(float min, float max) { return (float)((random.NextDouble() * (max - min)) + min); }

		/// <summary>
		/// Return a random integer between min and max - 1.
		/// </summary>
		public static int NextRandom(int min, int max) { return random.Next(min, max); }
	}
}
