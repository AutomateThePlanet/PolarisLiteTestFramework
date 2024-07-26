namespace PolarisLite.Mobile;

public class ToHaveContentWaitStrategy : WaitStrategy
{
    public ToHaveContentWaitStrategy(int? timeoutInterval = null, int? sleepInterval = null)
        : base(timeoutInterval, sleepInterval)
    {
    }

    public override void WaitUntil<TBy>(TBy by)
    {
        WaitUntil(d => ElementHasContent(WrappedAndroidDriver, by), TimeoutInterval, SleepInterval);
    }

    private bool ElementHasContent<TBy>(AndroidDriver searchContext, TBy by)
        where TBy : FindStrategy
    {
        try
        {
            var element = by.FindElement(searchContext);
            return !string.IsNullOrEmpty(element.Text);
        }
        catch (NoSuchElementException)
        {
            return false;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }
}
