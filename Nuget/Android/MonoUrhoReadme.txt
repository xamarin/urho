If you add MonoUrho from nuget via Xamarin Studio:
Set "Build" action to "AndroidNativeLibrary" to all *.so files inside Libs folder.

To launch application please use the following snippet:
ApplicationLauncher.Run(() => new HelloWorldGame(new Context()));
and make sure you've added needed Assets (Assets\CoreData and Assets\Data).