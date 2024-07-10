using PolarisLite.Web.Contracts;
using PolarisLite.Web.Core;

namespace PolarisLite.Web.Assertions;

public static partial class ValidateComponentExtensions
{
    public static void ValidatePhoneIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentPhone, IComponent
    {
        Validate(() => control.GetPhone().Equals(value), $"The control's phone should be '{value}' but was '{control.GetPhone()}'.", timeout, sleepInterval);
    }
}