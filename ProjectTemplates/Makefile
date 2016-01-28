.PHONY: ZipTemplates

ZipTemplates:
	powershell.exe -nologo -noprofile -command "& { Add-Type -A 'System.IO.Compression.FileSystem'; [IO.Compression.ZipFile]::CreateFromDirectory('VsSolutionTemplate', 'VSIX/ProjectTemplates/UrhoSharp.zip'); }"

ReleaseVsix: ZipTemplates
	msbuild VSIX.sln /t:Build /p:Configuration=Release