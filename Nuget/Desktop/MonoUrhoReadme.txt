If you add MonoUrho from nuget via Xamarin Studio:

Set "Copy to Output Directory" property to "Copy if newer" for libmono-urho.dylib and mono-urho.dll

To launch application please use the following snippet:
ApplicationLauncher.Run(() => new HelloWorldGame(new Context()), "%path to folder containing CoreData and Data resources%");

libmono-urho.dylib - 32bit/64bit fat Urho3D native library for Mac OS
mono-urho.dll - 64bit Urho3D native library for Windows.