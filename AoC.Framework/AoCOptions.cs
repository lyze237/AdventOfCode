using System.Runtime.InteropServices;

namespace AoC.Framework;

public class AoCOptions
{
    public string CacheDirectory { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
        ? Path.Combine(Environment.GetEnvironmentVariable("APPDATA") ?? throw new InvalidOperationException("%APPDATA% not found"), "AoC.Framework")
        : Path.Combine(Environment.GetEnvironmentVariable("XDG_CONFIG_HOME") ?? "~/.config/", "AoC.Framework");
}