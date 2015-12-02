In order to run a game:
1) Add "Data" folder containing your assets to "Resources" project folder and make sure all files have "BundleResource" build action.
2) use the following code snippet in order to open a new fullscreen activity with the game:

UrhoEngine.Init();
new MyGame().Run();//Run is not blocking for iOS






CoreData assets and native libs for arm64, armv7 and i386 are added via .targets file by the nuget package.