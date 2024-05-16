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
        var testClassType = GetType();
        PluginExecutionEngine.OnBeforeTestClassInitialize(testClassType);
        PerformClassInitialize();
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
        PerformTestInitialize();
        PluginExecutionEngine.OnAfterTestInitialize(testMethod);
    }

    [TearDown]
    public void CoreTestCleanup()
    {
        var testMethod = GetCurrentTestMethod();
        PluginExecutionEngine.OnBeforeTestCleanup((TestOutcome)TestContext.Result.Outcome.Status, testMethod);
        PerformTestCleanup();
        PluginExecutionEngine.OnAfterTestCleanup((TestOutcome)TestContext.Result.Outcome.Status, testMethod, _thrownException);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        var testClassType = GetType();
        PluginExecutionEngine.OnBeforeTestClassCleanup(testClassType);
        PerformClassCleanup();
        PluginExecutionEngine.OnAfterTestClassCleanup(testClassType);
    }

    protected virtual void PerformClassInitialize()
    {
        // Custom class initialization logic
    }

    protected virtual void PerformClassCleanup()
    {
        // Custom class cleanup logic
    }

    protected virtual void PerformTestInitialize()
    {
        // Custom test initialization logic
    }

    protected virtual void PerformTestCleanup()
    {
        // Custom test cleanup logic
    }

    private MethodInfo GetCurrentTestMethod()
    {
        return GetType().GetMethod(TestContext.CurrentContext.Test.Name);
    }
}