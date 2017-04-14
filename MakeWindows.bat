@echo off
:: usage: "MakeWindows.bat x64|x86 Release|Debug OpenGL|D3D11"

:: x64 or x86
set "PLATFORM=%~1"
:: Release or Debug
set "CONFIG=%~2"

if "%PLATFORM%" == "" echo ERROR: PLATFORM is not set, example of usage: "MakeWindows.bat x64 Release" && pause && exit /b
if "%CONFIG%" == "" echo ERROR: CONFIG is not set, example of usage: "MakeWindows.bat x64 Release" && pause && exit /b

if "%PLATFORM%" == "x64" (set "TARGET=Visual Studio 14 Win64") else (set "TARGET=Visual Studio 14")

set "RENDERER_FLAGS="
set "URHO3D_SRC_DIR=Urho3D/Source"
cd %URHO3D_SRC_DIR% 

rm -rf Urho3D/Urho3D_Windows/CMakeFiles
rm -rf Urho3D/Urho3D_Windows/CMakeCache.txt

@echo on

cmake -E make_directory ../Urho3D_Windows
cmake -E chdir ../Urho3D_Windows cmake -G "%TARGET%" ../Urho3D_Windows -DURHO3D_OPENGL=1 -DURHO3D_PCH=0 -DURHO3D_LUA=0 -DURHO3D_ANGELSCRIPT=0 -VS=14 ../../%URHO3D_SRC_DIR%/

cd ../Urho3D_Windows

cmake --build . --target Urho3D --config %CONFIG% 
cmake --build . --target PackageTool --config %CONFIG%
cmake --build . --target AssetImporter --config %CONFIG%

cd ../..

"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" Urho3D\Urho3D_Windows\MonoUrho.Windows\MonoUrho.Windows.vcxproj /p:PlatformToolset=v140 /p:Configuration=%CONFIG% /p:Platform=%PLATFORM%