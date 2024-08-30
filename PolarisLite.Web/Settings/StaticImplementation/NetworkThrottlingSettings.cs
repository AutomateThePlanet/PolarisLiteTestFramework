namespace PolarisLite.Web.Settings.StaticImplementation;
public class NetworkThrottlingSettings
{
    public double Offline { get; set; }
    public double Latency { get; set; }
    public double DownloadThroughput { get; set; }
    public double UploadThroughput { get; set; }
    public string ConnectionTypeName { get; set; }
    public Network.ConnectionType ConnectionType { get; set; }
}
