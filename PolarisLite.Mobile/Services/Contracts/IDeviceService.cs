namespace PolarisLite.Mobile;

public interface IDeviceService
{
    string GetCurrentActivity();
    DateTime GetDeviceTime();
    ScreenOrientation GetScreenOrientation();
    void Rotate(ScreenOrientation newOrientation);
    bool IsLocked();
    void Lock();
    void Unlock();
    void TurnOnLocationService();
    void OpenNotifications();
    void SetSetting(string setting, object value);
}
