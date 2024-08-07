using PolarisLite.Core.Plugins;
using System.Reflection;

namespace PolarisLite.Core;
public class Plugin
{
    public virtual void OnBeforeTestClassInitialize(Type type) { }
    public virtual void OnAfterTestClassInitialize(Type type) { }
    public virtual void OnBeforeTestInitialize(MethodInfo memberInfo) { }
    public virtual async Task OnBeforeTestInitializeAsync(MethodInfo memberInfo) { }
    public virtual void OnAfterTestInitialize(MethodInfo memberInfo) { }
    public virtual void OnBeforeTestCleanup(TestOutcome result, MethodInfo memberInfo) { }
    public virtual void OnAfterTestCleanup(TestOutcome result, MethodInfo memberInfo, Exception failedTestException) { }
    public virtual void OnBeforeTestClassCleanup(Type type) { }
    public virtual void OnAfterTestClassCleanup(Type type) { }
}
