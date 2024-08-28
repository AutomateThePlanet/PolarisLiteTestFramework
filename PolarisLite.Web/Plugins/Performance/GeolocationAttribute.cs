namespace PolarisLite.Web.Plugins;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class GeolocationAttribute : Attribute
{
    public GeolocationAttribute(string location)
    {
        Location = location;
    }

    public GeolocationAttribute(double? latitude, double? longitude, int accuracy)
    {
        Latitude = latitude;
        Longitude = longitude;
        Accuracy = accuracy;
    }

    public string Location { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public int Accuracy { get; set; } = 1;
}