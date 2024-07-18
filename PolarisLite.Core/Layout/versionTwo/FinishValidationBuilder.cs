using NUnit.Framework;

namespace PolarisLite.Core.Layout.Second;

public class FinishValidationBuilder
{
    private string notificationMessage;
    private string failedAssertionMessage;

    public FinishValidationBuilder(Func<bool> comparingFunction, Func<string> notificationMessageFunction, Func<string> failedAssertionMessageFunction)
    {
        ComparingFunction = comparingFunction;
        notificationMessage = notificationMessageFunction();
        failedAssertionMessage = failedAssertionMessageFunction();
    }

    public FinishValidationBuilder(Func<bool> comparingFunction)
    {
        ComparingFunction = comparingFunction;
    }

    public Func<bool> ComparingFunction { get; set; }

    public void Validate()
    {
        Console.WriteLine(notificationMessage);
        Assert.IsTrue(ComparingFunction.Invoke(), failedAssertionMessage);
    }
}
