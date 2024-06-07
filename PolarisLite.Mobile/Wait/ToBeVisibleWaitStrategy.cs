using OpenQA.Selenium.Appium.Android;

namespace PolarisLite.Mobile;

public class ToBeVisibleWaitStrategy : WaitStrategy
{
    public ToBeVisibleWaitStrategy(int? timeoutInterval = null, int? sleepInterval = null)
        : base(timeoutInterval, sleepInterval)
    {
    }

    public override void WaitUntil<TBy>(TBy by)
    {
        WaitUntil(d => ElementIsVisible(WrappedAndroidDriver, by), TimeoutInterval, SleepInterval);
    }

    private bool ElementIsVisible<TBy>(AndroidDriver searchContext, TBy by)
         where TBy : FindStrategy
    {
        try
        {
            var element = by.FindElement(searchContext);
            return element.Displayed;
        }
        catch (StaleElementReferenceException)
        {
            return false;
        }
    }
}
