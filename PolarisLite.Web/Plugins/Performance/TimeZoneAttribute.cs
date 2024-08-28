namespace PolarisLite.Web.Plugins;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class TimeZoneAttribute : Attribute
{
    public TimeZoneAttribute(string timeZoneName = "", string timeZoneId = "")
    {
        TimeZoneName = timeZoneName;
        TimeZoneId = timeZoneId;
    }


    public string TimeZoneName { get; set; }
    public string TimeZoneId { get; set; }
}