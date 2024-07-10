using PolarisLite.Web.Contracts;

namespace PolarisLite.Web.Assertions;

public static partial class ValidateComponentExtensions
{
    public static void ValidateSearchIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentSearch, IComponent
    {
        Validate(() => control.GetSearch().Equals(value), $"The control's search should be '{value}' but was '{control.GetSearch()}'.", timeout, sleepInterval);
    }
}