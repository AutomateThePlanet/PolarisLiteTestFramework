namespace PolarisLite.Mobile.Components;

public sealed class Find
{
    static Find() => By = new FindStrategyFactory();

    public static FindStrategyFactory By { get; }
}
