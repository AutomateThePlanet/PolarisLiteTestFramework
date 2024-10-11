using PolarisLite.Web.Plugins;

namespace DemoSystemTests.Web.Scalability.BaseClassesVersion;

[TestFixture]
[LambdaTest(BrowserType.Edge)]
public class ToDoTestsRunInParallelEdgeLatest : BaseToDoTestsRunInParallel
{
}