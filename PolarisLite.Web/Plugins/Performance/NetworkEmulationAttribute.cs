using OpenQA.Selenium.DevTools.V127.Network;

namespace PolarisLite.Web.Plugins;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class NetworkEmulationAttribute : Attribute
{
    public NetworkEmulationAttribute(double offline, double latency, double downloadThroughput, double uploadThroughput, string connectionType)
    {
        Offline = offline;
        Latency = latency;
        DownloadThroughput = downloadThroughput;
        UploadThroughput = uploadThroughput;
        ConnectionTypeName = connectionType;
    }

    public NetworkEmulationAttribute(double offline, double latency, double downloadThroughput, double uploadThroughput, ConnectionType connectionType)
    {
        Offline = offline;
        Latency = latency;
        DownloadThroughput = downloadThroughput;
        UploadThroughput = uploadThroughput;
        ConnectionType = connectionType;
    }

    public double Offline { get; set; }
    public double Latency { get; set; }
    public double DownloadThroughput { get; set; }
    public double UploadThroughput { get; set; }
    public string ConnectionTypeName { get; set; }
    public ConnectionType ConnectionType { get; set; }
}