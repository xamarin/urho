ZIP=powershell.exe -nologo -noprofile -command "& { Add-Type -A 'System.IO.Compression.FileSystem'; [IO.Compression.ZipFile]::CreateFromDirectory

.PHONY: ZipTemplates ZipHoloLensTemplates

ZipTemplates: Clean
	$(ZIP)('VsSolutionTemplate', 'VSIX/ProjectTemplates/UrhoSharp.zip'); }" && $(ZIP)('HoloLens', 'HoloLensVsix/ProjectTemplates/HoloUrhoSharp.zip'); }"	

ReleaseVsix: ZipTemplates ZipHoloLensTemplates
	msbuild VSIX.sln /t:Build /p:Configuration=Release
	
Clean:
	rm -f "VSIX/ProjectTemplates/UrhoSharp.zip" && rm -f "HoloLensVsix/ProjectTemplates/HoloUrhoSharp.zip"