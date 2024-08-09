namespace PolarisLite.API;
public class ApiAssertEventArgs
{
    public ApiAssertEventArgs(MeasuredResponse measuredResponse) => MeasuredResponse = measuredResponse;

    public ApiAssertEventArgs(MeasuredResponse measuredResponse, string actionValue)
        : this(measuredResponse) => ActionValue = actionValue;

    public MeasuredResponse MeasuredResponse { get; }
    public string ActionValue { get; }
}