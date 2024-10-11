namespace DemoSystemTests;
public interface ISettingsService
{
    T GetSetting<T>(Settings key);
    void SetSetting<T>(Settings key, T value);
    void RestoreDefault(Settings key);
    Dictionary<Settings, object> GetAllSettings();
}
