using System;

namespace Urho
{
	static class LogSharp // TODO: remove
	{
		public static LogSharpLevel LogLevel { get; set; } = LogSharpLevel.Debug;

		public static void Error(string str, Exception exc = null) => Write(LogSharpLevel.Error, $"Exception: {exc}. " + str);
		public static void Warn(string str) => Write(LogSharpLevel.Warn, str);
		public static void Debug(string str) => Write(LogSharpLevel.Debug, str);
		public static void Trace(string str) => Write(LogSharpLevel.Trace, str);

		static void Write(LogSharpLevel level, string str)
		{
			if (level < LogLevel)
				return;
#if __ANDROID__
			Android.Util.LogPriority logPriority = Android.Util.LogPriority.Verbose;
			switch (level)
			{
				case LogSharpLevel.Trace:
					logPriority = Android.Util.LogPriority.Verbose;
					break;
				case LogSharpLevel.Debug:
					logPriority = Android.Util.LogPriority.Debug;
					break;
				case LogSharpLevel.Warn:
					logPriority = Android.Util.LogPriority.Warn;
					break;
				case LogSharpLevel.Error:
					logPriority = Android.Util.LogPriority.Error;
					break;
			}
			Android.Util.Log.WriteLine(logPriority, "UrhoSharp", str);
#else
			System.Diagnostics.Debug.WriteLine($"{level}: {str}");
#endif
		}

	}

	public enum LogSharpLevel
	{
		Trace,
		Debug,
		Warn,
		Error
	}
}
