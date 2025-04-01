
# 📦 Changelog

All notable changes to this project will be documented in this file.

---

## [Unreleased]

### Added
- Feature: CLI support for installing and uninstalling BepInEx
- GitHub Actions integration with auto-release
- Modular code structure (BepInExManager, GameInfoManager, UIManager, etc.)

### Changed
- Refactored `Main.cs` into a clean architecture
- Moved INI logic to a dedicated helper

### Fixed
- Resolved startup error with missing default enum values
- Improved translation and UI consistency

---

## [v1.0] - 2025-04-01

Initial release with full GUI, drag-and-drop support, Unity detection, and auto install of BepInEx and UnityExplorer.
