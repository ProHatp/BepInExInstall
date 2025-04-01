
@echo off
echo Building BepInExInstall in Release mode...
dotnet build BepInExInstall.sln -c Release
if %ERRORLEVEL% NEQ 0 (
    echo Build failed!
    exit /b %ERRORLEVEL%
)
echo Build succeeded!
pause
