using PolarisLite.Web.Contracts;

namespace PolarisLite.Web.Assertions;

public static partial class ValidateComponentExtensions
{
    public static void ValidateMinLengthIsNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
        where T : IComponentMinLength, IComponent
    {
        Validate(() => control.MinLength == null, $"The control's minlength should be null but was '{control.MinLength}'.", timeout, sleepInterval);
    }

    public static void ValidateMinLengthIs<T>(this T control, int value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentMinLength, IComponent
    {
        Validate(() => control.MinLength.Equals(value), $"The control's minlength should be '{value}' but was '{control.MinLength}'.", timeout, sleepInterval);
    }
}