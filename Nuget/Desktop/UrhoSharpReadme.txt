Use the following code snippet in order to show a game:

UrhoEngine.Init(pathToAssets);
new MyGame(new Context()).Run();

if pathToAssets is null - current directory will be used. The pathToAssets should contain "Data" folder with all your assets.





CoreData assets and native libs for OSX (fat) and Windows (x64 only) are added via .targets file by the nuget package.