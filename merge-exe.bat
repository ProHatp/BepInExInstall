
@echo off
set EXENAME=BepInExInstall.exe
set OUTPUT=BepInExInstall.exe
set DLL1=MetroFramework.dll
set DLL2=MetroFramework.Fonts.dll
set ILMERGE=packages\ILMerge.Tools.2.14.1208\tools\ILMerge.exe

echo Merging files with ILMerge...
"%ILMERGE%" /target:winexe /targetplatform:v4,C:\Windows\Microsoft.NET\Framework\v4.0.30319 ^
  /out:bin\Release\%OUTPUT% bin\Release\%EXENAME% bin\Release\%DLL1% bin\Release\%DLL2%

if %ERRORLEVEL% NEQ 0 (
    echo ILMerge failed!
    exit /b %ERRORLEVEL%
)

echo Done! Generated %OUTPUT%
