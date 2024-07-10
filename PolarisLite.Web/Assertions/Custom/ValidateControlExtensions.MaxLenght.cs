using PolarisLite.Web.Contracts;

namespace PolarisLite.Web.Assertions;

public static partial class ValidateComponentExtensions
{
    public static void ValidateMaxLengthIsNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
        where T : IComponentMaxLength, IComponent
    {
        Validate(() => control.MaxLength == null, $"The control's maxlength should be null but was '{control.MaxLength}'.", timeout, sleepInterval);
    }

    public static void ValidateMaxLengthIs<T>(this T control, int value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentMaxLength, IComponent
    {
        Validate(() => control.MaxLength.Equals(value), $"The control's maxlength should be '{value}' but was '{control.MaxLength}'.", timeout, sleepInterval);
    }
}