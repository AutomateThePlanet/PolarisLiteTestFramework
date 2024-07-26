namespace PolarisLite.Mobile;

public class AndroidDriverWait : DefaultWait<AndroidDriver>
{
    public AndroidDriverWait(AndroidDriver driver, IClock clock, TimeSpan timeout, TimeSpan sleepInterval)
        : base(driver, clock)
    {
        Timeout = timeout;
        PollingInterval = sleepInterval;
    }

    public AndroidDriverWait(AndroidDriver driver, IClock clock)
           : base(driver, clock)
    {
    }
}
