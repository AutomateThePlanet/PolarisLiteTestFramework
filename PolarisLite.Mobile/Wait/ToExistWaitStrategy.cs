namespace PolarisLite.Mobile;

public class ToExistWaitStrategy : WaitStrategy
{
    public ToExistWaitStrategy(int? timeoutInterval = null, int? sleepInterval = null)
        : base(timeoutInterval, sleepInterval)
    {
    }

    public override void WaitUntil<TBy>(TBy by)
    {
        WaitUntil(d => ElementExists(WrappedAndroidDriver, by), TimeoutInterval, SleepInterval);
    }

    private bool ElementExists<TBy>(AndroidDriver searchContext, TBy by)
        where TBy : FindStrategy
    {
        try
        {
            var element = by.FindElement(searchContext);
            return element != null;
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
