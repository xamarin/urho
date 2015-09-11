param($installPath, $toolsPath, $package, $project)

$nativeWinFile = $project.ProjectItems.Item("mono-urho.dll")
$nativeMacFile = $project.ProjectItems.Item("libmono-urho.dylib")

# set 'Copy To Output Directory' to 'Copy if newer'
$nativeWinFile.Properties.Item("CopyToOutputDirectory").Value = 2
$nativeMacFile.Properties.Item("CopyToOutputDirectory").Value = 2