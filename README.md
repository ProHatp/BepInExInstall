
<p align="center">
  <img src="Assets/icon.ico" height="96" />
  <h1 align="center">BepInExInstall</h1>
  <p align="center">🔧 Easily install and manage BepInEx & UnityExplorer for Unity games.</p>
  <p align="center">
    <img src="https://github.com/ProHatp/BepInExInstall/actions/workflows/build-and-release.yml/badge.svg" alt="Build Status">
    <a href="https://github.com/ProHatp/BepInExInstall/releases"><img src="https://img.shields.io/github/v/release/ProHatp/BepInExInstall?label=latest" alt="Latest Release"></a>
    <a href="http://codebuilding.org"><img src="https://user-images.githubusercontent.com/7288322/34429117-c74dbd12-ecb8-11e7-896d-46369cd0de5b.png" alt="License"></a>
  </p>
</p>

---

## 📸 Preview

![Screenshot](https://via.placeholder.com/800x400.png?text=Preview+of+the+BepInExInstall+UI)

---

## 🚀 Features

- ✅ One-click BepInEx install (v5/v6 supported)
- ✅ UnityExplorer integration
- ✅ Drag & Drop game `.exe`
- ✅ Automatic Unity version & architecture detection
- ✅ Config editor (`BepInEx.cfg`)
- ✅ CLI support: `--install`, `--uninstall`
- ✅ GitHub Actions CI + auto release

---

## 🔧 How to Use

```bash
# GUI mode
Run BepInExInstall.exe and drag your game .exe

# CLI mode
BepInExInstall.exe --install "C:\Path\To\Game.exe"
BepInExInstall.exe --uninstall "C:\Path\To\Game.exe"
```

---

## 🛠️ Build Locally

```bash
git clone https://github.com/ProHatp/BepInExInstall
cd BepInExInstall
build.bat
```

Or open `BepInExInstall.sln` in Visual Studio 2019+ and build.

---

## 📦 Download

Grab the latest release from the [Releases page](https://github.com/ProHatp/BepInExInstall/releases).

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

## 🙏 Credits

- [BepInEx](https://github.com/BepInEx/BepInEx)
- [UnityExplorer](https://github.com/sinai-dev/UnityExplorer)
- [MetroFramework](https://github.com/thielj/MetroFramework)
