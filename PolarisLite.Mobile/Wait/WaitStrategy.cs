using PolarisLite.Mobile.Plugins.AppExecution;

namespace PolarisLite.Mobile;

public abstract class WaitStrategy
{
    protected WaitStrategy(int? timeoutInterval = null, int? sleepInterval = null)
    {
        WrappedAndroidDriver = DriverFactory.WrappedAndroidDriver;
        TimeoutInterval = timeoutInterval ?? 30;
        SleepInterval = sleepInterval ?? 2;
    }

    protected AndroidDriver WrappedAndroidDriver { get; }

    protected int? TimeoutInterval { get; set; }

    protected int? SleepInterval { get; }

    public abstract void WaitUntil<TBy>(TBy by)
        where TBy : FindStrategy;

    protected void WaitUntil(Func<AndroidDriver, bool> waitCondition, int? timeout, int? sleepInterval)
    {
        if (timeout != null && sleepInterval != null)
        {
            var timeoutTimeSpan = TimeSpan.FromSeconds((int)timeout);
            var sleepIntervalTimeSpan = TimeSpan.FromSeconds((int)sleepInterval);
            var wait = new AndroidDriverWait(WrappedAndroidDriver, new SystemClock(), timeoutTimeSpan, sleepIntervalTimeSpan);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException), typeof(InvalidOperationException));
            wait.Until(waitCondition);
        }
    }
}
