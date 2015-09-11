param($installPath, $toolsPath, $package, $project)

$project.ProjectItems.Item("Libs").ProjectItems.Item("armeabi").ProjectItems.Item("libmono-urho.so").Properties.Item("ItemType").Value = "AndroidNativeLibrary"
$project.ProjectItems.Item("Libs").ProjectItems.Item("armeabi-v7a").ProjectItems.Item("libmono-urho.so").Properties.Item("ItemType").Value = "AndroidNativeLibrary"
$project.ProjectItems.Item("Libs").ProjectItems.Item("x86").ProjectItems.Item("libmono-urho.so").Properties.Item("ItemType").Value = "AndroidNativeLibrary"