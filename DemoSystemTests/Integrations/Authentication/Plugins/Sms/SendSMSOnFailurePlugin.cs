using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Integrations;
using PolarisLite.Integrations.Settings;
using System.Reflection;

namespace DemoSystemTests.Integrations.Authentication.Plugins.Sms;
public class SendSMSOnFailurePlugin : Plugin
{
    public override void OnBeforeTestCleanup(TestOutcome result, MethodInfo memberInfo)
    {
        if (!IntegrationSettings.TwilioSettings.SendSmsOnFailure || result == TestOutcome.Error)
        {
            // Add to settings phone numbers to send in case of emergency
            TwillioService.SendSms("+00189223413112", "Some of the heartbeat tests failed on PROD. Call the SWAT Team!");
        }
    }
}
