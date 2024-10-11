using PolarisLite.Web.Plugins;


namespace DemoSystemTests.Web.Scalability.BaseClassesVersion;

[TestFixture]
[LambdaTest(BrowserType.Chrome, browserVersion: 127)]
[Category("Chrome_127")]
public class ToDoTestsRunInParallelChrome127 : BaseToDoTestsRunInParallel
{
}