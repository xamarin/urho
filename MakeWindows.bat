@echo off
:: usage: "MakeWindows.bat x64|x86 Release|Debug 2015|2017 OpenGL|DirectX"

:: x64 or x86
set "PLATFORM=%~1"
:: Release or Debug
set "CONFIG=%~2"
:: 2015 or 2017
set "VSVER=%~3%"
:: OpenGL or DirectX
set "RENDERER=%~4%"


if "%PLATFORM%" == "" echo ERROR: PLATFORM is not set, example of usage: "MakeWindows.bat x64 Release 2017 OpenGL" && pause && exit /b
if "%CONFIG%" == "" echo ERROR: CONFIG is not set, example of usage: "MakeWindows.bat x64 Release 2017 OpenGL" && pause && exit /b
if "%VSVER%" == "" echo ERROR: VS_VER is not set, example of usage: "MakeWindows.bat x64 Release 2017 OpenGL" && pause && exit /b

if "%VSVER%" == "2015" set "VS_VER=14"
if "%VSVER%" == "2017" set "VS_VER=15"
if "%PLATFORM%" == "x64" (set "TARGET=Visual Studio %VS_VER% Win64") else (set "TARGET=Visual Studio %VS_VER%")
if "%PLATFORM%" == "x64" (set "PLATFORM_SUFFIX=Win64") else (set "PLATFORM_SUFFIX=Win32")
if "%PLATFORM%" == "x64" (set "MONOURHO_PLATFORM=x64") else (set "MONOURHO_PLATFORM=Win32")
if "%CONFIG%" == "Release" (set "CONFIG_SUFFIX=") else (set "CONFIG_SUFFIX=_d")
if "%RENDERER%" == "OpenGL" (set "RENDERER_FLAGS=-DURHO3D_OPENGL=1") else (set "RENDERER_FLAGS=-DURHO3D_OPENGL=0 -DURHO3D_D3D11=1")
if "%RENDERER%" == "OpenGL" (set "RENDERER_SUFFIX=OPENGL") else (set "RENDERER_SUFFIX=D3D11")
if "%RENDERER%" == "OpenGL" (set "MONOURHO_SUFFIX=") else (set "MONOURHO_SUFFIX=D3D")


set "URHO3D_SRC_DIR=Urho3D/Source"

del Urho3D\Urho3D_Windows\CMakeCache.txt 2>NUL
cd %URHO3D_SRC_DIR% 

@echo on

cmake -E make_directory ../Urho3D_Windows
cmake -E chdir ../Urho3D_Windows cmake -G "%TARGET%" ../Urho3D_Windows %RENDERER_FLAGS% -DURHO3D_WEBP=0 -DURHO3D_PCH=0 -DURHO3D_LUA=0 -DURHO3D_ANGELSCRIPT=0 -VS=%VS_VER% ../../%URHO3D_SRC_DIR%/

cd ../Urho3D_Windows

cmake --build . --target Urho3D --config %CONFIG% 
cmake --build . --target PackageTool --config %CONFIG%
cmake --build . --target AssetImporter --config %CONFIG%

copy lib\Urho3D%CONFIG_SUFFIX%.lib lib\Urho3D%CONFIG_SUFFIX%_%PLATFORM_SUFFIX%_%RENDERER_SUFFIX%.lib /Y

cd ../..

:: msbuild Urho3D\Urho3D_Windows\MonoUrho.Windows\MonoUrho.Windows%MONOURHO_SUFFIX%.vcxproj /p:Configuration=%CONFIG% /p:Platform=%MONOURHO_PLATFORM%
