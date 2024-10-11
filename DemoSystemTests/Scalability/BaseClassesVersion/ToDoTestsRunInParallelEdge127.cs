using PolarisLite.Web.Plugins;

namespace DemoSystemTests.Web.Scalability.BaseClassesVersion;

[TestFixture]
[LambdaTest(BrowserType.Edge, browserVersion: 127)]
public class ToDoTestsRunInParallelEdge127 : BaseToDoTestsRunInParallel
{
}