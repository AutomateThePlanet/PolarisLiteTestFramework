namespace PolarisLite.Mobile;

public class WaitStrategyFactory
{
    public ToExistWaitStrategy Exists(int? timeoutInterval = null, int? sleepinterval = null) => new ToExistWaitStrategy(timeoutInterval, sleepinterval);

    public NotExistWaitStrategy NotExists(int? timeoutInterval = null, int? sleepinterval = null) => new NotExistWaitStrategy(timeoutInterval, sleepinterval);

    public ToBeVisibleWaitStrategy BeVisible(int? timeoutInterval = null, int? sleepinterval = null) => new ToBeVisibleWaitStrategy(timeoutInterval, sleepinterval);

    public NotBeVisibleWaitStrategy BeNotVisible(int? timeoutInterval = null, int? sleepinterval = null) => new NotBeVisibleWaitStrategy(timeoutInterval, sleepinterval);

    public ToBeClickableWaitStrategy BeClickable(int? timeoutInterval = null, int? sleepinterval = null) => new ToBeClickableWaitStrategy(timeoutInterval, sleepinterval);

    public ToHaveContentWaitStrategy HasContent(int? timeoutInterval = null, int? sleepinterval = null) => new ToHaveContentWaitStrategy(timeoutInterval, sleepinterval);
}
