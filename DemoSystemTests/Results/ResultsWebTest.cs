using Allure.EventHandlers;
using PolarisLite.Web.Core.NUnit;

namespace DemoSystemTests.Results;
public class ResultsWebTest : WebTest
{
    private static bool _arePluginsAlreadyInitialized = false;
    protected override void Configure()
    {
        base.Configure();

        if (!_arePluginsAlreadyInitialized)
        {
            AddAllureSteps();
            _arePluginsAlreadyInitialized = true; 
        }
    }

    public static void AddAllureSteps()
    {
        new AllureStepsTextFieldEventHandlers().SubscribeToAll();
        new AllureStepsDateEventHandlers().SubscribeToAll();
        new AllureStepsCheckboxEventHandlers().SubscribeToAll();
        new AllureStepsEmailEventHandlers().SubscribeToAll();
        new AllureStepsInputFileEventHandlers().SubscribeToAll();
        new AllureStepsSelectEventHandlers().SubscribeToAll();
        new AllureStepsValidateExtensionsService().SubscribeToAll();
    }
}
