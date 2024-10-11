namespace DemoSystemTests;
public static class EventEnabledMockSettingsService
{
    private static readonly Dictionary<Settings, object> _defaultSettings;
    private static readonly Dictionary<Settings, object> _settings;

    // Thread-local event handlers for each event
    private static readonly ThreadLocal<EventHandler<SettingsEventArgs>> _settingRetrieved = new ThreadLocal<EventHandler<SettingsEventArgs>>();
    private static readonly ThreadLocal<EventHandler<SettingsEventArgs>> _settingUpdated = new ThreadLocal<EventHandler<SettingsEventArgs>>();
    private static readonly ThreadLocal<EventHandler<SettingsEventArgs>> _settingRestored = new ThreadLocal<EventHandler<SettingsEventArgs>>();

    static EventEnabledMockSettingsService()
    {
        // Initialize default settings
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

    // Thread-local events
    public static event EventHandler<SettingsEventArgs> SettingRetrieved
    {
        add
        {
            if (!_settingRetrieved.IsValueCreated)
            {
                _settingRetrieved.Value = value;
            }
            else
            {
                _settingRetrieved.Value += value;
            }
        }
        remove
        {
            if (_settingRetrieved.IsValueCreated)
            {
                _settingRetrieved.Value -= value;
            }
        }
    }

    public static event EventHandler<SettingsEventArgs> SettingUpdated
    {
        add
        {
            if (!_settingUpdated.IsValueCreated)
            {
                _settingUpdated.Value = value;
            }
            else
            {
                _settingUpdated.Value += value;
            }
        }
        remove
        {
            if (_settingUpdated.IsValueCreated)
            {
                _settingUpdated.Value -= value;
            }
        }
    }

    public static event EventHandler<SettingsEventArgs> SettingRestored
    {
        add
        {
            if (!_settingRestored.IsValueCreated)
            {
                _settingRestored.Value = value;
            }
            else
            {
                _settingRestored.Value += value;
            }
        }
        remove
        {
            if (_settingRestored.IsValueCreated)
            {
                _settingRestored.Value -= value;
            }
        }
    }

    // Static methods for accessing settings
    public static T GetSetting<T>(Settings key)
    {
        var value = (T)_settings[key];
        OnSettingRetrieved(key, value);  // Raise the event
        return value;
    }

    public static void SetSetting<T>(Settings key, T value)
    {
        _settings[key] = value;
        OnSettingUpdated(key, value);  // Raise the event
    }

    public static void RestoreDefault(Settings key)
    {
        _settings[key] = _defaultSettings[key];
        OnSettingRestored(key, _defaultSettings[key]);  // Raise the event
    }

    public static Dictionary<Settings, object> GetAllSettings()
    {
        return _settings;
    }

    // Static methods to raise events
    private static void OnSettingRetrieved(Settings key, object value)
    {
        _settingRetrieved.Value?.Invoke(null, new SettingsEventArgs(key, value));
    }

    private static void OnSettingUpdated(Settings key, object value)
    {
        _settingUpdated.Value?.Invoke(null, new SettingsEventArgs(key, value));
    }

    private static void OnSettingRestored(Settings key, object value)
    {
        _settingRestored.Value?.Invoke(null, new SettingsEventArgs(key, value));
    }
}