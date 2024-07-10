using PolarisLite.Web.Contracts;

namespace PolarisLite.Web.Assertions;

public static partial class ValidateComponentExtensions
{
    public static void ValidateSizeIsNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
        where T : IComponentSize, IComponent
    {
        Validate(() => control.Size == null, $"The control's size should be null but was '{control.Size}'.", timeout, sleepInterval);
    }

    public static void ValidateSizeIs<T>(this T control, int value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentSize, IComponent
    {
        Validate(() => control.Size.Equals(value), $"The control's size should be '{value}' but was '{control.Size}'.", timeout, sleepInterval);
    }
}