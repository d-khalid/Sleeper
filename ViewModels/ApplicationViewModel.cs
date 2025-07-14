using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Dialogs;
using CommunityToolkit.Mvvm.Input;
using Sleeper.Models;
using Sleeper.Views;

namespace Sleeper.ViewModels;

/*
 *  For now, this supports only Windows and Linux with KDE+Wayland
 */
public partial class ApplicationViewModel : ViewModelBase
{
    // Wait 0.5s before running a command
    public static int actionDelayMs = 500;

    [RelayCommand]
    public void MonitorOff()
    {
        try
        {
            if (EnvironmentInfo.OS == OSPlatform.Windows)
            {
                Thread.Sleep(actionDelayMs);
                Process.Start("scrnsaver.scr", "/s");
            }
            else if (EnvironmentInfo.OS == OSPlatform.Linux &&
                     EnvironmentInfo.LinuxDesktopEnvironment == DesktopEnvironment.KDE &&
                     EnvironmentInfo.LinuxWindowingSystem == WindowingSystem.Wayland)
            {
                Thread.Sleep(actionDelayMs);
                Process.Start("kscreen-doctor", "--dpms off");

            }
            else throw new PlatformNotSupportedException("This platform is not supported.\n\n" +
                                                         "Current Supported Platforms are: Windows and Linux (KDE+Wayland)");

        }
        catch (Exception e)
        {
            MessageBox mb = new MessageBox(e.Message);
            mb.Show();
        }


    }

    [RelayCommand]
    public void Sleep()
    {
        try
        {
            if (EnvironmentInfo.OS == OSPlatform.Windows)
            {
                Thread.Sleep(actionDelayMs);
                Process.Start("rundll32.exe", "powrprof.dll,SetSuspendState 0,1,0");
            }
            else if (EnvironmentInfo.OS == OSPlatform.Linux && 
                     EnvironmentInfo.LinuxDesktopEnvironment == DesktopEnvironment.KDE &&
                     EnvironmentInfo.LinuxWindowingSystem == WindowingSystem.Wayland && 
                     EnvironmentInfo.IsSystemd)
            {
                Thread.Sleep(actionDelayMs);
                Process.Start("systemctl", "suspend");

            }
            else throw new PlatformNotSupportedException("This platform is not supported.\n\n" +
                                                         "Current Supported Platforms are: Windows and Linux (KDE+Wayland)");

            // if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            // {
            //    
            // }
            // else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            // {
            //     Process.Start("systemctl", "suspend"); // Requires systemd
            // }
            // else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            // {
            //     // TODO: TEST THIS SOMEHOW
            //     Process.Start("pmset", "sleepnow"); // macOS
            // }
            // else
            // {
            //     throw new PlatformNotSupportedException("Sleep not supported on this OS.");
            // }
        }
        catch (Exception e)
        {
            MessageBox mb = new MessageBox(e.Message);
            mb.Show();
        }
    }

    [RelayCommand]
    public void Exit()
    {
        Environment.Exit(0);
    }
}