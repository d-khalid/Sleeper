using Avalonia;
using System;
using Sleeper.Models;

namespace Sleeper;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        // Output platform info for debugging
        Console.WriteLine($"Running on: {EnvironmentInfo.OS}");
        Console.WriteLine("Windowing System: " + EnvironmentInfo.LinuxWindowingSystem);
        Console.WriteLine("Desktop Environment: " + EnvironmentInfo.LinuxDesktopEnvironment);
        Console.WriteLine("Is systemd: " + EnvironmentInfo.IsSystemd);

     
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

    }

  

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
