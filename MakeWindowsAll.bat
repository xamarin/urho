set "CONFIG=%~1"

if NOT "%CONFIG%" == "Debug" (set "CONFIG=Release")

call MakeWindows.bat x64 %CONFIG% 2017 OpenGL
call MakeWindows.bat x86 %CONFIG% 2017 OpenGL
call MakeWindows.bat x64 %CONFIG% 2017 DirectX
call MakeWindows.bat x86 %CONFIG% 2017 DirectX
