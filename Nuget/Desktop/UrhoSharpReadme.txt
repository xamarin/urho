Use the following code snippet in order to run your game application:

UrhoEngine.Init();
new MyGame().Run();


If you have some custom assets (built-in are not enough for you):

new MyGame(new ApplicationOptions("Data")).Run();

if pathToAssets is null - current directory will be used. The pathToAssets should contain "Data" folder with all your assets.




CoreData assets and native libs for OSX (fat) and Windows (x64 only) are added via .targets file by the nuget package.