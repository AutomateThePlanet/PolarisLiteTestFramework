namespace PolarisLite.Web.Plugins;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class LocaleAttribute : Attribute
{
    public LocaleAttribute(string locale)
    {
        Locale = locale;
    }

    public string Locale { get; set; }
}