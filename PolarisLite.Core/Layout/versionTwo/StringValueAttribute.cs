namespace PolarisLite.Core.Layout.Second;

public class StringValueAttribute : Attribute
{
    public StringValueAttribute(string value) => StringValue = value;

    public string StringValue { get; protected set; }
}
