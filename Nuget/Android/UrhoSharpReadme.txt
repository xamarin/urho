In order to run a game:
1) Add "Data" folder containing your assets to "Assets" project folder and make sure all files have "AndroidAsset" build action.
2) use the following code snippet in order to open a new fullscreen activity with the game:

UrhoEngine.Init();
UrhoSurface.RunInActivity<MyGame>();







CoreData assets and native libs for x86, armeabi, armeabi-v7a are added via .targets file by the nuget package.