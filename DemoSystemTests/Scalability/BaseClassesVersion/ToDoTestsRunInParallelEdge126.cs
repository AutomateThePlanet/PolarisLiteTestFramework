using PolarisLite.Web.Plugins;

namespace DemoSystemTests.Web.Scalability.BaseClassesVersion;

[TestFixture]
[LambdaTest(BrowserType.Edge, browserVersion: 126)]
public class ToDoTestsRunInParallelEdge126 : BaseToDoTestsRunInParallel
{
}