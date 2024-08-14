using PolarisLite.Core;
using PolarisLite.Core.Plugins;
using PolarisLite.Integrations;
using PolarisLite.Integrations.Settings;
using PolarisLite.Web.Plugins;
using System.Reflection;

namespace DemoSystemTests.Authentication.Plugins;
public class ListenForSMSPlugin : Plugin
{
    private static SmsListener _smsListener;

    public override void OnBeforeTestInitialize(MethodInfo memberInfo) 
    {
        var shouldListen = ShouldListenForSMS(memberInfo.DeclaringType);
        if (shouldListen)
        {
            _smsListener = TwillioService.ListenForSms(IntegrationSettings.TwilioSettings.PhoneNumber);
        }
    }

    public override void OnBeforeTestCleanup(TestOutcome result, MethodInfo memberInfo) 
    {
        var shouldListen = ShouldListenForSMS(memberInfo.DeclaringType);
        if (shouldListen && _smsListener != null)
        {
            TwillioService.StopListeningForSms(_smsListener);
        }
    }

    private bool ShouldListenForSMS(Type testClass)
    {
        var gridAttribute = testClass.GetCustomAttribute<ListenForSMSAttribute>(true);
        return gridAttribute != null;
    }
}
