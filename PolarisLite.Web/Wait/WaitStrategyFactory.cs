namespace PolarisLite;

public class WaitStrategyFactory
{
    public ToExistWaitStrategy Exists(int? timeoutInterval = null, int? sleepInterval = null)
    {
        return new ToExistWaitStrategy(timeoutInterval, sleepInterval);
    }

    public ToBeVisibleWaitStrategy BeVisible(int? timeoutInterval = null, int? sleepInterval = null)
    {
        return new ToBeVisibleWaitStrategy(timeoutInterval, sleepInterval);
    }

    public ToBeClickableWaitStrategy BeClickable(int? timeoutInterval = null, int? sleepInterval = null)
    {
        return new ToBeClickableWaitStrategy(timeoutInterval, sleepInterval);
    }
}
