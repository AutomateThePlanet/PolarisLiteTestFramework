using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace PolarisLite.Mobile;

public class NotExistWaitStrategy : WaitStrategy
{
    public NotExistWaitStrategy(int? timeoutInterval = null, int? sleepInterval = null)
        : base(timeoutInterval, sleepInterval)
    {
    }

    public override void WaitUntil<TBy>(TBy by)
    {
        WaitUntil(d => ElementNotExists(WrappedAndroidDriver, by), TimeoutInterval, SleepInterval);
    }

    private bool ElementNotExists<TBy>(AndroidDriver searchContext, TBy by)
        where TBy : FindStrategy
    {
        try
        {
            var element = by.FindElement(searchContext);
            return element == null;
        }
        catch (InvalidOperationException)
        {
            return true;
        }
        catch (TimeoutException)
        {
            return true;
        }
        catch (NoSuchElementException)
        {
            return true;
        }
    }
}
