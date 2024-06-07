namespace PolarisLite.Mobile;

public class Wait
{
    static Wait() => To = new WaitStrategyFactory();

    public static WaitStrategyFactory To { get; }
}
