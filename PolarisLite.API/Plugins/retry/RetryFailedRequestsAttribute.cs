namespace PolarisLite.API;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class RetryFailedRequestsAttribute : Attribute
{
    public RetryFailedRequestsAttribute(
        int maxRetryAttempts = 1, int pauseBetweenFailures = 0, TimeUnit timeUnit = TimeUnit.Seconds)
        => RetryFailedRequestsInfo = new RetryFailedRequestsInfo
                                     {
                                         MaxRetryAttempts = maxRetryAttempts,
                                         PauseBetweenFailures = pauseBetweenFailures,
                                         TimeUnit = timeUnit,
                                     };

    public RetryFailedRequestsInfo RetryFailedRequestsInfo { get; }
}