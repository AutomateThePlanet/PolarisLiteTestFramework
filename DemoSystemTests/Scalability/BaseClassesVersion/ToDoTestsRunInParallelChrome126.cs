using PolarisLite.Web.Plugins;


namespace DemoSystemTests.Web.Scalability.BaseClassesVersion;

[TestFixture]
[LambdaTest(BrowserType.Chrome, browserVersion: 126)]
[Category("Chrome_126")]
public class ToDoTestsRunInParallelChrome126 : BaseToDoTestsRunInParallel
{
}