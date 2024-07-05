using PolarisLite.Core;
using PolarisLite.Locators;
using PolarisLite.Web;

namespace PolarisLite;

public class ToValueContainingWaitStrategy : WaitStrategy
{
    private string _value;
    public ToValueContainingWaitStrategy(string value, int? timeoutIntervalInSeconds = null, int? sleepIntervalInSeconds = null)
        : base(timeoutIntervalInSeconds, sleepIntervalInSeconds)
    {
        _value = value;
        TimeoutInterval = TimeSpan.FromSeconds(60);
    }

    public override void WaitUntil<TBy>(TBy by)
    {
        WaitUntilInternal(d => ValueContaining(WrappedDriver, by));
    }

    private bool ValueContaining<TFindStrategy>(ISearchContext searchContext, TFindStrategy by)
         where TFindStrategy : FindStrategy
    {
        try
        {
            var element = FindElement(searchContext, by.Convert());
            return element.GetAttribute("value").Contains(_value);
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}
