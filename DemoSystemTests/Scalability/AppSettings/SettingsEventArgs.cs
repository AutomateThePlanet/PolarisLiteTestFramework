namespace DemoSystemTests;

// Event args class to pass relevant data with events
public class SettingsEventArgs : EventArgs
{
    public Settings Setting { get; }
    public object Value { get; }

    public SettingsEventArgs(Settings setting, object value)
    {
        Setting = setting;
        Value = value;
    }
}

