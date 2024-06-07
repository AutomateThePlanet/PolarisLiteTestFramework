namespace PolarisLite.Mobile;

public interface IKeyboardService
{
    void HideKeyboard();
    void LongPressKey(int keyCode);
    void PressKey(int keyCode);
}
