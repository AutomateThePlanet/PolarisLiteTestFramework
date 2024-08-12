using System.ComponentModel;
using System.Reflection;

namespace PolarisLite;
public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        // Get the field that corresponds to the enum value
        FieldInfo field = value.GetType().GetField(value.ToString());

        // Get the DescriptionAttribute, if it exists
        DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();

        // Return the description or the value itself if no description is found
        return attribute == null ? value.ToString() : attribute.Description;
    }
}
