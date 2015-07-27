using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Urho
{
	public enum LogLevel
	{
		Raw = -1,
		Default = 0,
		Info = 1;
		Warning = 2;
		Error = 3;
		None = 4;
	}

	public partial class Log
	{
		public static void Write (int level, string message)
		{
			Log_Write (level, message);
		}
		
		public static void Write (LogLevel level, string message)
		{
			Log_Write ((int)level, message);
		}

		public int Level {
			get {
				return GetLevel ();
			}
			set {
				SetLevel (value);
			}
		}
		
		public LogLevel LogLevel {
			get {
				return (LogLevel)GetLevel ();
			}
			set {
				SetLevel ((int)value);
			}
		}
	}
}
