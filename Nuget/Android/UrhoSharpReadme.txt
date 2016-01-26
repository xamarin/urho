In order to run a game:
1) If you need custom assets - add "Data" folder containing your assets to "Assets" project folder and make sure all files have "AndroidAsset" build action.
2) use the following code snippet in order to open a new fullscreen activity with the game:


UrhoSurface.RunInActivity<MyGame>(new ApplicationOptions("Data"));

or 

UrhoSurface.RunInActivity<MyGame>(); 

if you don't need any custom assets and built-in ones (CoreData) are enough for you.




CoreData assets and native libs for x86, armeabi, armeabi-v7a are added via .targets file by the nuget package.