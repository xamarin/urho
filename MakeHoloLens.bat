@echo off
:: usage: "MakeHoloLens.bat x64|x86 Release|Debug 2015|2017"

:: x64 or x86
set "PLATFORM=%~1"
:: Release or Debug
set "CONFIG=%~2"
:: 2015 or 2017
set "VSVER=%~3%"

set "LIB_PREFIX=_d"

if "%PLATFORM%" == "" echo ERROR: PLATFORM is not set, example of usage: "MakeHoloLens.bat x64 Release 2017" && pause && exit /b
if "%CONFIG%" == "" echo ERROR: CONFIG is not set, example of usage: "MakeHoloLens.bat x64 Release 2017" && pause && exit /b
if "%VSVER%" == "" echo ERROR: VS_VER is not set, example of usage: "MakeHoloLens.bat x64 Release 2017" && pause && exit /b

if "%CONFIG%" == "Release" set "LIB_PREFIX="
if "%VSVER%" == "2015" set "VS_VER=14"
if "%VSVER%" == "2017" set "VS_VER=15"
if "%PLATFORM%" == "x64" (set "TARGET=Visual Studio %VS_VER% Win64") else (set "TARGET=Visual Studio %VS_VER%")

cd Urho3D/Source

cmake -E make_directory ../Urho3D_HoloLens
cmake -E chdir ../Urho3D_HoloLens cmake -G "%TARGET%" ../Urho3D_HoloLens -DURHO3D_D3D11=1 -DUWP=1 -DUWP_HOLO=1 -DURHO3D_NEON=0 -DURHO3D_WIN32_CONSOLE=0 -DURHO3D_NETWORK=1 -DURHO3D_FILEWATCHER=0 -DURHO3D_PROFILING=0 -DURHO3D_THREADING=0 -DURHO3D_PCH=0 -DURHO3D_LUA=0 -DURHO3D_ANGELSCRIPT=0 -VS=%VS_VER% ../../Urho3D/Source/

cd ..
xcopy Urho3D_UWP\MonoUrho.UWP\SdlStub\SDL Urho3D_HoloLens\include\Urho3D\ThirdParty\SDL\* /Y
cd Urho3D_HoloLens
cmake --build . --target Urho3D --config %CONFIG%
copy lib\Urho3D%LIB_PREFIX%.lib lib\Urho3D%LIB_PREFIX%_%PLATFORM%.lib /Y
cd ../..

:: msbuild Urho3D\Urho3D_HoloLens\UrhoSharp.HoloLens\UrhoSharp.HoloLens.vcxproj /p:Configuration=%CONFIG% /p:Platform=%PLATFORM%