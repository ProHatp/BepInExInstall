
@echo off
set /p VERSION=Enter version (ex: v1.0.0): 
if "%VERSION%"=="" (
    echo No version entered. Aborting.
    exit /b 1
)

echo Creating tag %VERSION%...
git add .
git commit -m "Release %VERSION%"
git tag %VERSION%
git push origin main
git push origin %VERSION%

echo Done! Release tag pushed to GitHub.
pause
