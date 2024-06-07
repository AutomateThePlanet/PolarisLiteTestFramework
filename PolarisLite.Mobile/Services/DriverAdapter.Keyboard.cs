namespace PolarisLite.Mobile.Services;

public partial class DriverAdapter : IKeyboardService
{
    public void HideKeyboard()
    {
        try
        {
            _androidDriver.HideKeyboard();
        }
        catch (WebDriverException) { }
    }

    public void LongPressKey(int keyCode)
    {
        _androidDriver.LongPressKeyCode(keyCode);
    }

    public void PressKey(int keyCode)
    {
        _androidDriver.PressKeyCode(keyCode);
    }
}
