namespace DemoSystemTests;
public class MockSettingsService : ISettingsService
{
    private readonly Dictionary<Settings, object> _settings;
    private readonly Dictionary<Settings, object> _defaultSettings;

    public MockSettingsService()
    {
        // Initialize default settings with enums as keys
        _defaultSettings = new Dictionary<Settings, object>
        {
            { Settings.EnableDarkMode, false },
            { Settings.MaxItemsPerPage, 10 },
            { Settings.EnablePromotions, true },
            { Settings.Currency, CurrencyType.USD },
            { Settings.CountrySpecificFeatures, false },
            { Settings.EnableNewCheckoutFlow, false },
            { Settings.EnableBetaFeatures, false },
            { Settings.ItemsInCartLimit, 50 },
            { Settings.Enable2FA, true },
            { Settings.EnableAnalyticsTracking, true }
        };

        // Initialize runtime settings (can be changed during test execution)
        _settings = new Dictionary<Settings, object>(_defaultSettings);
    }

    public T GetSetting<T>(Settings key)
    {
        return (T)_settings[key];
    }

    public void SetSetting<T>(Settings key, T value)
    {
        _settings[key] = value;
    }

    public void RestoreDefault(Settings key)
    {
        _settings[key] = _defaultSettings[key];
    }

    public Dictionary<Settings, object> GetAllSettings()
    {
        return _settings;
    }
}
