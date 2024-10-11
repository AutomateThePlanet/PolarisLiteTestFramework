using PolarisLite.Web.Plugins;

namespace DemoSystemTests.Web.Scalability.BaseClassesVersion;

[TestFixture]
[LambdaTest(BrowserType.Firefox, browserVersion: 128)]
public class ToDoTestsRunInParallelFirefox128 : BaseToDoTestsRunInParallel
{
}