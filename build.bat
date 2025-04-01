
@echo off
echo Building BepInExInstall in Release mode...

REM Use MSBuild from Visual Studio 2022
"C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe" BepInExInstall.sln /p:Configuration=Release

if %ERRORLEVEL% NEQ 0 (
    echo Build failed!
    exit /b %ERRORLEVEL%
)
echo Build succeeded!
pause
