using OpenQA.Selenium.Appium.Android;

namespace PolarisLite.Mobile;

public class ToBeClickableWaitStrategy : WaitStrategy
{
    public ToBeClickableWaitStrategy(int? timeoutInterval = null, int? sleepInterval = null)
        : base(timeoutInterval, sleepInterval)
    {
    }

    public override void WaitUntil<TBy>(TBy by)
    {
        WaitUntil(ElementIsClickable(WrappedAndroidDriver, by), TimeoutInterval, SleepInterval);
    }

    private Func<AndroidDriver, bool> ElementIsClickable<TBy>(AndroidDriver searchContext, TBy by)
        where TBy : FindStrategy
    {
        return driver =>
                {
                    var element = by.FindElement(searchContext);
                    element = element.Displayed ? element : null;
                    try
                    {
                        return element != null && element.Enabled;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return false;
                    }
                };
    }
}
