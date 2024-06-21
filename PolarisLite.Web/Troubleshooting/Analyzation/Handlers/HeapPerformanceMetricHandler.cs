namespace PolarisLite.Web.Troubshoting;

public class HeapPerformanceMetricHandler : PerformanceMetricHandler
{
    public HeapPerformanceMetricHandler()
        : base("JSHeapTotalSize", 1000)
    {
    }
}
