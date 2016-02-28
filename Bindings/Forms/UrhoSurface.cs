using System;
using System.Threading.Tasks;

namespace Urho.Forms
{
	public class UrhoSurface : Xamarin.Forms.View
	{
		internal Func<Type, ApplicationOptions, Task<Urho.Application>> UrhoApplicationLauncher { get; set; }

		public async Task<TUrhoApplication> Show<TUrhoApplication>(ApplicationOptions options) where TUrhoApplication : Urho.Application
		{
			if (UrhoApplicationLauncher == null)
				throw new InvalidOperationException("Impl assembly is not referenced");
			return (TUrhoApplication)await UrhoApplicationLauncher(typeof (TUrhoApplication), options);
		}
	}
}
