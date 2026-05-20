using System;

using Microsoft.Win32;

using static Dalamud.api;

namespace MidiBard.IPC;

internal static class IPCManagerFactory
{
    /// <summary>
    /// Creates the appropriate IIPCManager for the current runtime environment.
    /// Returns <see cref="LinuxIPCManager"/> when running under Wine on Linux,
    /// and <see cref="WindowsIPCManager"/> on native Windows.
    /// </summary>
    internal static IIPCManager Create()
    {
        if (IsRunningUnderWine())
        {
            PluginLog.Debug("Wine detected – using LinuxIPCManager");
            return new LinuxIPCManager();
        }

        PluginLog.Debug("Native Windows detected – using WindowsIPCManager");
        return new WindowsIPCManager();
    }

    /// <summary>
    /// Detects Wine by checking for the HKLM\Software\Wine registry key,
    /// which Wine always populates and native Windows never does.
    /// </summary>
    private static bool IsRunningUnderWine()
    {
        // Wine reports OperatingSystem.IsWindows() == true, so we cannot use
        // OperatingSystem.IsLinux() here. The registry key is the reliable signal.
        if (!OperatingSystem.IsWindows())
        {
            // Running on native Linux with a non-Wine .NET host – unlikely in
            // this FFXIV/Dalamud context, but handle it gracefully.
            return true;
        }

        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(@"Software\Wine");
            return key is not null;
        }
        catch (Exception e)
        {
            PluginLog.Debug(e, "Wine detection via registry failed, assuming native Windows");
            return false;
        }
    }
}
