namespace PolarisLite.Core.Layout.Second;

public static class StringValueEnumExtensions
{
    public static string GetStringValue(this Enum value)
    {
        var type = value.GetType();
        var fieldInfo = type.GetField(value.ToString());

        if (fieldInfo == null)
        {
            return string.Empty;
        }

        var attribs = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

        return attribs != null && attribs.Length > 0 ? attribs[0].StringValue : null;
    }

    public static string GetClearStringValue(this Enum value)
    {
        var type = value.GetType();
        var fieldInfo = type.GetField(value.ToString());

        if (fieldInfo == null)
        {
            throw new ArgumentException("The field value was not resolved.");
        }

        var clearStringValue = fieldInfo.Name.Replace("_", string.Empty);

        return clearStringValue;
    }
}
