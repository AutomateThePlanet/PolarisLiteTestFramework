using Microsoft.VisualStudio.TestTools.UnitTesting;
using PolarisLite.Core.Plugins;
using System.Reflection;

namespace PolarisLite.Core.Infrastructure.MSTest;

[TestClass]
public abstract class BaseTest
{
    private static readonly HashSet<string> InitializedTestClasses = new HashSet<string>();
    private static Exception _thrownException;

    public TestContext TestContext { get; set; }

    [TestInitialize]
    public void CoreTestInitialize()
    {
        AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
        {
            _thrownException = eventArgs.Exception;
        };

        Configure();
        var testClassType = GetCurrentTestClassType();
        var testMethod = GetCurrentTestMethod();
        if (!InitializedTestClasses.Contains(TestContext.FullyQualifiedTestClassName))
        {
            PluginExecutionEngine.OnBeforeTestClassInitialize(testClassType);
            PerformClassInitialize();
            PluginExecutionEngine.OnAfterTestClassInitialize(testClassType);
            InitializedTestClasses.Add(TestContext.FullyQualifiedTestClassName);
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
        PerformTestInitialize();
        PluginExecutionEngine.OnAfterTestCleanup((TestOutcome)TestContext.CurrentTestOutcome, testMethod, _thrownException);
    }

    protected virtual void Configure()
    {
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
