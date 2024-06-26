using System.Diagnostics;
using System.Reflection;

namespace Xpenses.API.Models;

public class ServerInfoStatus
{
    public string UpTime { get; set; }
    public string Message { get; set; } = "Ok";
    public string CPUusage { get; set; } = string.Empty;
    public string MemoryUsage { get; set; } = string.Empty;
    public DateTime DateTime { get; set; } = DateTime.Now;
    public string ApplicationVersion { get; set; } = "1.0.0";

    public ServerInfoStatus()
    {
        UpTime = GetUpTime();
        CPUusage = GetCPUUsage();
        MemoryUsage = GetMemoryUsage();
        ApplicationVersion = GetApplicationVersion();
    }
    
    private static string GetUpTime()
    {
        var uptime = TimeSpan.FromMilliseconds(Environment.TickCount64);
        return uptime.ToString(@"dd\.hh\:mm\:ss");
    }

    private static string GetCPUUsage()
    {
        var cpuUsage = string.Empty;

        try
        {
            using (var process = Process.GetCurrentProcess())
            {
                // Get total CPU time used by the process
                var totalCpuTime = process.TotalProcessorTime.TotalMilliseconds;
                var cpuCount = Environment.ProcessorCount;
                var upTime = Environment.TickCount64;

                // Calculate CPU usage percentage
                var cpuUsagePercentage = (totalCpuTime / (upTime * cpuCount)) * 100;
                cpuUsage = $"{cpuUsagePercentage:F2}%";
            }
        }
        catch (Exception ex)
        {
            cpuUsage = $"Error: {ex.Message}";
        }

        return cpuUsage;
    }

    private static string GetMemoryUsage()
    {
        var memoryUsage = string.Empty;

        try
        {
            using (var process = Process.GetCurrentProcess())
            {
                var memoryUsageBytes = process.WorkingSet64;
                var memoryUsageMB = memoryUsageBytes / (1024 * 1024);
                memoryUsage = $"{memoryUsageMB} MB";
            }
        }
        catch (Exception ex)
        {
            memoryUsage = $"Error: {ex.Message}";
        }

        return memoryUsage;
    }

    private static string GetApplicationVersion()
    {
        return Assembly.GetExecutingAssembly().GetName().Version!.ToString();
    }
    
}