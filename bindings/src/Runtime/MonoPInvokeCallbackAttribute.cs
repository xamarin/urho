using System;

namespace Urho
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class MonoPInvokeCallbackAttribute : Attribute
	{
		public Type Type { get; set; }

		public MonoPInvokeCallbackAttribute(Type type)
		{
			Type = type;
		}
	}
}
