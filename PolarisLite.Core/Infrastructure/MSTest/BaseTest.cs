using Microsoft.VisualStudio.TestTools.UnitTesting;
using PolarisLite.Core.Plugins;
using System.Reflection;

namespace PolarisLite.Core.Infrastructure.MSTest;

[TestClass]
public abstract class BaseTest
{
    // private static Exception _thrownException;
    private static readonly ThreadLocal<Exception> _thrownException = new ThreadLocal<Exception>();

    // Thread-safe initialization tracking with lock to prevent race conditions
    // Access to InitializedTestClasses is synchronized using a lock. This ensures that test classes are initialized only once, even when multiple tests run in parallel.
    private static readonly HashSet<string> InitializedTestClasses = new HashSet<string>();
    private static readonly object _lockObject = new object();

    public TestContext TestContext { get; set; }

    [TestInitialize]
    public void CoreTestInitialize()
    {
        // Thread-safe exception capture
        AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
        {
            _thrownException.Value = eventArgs.Exception;
        };

        Configure();

        var testClassType = GetCurrentTestClassType();
        var testMethod = GetCurrentTestMethod();

        // Synchronize access to ensure that test classes are initialized only once in a thread-safe manner
        lock (_lockObject)
        {
            if (!InitializedTestClasses.Contains(TestContext.FullyQualifiedTestClassName))
            {
                PluginExecutionEngine.OnBeforeTestClassInitialize(testClassType);
                PerformClassInitialize();
                PluginExecutionEngine.OnAfterTestClassInitialize(testClassType);
                InitializedTestClasses.Add(TestContext.FullyQualifiedTestClassName);
            }
        }

        PluginExecutionEngine.OnBeforeTestInitialize(testMethod);
        PerformTestInitialize();
        PluginExecutionEngine.OnAfterTestInitialize(testMethod);
    }

    [TestCleanup]
    public void CoreTestCleanup()
    {
        var testClassType = GetCurrentTestClassType();
        var testMethod = GetCurrentTestMethod();

        PluginExecutionEngine.OnBeforeTestCleanup((TestOutcome)TestContext.CurrentTestOutcome, testMethod);
        PerformTestCleanup();
        PluginExecutionEngine.OnAfterTestCleanup((TestOutcome)TestContext.CurrentTestOutcome, testMethod, _thrownException.Value);
    }

    protected virtual void Configure()
    {
        // Custom configuration logic
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

    private Type GetCurrentTestClassType()
    {
        string className = TestContext.FullyQualifiedTestClassName;
        return Type.GetType(className);
    }

    private MethodInfo GetCurrentTestMethod()
    {
        return GetType().GetMethod(TestContext.TestName);
    }
}

