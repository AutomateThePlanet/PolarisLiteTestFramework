using NUnit.Framework;

namespace PolarisLite.Core.Layout.Second;

public class LayoutPreciseValidationBuilder
{
    private double actualDistance;
    private string notificationMessage;
    private string failedAssertionMessage;

    public LayoutPreciseValidationBuilder(Func<double> calculateActualDistanceFunction, Func<string> notificationMessageFunction, Func<string> failedAssertionMessageFunction)
    {
        if (calculateActualDistanceFunction != null)
        {
            actualDistance = calculateActualDistanceFunction();
        }

        notificationMessage = notificationMessageFunction();
        failedAssertionMessage = failedAssertionMessageFunction();
    }

    public LayoutPreciseValidationBuilder(double actualDistance)
    {
        this.actualDistance = actualDistance;
    }

    public FinishValidationBuilder Equal(int expected)
    {
        return new FinishValidationBuilder(() => actualDistance == expected,
                () => BuildNotificationValidationMessage(ComparingOperators.Equal, expected),
                () => BuildFailedValidationMessage(ComparingOperators.Equal, expected));
    }

    public FinishValidationBuilder LessThan(int expected)
    {
        return new FinishValidationBuilder(() => actualDistance < expected,
                () => BuildNotificationValidationMessage(ComparingOperators.LessThan, expected),
                () => BuildFailedValidationMessage(ComparingOperators.LessThan, expected));
    }

    public FinishValidationBuilder LessThanOrEqual(int expected)
    {
        return new FinishValidationBuilder(() => actualDistance <= expected,
                () => BuildNotificationValidationMessage(ComparingOperators.LessThanEqual, expected),
                () => BuildFailedValidationMessage(ComparingOperators.LessThanEqual, expected));
    }

    public FinishValidationBuilder GreaterThan(int expected)
    {
        return new FinishValidationBuilder(() => actualDistance > expected,
                () => BuildNotificationValidationMessage(ComparingOperators.GreaterThan, expected),
                () => BuildFailedValidationMessage(ComparingOperators.GreaterThan, expected));
    }

    public FinishValidationBuilder GreaterThanOrEqual(int expected)
    {
        return new FinishValidationBuilder(() => actualDistance >= expected,
                () => BuildNotificationValidationMessage(ComparingOperators.GreaterThanEqual, expected),
                () => BuildFailedValidationMessage(ComparingOperators.GreaterThanEqual, expected));
    }

    private string BuildNotificationValidationMessage(ComparingOperators comparingMessage, int expected)
    {
        return $"{notificationMessage}{comparingMessage.GetStringValue()} {expected} px";
    }

    private string BuildFailedValidationMessage(ComparingOperators comparingMessage, int expected)
    {
        return $"{failedAssertionMessage}{comparingMessage.GetStringValue()} {expected} px";
    }

    public void Validate()
    {
        Assert.That(actualDistance > 0, Is.True, failedAssertionMessage);
    }
}
