.PHONY: all pack clean run profile help
all: run
pack: clean
	mkdir ..\..\repo
	xcopy /E /H ..\..\ivs_projekt2 ..\..\repo
	powershell Compress-Archive -Force ../../repo odevzdat.zip
	rmdir /Q /S ..\..\repo 
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

	