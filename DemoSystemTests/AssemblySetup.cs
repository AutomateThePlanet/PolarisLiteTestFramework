using PolarisLite.Api.Configuration;
using PolarisLite.Mobile.Settings.StaticImplementation;
using PolarisLite.Web.Configuration.StaticImplementation;

namespace DemoSystemTests;
[SetUpFixture]
public class AssemblySetup
{
    [OneTimeSetUp]
    public void GlobalSetup()
    {
        MobileConfigurationLoader.LoadConfiguration();
        WebConfigurationLoader.LoadConfiguration();
        ApiConfigurationLoader.LoadConfiguration();
    }

    //[OneTimeTearDown]
    //public void GlobalTeardown()
    //{
    //    // Code that runs once after all tests in the assembly
    //    Console.WriteLine("Global teardown for the test assembly.");
    //    // Example: Clean up resources, close database connection, etc.
    //}
}
