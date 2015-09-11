If you add MonoUrho from nuget via Xamarin Studio:

Set "Build" action to "Content" and "Copy to Output Directory" property to "Copy if newer" for libmono-urho.dylib file.

To launch application please use the following snippet:
ApplicationLauncher.Run(() => new HelloWorldGame(new Context()));

and make sure you've added needed assets (Resources\CoreData and Resources\Data).