//
// Runtime C# support
//
// Authors:
//   Miguel de Icaza (miguel@xamarin.com)
//
// Copyrigh 2015 Xamarin INc
//
using System;
using static System.Console;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Urho {
	
	public partial class Runtime {
		public static T LookupObject<T> (IntPtr ptr) where T:RefCounted
		{
			if (ptr == IntPtr.Zero)
				return null;
			try {
				return (T)Activator.CreateInstance (
					typeof(T), BindingFlags.Instance|BindingFlags.NonPublic, null, new object [] { ptr }, null);
			} catch {
				Console.WriteLine ($"Failed to instantiate a {typeof (T)} from {Environment.StackTrace}");
				throw;
			}
		}
				
	}
}
