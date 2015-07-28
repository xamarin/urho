using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Urho
{

	public partial class Log
	{
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
