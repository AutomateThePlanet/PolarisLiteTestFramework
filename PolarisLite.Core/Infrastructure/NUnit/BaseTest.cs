using NUnit.Framework;
using PolarisLite.Core.Plugins;
using System.Reflection;

namespace PolarisLite.Core.Infrastructure.NUnit;

[TestFixture]
public abstract class BaseTest
{
    private static readonly ThreadLocal<Exception> _thrownException = new ThreadLocal<Exception>();

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
        // Thread-safe exception capture
        AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
        {
            _thrownException.Value = eventArgs.Exception;
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
        var testOutcome = (TestOutcome)TestContext.Result.Outcome.Status;

        PluginExecutionEngine.OnBeforeTestCleanup(testOutcome, testMethod);
        TestCleanup();
        PluginExecutionEngine.OnAfterTestCleanup(testOutcome, testMethod, _thrownException.Value);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        var testClassType = GetType();
        PluginExecutionEngine.OnBeforeTestClassCleanup(testClassType);
        ClassCleanup();
        PluginExecutionEngine.OnAfterTestClassCleanup(testClassType);

        _thrownException.Dispose();
    }

    protected virtual void Configure()
    {
        // Custom configuration logic
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
        // This could potentially cause issues with overloaded methods, so you may want to improve the method retrieval logic.
        return GetType().GetMethod(TestContext.CurrentContext.Test.Name) ?? GetType().BaseType.GetMethod(TestContext.CurrentContext.Test.Name);
    }
}
