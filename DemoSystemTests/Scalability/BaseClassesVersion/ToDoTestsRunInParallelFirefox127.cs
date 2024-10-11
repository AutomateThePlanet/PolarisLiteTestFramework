using PolarisLite.Web.Plugins;

namespace DemoSystemTests.Web.Scalability.BaseClassesVersion;

[TestFixture]
[LambdaTest(BrowserType.Firefox, browserVersion: 127)]
public class ToDoTestsRunInParallelFirefox127 : BaseToDoTestsRunInParallel
{
}