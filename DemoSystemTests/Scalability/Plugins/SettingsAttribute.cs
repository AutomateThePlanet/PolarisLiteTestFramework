
namespace DemoSystemTests;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class SettingsAttribute : Attribute
{
    public SettingsAttribute(Settings settings, object value, bool setTags = true)
    {
        Settings = settings;
        Value = value;
        SetTags = setTags;
    }

    public  Settings Settings { get; set; }
    public object Value { get; set; }
    public bool SetTags { get; set; }
}
