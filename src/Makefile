.PHONY: all pack clean run profile help
all: run
pack: clean
	mkdir ..\..\xbacae00_xskvar09_xzavad18_xnovot2a\repo
	xcopy /E /H ..\. ..\..\xbacae00_xskvar09_xzavad18_xnovot2a\repo
	powershell Compress-Archive -Force ..\..\xbacae00_xskvar09_xzavad18_xnovot2a xbacae00_xskvar09_xzavad18_xnovot2a.zip
	rmdir /Q /S ..\..\xbacae00_xskvar09_xzavad18_xnovot2a 
clean:
	for /d /r .. %%d in (obj) do @if exist "%%d" rd /s/q "%%d"
	for /d /r .. %%d in (bin) do @if exist "%%d" rd /s/q "%%d"
	for /d /r .. %%d in (packages) do @if exist "%%d" rd /s/q "%%d"
	for /d /r .. %%d in (.vs) do @if exist "%%d" rd /s/q "%%d"
run:
	Calculator\bin\Release\Calculator.exe
profile:
	type Standard.Deviation\data.txt | Standard.Deviation\bin\Release\Standard.Deviation.exe
help:
	type ..\README.md

	