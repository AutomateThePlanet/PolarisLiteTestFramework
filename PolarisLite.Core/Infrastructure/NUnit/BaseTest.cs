using NUnit.Framework;
using PolarisLite.Core.Plugins;
using System.Reflection;

namespace PolarisLite.Core.Infrastructure.NUnit;

[TestFixture]
public abstract class BaseTest
{
    private Exception _thrownException;

    public TestContext TestContext => TestContext.CurrentContext;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        Configure();
        var testClassType = GetType();
        PluginExecutionEngine.OnBeforeTestClassInitialize(testClassType);
        ClassInitialize();
        PluginExecutionEngine.OnAfterTestClassInitialize(testClassType);
    }

    [SetUp]
    public void CoreTestInitialize()
    {
        AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
        {
            _thrownException = eventArgs.Exception;
        };

        var testMethod = GetCurrentTestMethod();
        PluginExecutionEngine.OnBeforeTestInitialize(testMethod);
        TestInitialize();
        PluginExecutionEngine.OnAfterTestInitialize(testMethod);
    }

    [TearDown]
    public void CoreTestCleanup()
    {
        var testMethod = GetCurrentTestMethod();
        PluginExecutionEngine.OnBeforeTestCleanup((TestOutcome)TestContext.Result.Outcome.Status, testMethod);
        TestCleanup();
        PluginExecutionEngine.OnAfterTestCleanup((TestOutcome)TestContext.Result.Outcome.Status, testMethod, _thrownException);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        var testClassType = GetType();
        PluginExecutionEngine.OnBeforeTestClassCleanup(testClassType);
        ClassCleanup();
        PluginExecutionEngine.OnAfterTestClassCleanup(testClassType);
    }

    protected virtual void Configure()
    {
    }

    protected virtual void ClassInitialize()
    {
        // Custom class initialization logic
    }

    protected virtual void ClassCleanup()
    {
        // Custom class cleanup logic
    }

    protected virtual void TestInitialize()
    {
        // Custom test initialization logic
    }

    protected virtual void TestCleanup()
    {
        // Custom test cleanup logic
    }

    private MethodInfo GetCurrentTestMethod()
    {
        return GetType().GetMethod(TestContext.CurrentContext.Test.Name);
    }
}