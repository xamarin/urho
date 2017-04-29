ZIP=powershell.exe -nologo -noprofile -command "& { Add-Type -A 'System.IO.Compression.FileSystem'; [IO.Compression.ZipFile]::CreateFromDirectory

.PHONY: ZipTemplates ZipSharpRealityTemplates

ZipTemplates: Clean
	$(ZIP)('VsSolutionTemplate', 'VSIX/ProjectTemplates/UrhoSharp.zip'); }" && $(ZIP)('SharpReality', 'SharpRealityVsix/ProjectTemplates/HoloUrhoSharp.zip'); }"	

ReleaseVsix: ZipTemplates ZipSharpRealityTemplates
	msbuild VSIX.sln /t:Build /p:Configuration=Release
	
Clean:
	rm -f "VSIX/ProjectTemplates/UrhoSharp.zip" && rm -f "SharpRealityVsix/ProjectTemplates/HoloUrhoSharp.zip"