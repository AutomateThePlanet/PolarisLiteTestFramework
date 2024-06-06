namespace PolarisLite.API;
public class MeasuredResponse<TReturnType> : RestResponse
        where TReturnType : new()
{
    public MeasuredResponse(RestResponse<TReturnType> restResponse, TimeSpan executionTime)
    {
        Response = restResponse;
        ExecutionTime = executionTime;
    }

    public TimeSpan ExecutionTime { get; set; }
    public RestResponse<TReturnType> Response { get; set; }
}