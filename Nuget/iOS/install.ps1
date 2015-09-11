param($installPath, $toolsPath, $package, $project)

$nativeFile = $project.ProjectItems.Item("libmono-urho.dylib")

$nativeFile.Properties.Item("CopyToOutputDirectory").Value = 2
$nativeFile.Properties.Item("ItemType").Value = "Content"