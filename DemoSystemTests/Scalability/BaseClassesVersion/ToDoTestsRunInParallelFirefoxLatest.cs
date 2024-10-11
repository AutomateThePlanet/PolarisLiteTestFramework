using PolarisLite.Web.Plugins;

namespace DemoSystemTests.Web.Scalability.BaseClassesVersion;

[TestFixture]
[LambdaTest(BrowserType.Firefox)]
public class ToDoTestsRunInParallelFirefoxLatest : BaseToDoTestsRunInParallel
{
}