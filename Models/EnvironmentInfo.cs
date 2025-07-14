using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Sleeper.Models;

public static class EnvironmentInfo
{
    // OS Properties
    public static OSPlatform OS { get; }
    public static bool IsWindows { get; }
    public static bool IsLinux { get; }
    public static bool IsMacOS { get; }

    public static bool IsSystemd { get; }

    // Windowing System (Linux-specific)
    public static WindowingSystem? LinuxWindowingSystem { get; } // "x11", "wayland", etc.

    // Desktop Environment (Linux-specific)
    public static DesktopEnvironment? LinuxDesktopEnvironment { get; } // "gnome", "kde", etc.

    static EnvironmentInfo()
    {
        // Detect OS
        OS = GetOSPlatform();
        IsWindows = OS == OSPlatform.Windows;
        IsLinux = OS == OSPlatform.Linux;
        IsMacOS = OS == OSPlatform.OSX;

        IsSystemd = CheckSystemd();

        // Detect Linux-specific info
        if (IsLinux)
        {
            LinuxWindowingSystem = GetLinuxWindowingSystem();
            LinuxDesktopEnvironment = GetLinuxDesktopEnvironment();
        }
    }

    private static OSPlatform GetOSPlatform()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return OSPlatform.Windows;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return OSPlatform.Linux;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return OSPlatform.OSX;
        throw new PlatformNotSupportedException("Unsupported OS.");
    }

    private static WindowingSystem? GetLinuxWindowingSystem()
    {
        // Check WAYLAND_DISPLAY or XDG_SESSION_TYPE
        string? waylandDisplay = Environment.GetEnvironmentVariable("WAYLAND_DISPLAY");
        string? xdgSessionType = Environment.GetEnvironmentVariable("XDG_SESSION_TYPE");

        if (!string.IsNullOrEmpty(waylandDisplay) || xdgSessionType?.Equals("wayland", StringComparison.OrdinalIgnoreCase) == true)
            return WindowingSystem.Wayland;
        else if (xdgSessionType?.Equals("x11", StringComparison.OrdinalIgnoreCase) == true)
            return WindowingSystem.X11;

        return null; // Unknown
    }
    
    public static bool CheckSystemd()
    {
        try
        {
        
            var psi = new ProcessStartInfo
            {
                FileName = "ls",
                Arguments = "-l /usr/lib | grep systemd",
                RedirectStandardOutput = true,
                CreateNoWindow = false
            };
            using var process = Process.Start(psi);
            // If there is an output, that means systemd is present
            return !string.IsNullOrEmpty(process?.StandardOutput.ReadToEnd());
        }
        catch(Exception e)
        {
            return false; 
        }
    }

    private static DesktopEnvironment? GetLinuxDesktopEnvironment()
    {
        // Check XDG_CURRENT_DESKTOP or GDMSESSION
        string? xdgCurrentDesktop = Environment.GetEnvironmentVariable("XDG_CURRENT_DESKTOP")?.ToLower();
        string? gdmSession = Environment.GetEnvironmentVariable("GDMSESSION")?.ToLower();

        // TODO: This probably needs to be expanded
        if (!string.IsNullOrEmpty(xdgCurrentDesktop))
        {
            if (xdgCurrentDesktop.Contains("gnome")) return DesktopEnvironment.Gnome;
            if (xdgCurrentDesktop.Contains("kde")) return DesktopEnvironment.KDE;
            if (xdgCurrentDesktop.Contains("xfce")) return DesktopEnvironment.XFCE;
            if (xdgCurrentDesktop.Contains("mate")) return DesktopEnvironment.Mate;
        }

        return null;
    }
}

public enum WindowingSystem
{
    X11,
    Wayland
}

public enum DesktopEnvironment
{
    Gnome,
    KDE,
    XFCE,
    Mate,
    Other
}
