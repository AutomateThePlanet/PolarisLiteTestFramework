using OpenQA.Selenium.Appium.Android;

namespace PolarisLite.Mobile;

public class NotBeVisibleWaitStrategy : WaitStrategy
{
    public NotBeVisibleWaitStrategy(int? timeoutInterval = null, int? sleepInterval = null)
        : base(timeoutInterval, sleepInterval)
    {
    }

    public override void WaitUntil<TBy>(TBy by)
    {
        WaitUntil(d => ElementIsInvisible(WrappedAndroidDriver, by), TimeoutInterval, SleepInterval);
    }

    private bool ElementIsInvisible<TBy>(AndroidDriver searchContext, TBy by)
        where TBy : FindStrategy
    {
        try
        {
            var element = by.FindElement(searchContext);
            return !element.Displayed;
        }
        catch (NoSuchElementException)
        {
            return true;
        }
        catch (InvalidOperationException)
        {
            return true;
        }
        catch (StaleElementReferenceException)
        {
            return true;
        }
    }
}
