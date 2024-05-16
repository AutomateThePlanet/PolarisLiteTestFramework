namespace PolarisLite;

public static class Wait
{
    static Wait()
    {
        To = new WaitStrategyFactory();
    }

    public static WaitStrategyFactory To { get; }
}
