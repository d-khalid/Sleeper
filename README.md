# Sleeper
A cross-platform tray-icon app that can be used to put your computer to sleep or turn off its display. The app currently supports Windows 10/11 and Linux (but only KDE Plasma + Wayland).<br/><br/>
This app does NOT require the .NET runtime to be installed on your machine as it uses Native AOT compilation, which produces native binaries for each platform.<br/>
## Showcase
### On Windows 10/11:
<img width="431" height="218" alt="image" src="https://github.com/user-attachments/assets/c21c4abb-c34e-428a-9aee-a8d8832daa86" /> <br/>
### On KDE Plasma (Fedora 42):
<img width="456" height="208" alt="Screenshot_20250714_071439" src="https://github.com/user-attachments/assets/23d72a3a-7177-48a9-8900-52ea06dbae78" />

## Build Instructions
1. Install .NET SDK 9.0 from [here](https://dotnet.microsoft.com/en-us/download/dotnet/9.0).
2. `cd` into the project's root directory in a terminal.
3. Execute `dotnet build` to compile the app.
4. Execute `dotnet run` to run the app.

## Credits
Nir Sofer's nircmd utility for Windows Operating Systems. It is used to implement sleep and monitor off functionality on Windows. Check out his website from [here](https://www.nirsoft.net/#google_vignette)
