using System.Globalization;

namespace PolarisLite.Mobile.Services;

public partial class DriverAdapter : IDeviceService
{
    public string GetCurrentActivity()
    {
        return _androidDriver.CurrentActivity;
    }

    public DateTime GetDeviceTime()
    {
        var formatter = "yyyy-MM-dd HH:mm";
        var deviceTime = _androidDriver.DeviceTime;
        return DateTime.ParseExact(deviceTime, formatter, CultureInfo.InvariantCulture);
    }

    public ScreenOrientation GetScreenOrientation()
    {
        return _androidDriver.Orientation;
    }

    public void Rotate(ScreenOrientation newOrientation)
    {
        _androidDriver.Orientation = newOrientation;
    }

    public bool IsLocked()
    {
        return _androidDriver.IsLocked();
    }

    public void Lock()
    {
        _androidDriver.Lock();
    }

    public void Unlock()
    {
        _androidDriver.Unlock();
    }

    public void TurnOnLocationService()
    {
        _androidDriver.ToggleLocationServices();
    }

    public void OpenNotifications()
    {
        _androidDriver.OpenNotifications();
    }

    public void SetSetting(string setting, object value)
    {
        _androidDriver.SetSetting(setting, value);
    }
}
