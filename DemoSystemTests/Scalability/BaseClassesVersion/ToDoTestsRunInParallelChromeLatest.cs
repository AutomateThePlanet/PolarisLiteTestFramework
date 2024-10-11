using PolarisLite.Web.Plugins;


namespace DemoSystemTests.Web.Scalability.BaseClassesVersion;

[TestFixture]
[LambdaTest(BrowserType.Chrome)]
public class ToDoTestsRunInParallelChromeLatest : BaseToDoTestsRunInParallel
{
}