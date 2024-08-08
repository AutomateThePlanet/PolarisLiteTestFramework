using PolarisLite.Logging;

namespace PolarisLite.API;
public class BDDLoggingAssertExtensions : AssertExtensionsEventHandlers
{
    protected override void AssertContentContainsEventHandler(object sender, ApiAssertEventArgs arg)
    {
        Logger.LogInfo($"Assert response content contains {arg.ActionValue}.");
    }

    protected override void AssertContentEncodingEventHandler(object sender, ApiAssertEventArgs arg)
    {
        Logger.LogInfo($"Assert response Cache-Info header is equal to {arg.ActionValue}.");
    }

    protected override void AssertContentEqualsEventHandler(object sender, ApiAssertEventArgs arg)
    {
        Logger.LogInfo($"Assert response content is equal to {arg.ActionValue}.");
    }

    protected override void AssertContentNotContainsEventHandler(object sender, ApiAssertEventArgs arg)
    {
        Logger.LogInfo($"Assert response content does not contain {arg.ActionValue}.");
    }

    protected override void AssertContentNotEqualsEventHandler(object sender, ApiAssertEventArgs arg)
    {
        Logger.LogInfo($"Assert response content is not equal to {arg.ActionValue}.");
    }

    protected override void AssertContentTypeEventHandler(object sender, ApiAssertEventArgs arg)
    {
        Logger.LogInfo($"Assert response Content-Type is equal to {arg.ActionValue}.");
    }

    protected override void AssertCookieEventHandler(object sender, ApiAssertEventArgs arg)
    {
        Logger.LogInfo($"Assert response cookie is equal to {arg.ActionValue}.");
    }

    protected override void AssertCookieExistsEventHandler(object sender, ApiAssertEventArgs arg)
    {
        Logger.LogInfo($"Assert response cookie {arg.ActionValue} exists.");
    }

    protected override void AssertExecutionTimeUnderEventHandler(object sender, ApiAssertEventArgs arg)
    {
        Logger.LogInfo($"Assert response execution time is under {arg.ActionValue}.");
    }

    protected override void AssertResponseHeaderEventHandler(object sender, ApiAssertEventArgs arg)
    {
        Logger.LogInfo($"Assert response header is equal to {arg.ActionValue}.");
    }

    protected override void AssertResultEqualsEventHandler(object sender, ApiAssertEventArgs arg)
    {
        Logger.LogInfo($"Assert response content is equal to {arg.ActionValue}.");
    }

    protected override void AssertResultNotEqualsEventHandler(object sender, ApiAssertEventArgs arg)
    {
        Logger.LogInfo($"Assert response content is not equal to {arg.ActionValue}.");
    }

    protected override void AssertStatusCodeEventHandler(object sender, ApiAssertEventArgs arg)
    {
        Logger.LogInfo($"Assert response status code is equal to {arg.ActionValue}.");
    }

    protected override void AssertSuccessStatusCodeEventHandler(object sender, ApiAssertEventArgs arg)
    {
        Logger.LogInfo($"Assert response status code is successfull.");
    }

    protected override void AssertSchemaEventHandler(object sender, ApiAssertEventArgs arg)
    {
        Logger.LogInfo($"Assert response is compatible to specified schema.");
    }
}