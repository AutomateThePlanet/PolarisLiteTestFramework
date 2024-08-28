using NUnit.Framework;

namespace PolarisLite.Web.Plugins;

public class MetricPreciseValidationBuilder
{
    public static event EventHandler<LighthouseReportEventArgs> AssertedLighthouseReportEventArgs;

    private dynamic _actualValue;
    private string _metricName;

    public MetricPreciseValidationBuilder(dynamic actualValue, string message)
    {
        _actualValue = actualValue;
        _metricName = message;
    }

    public FinishValidationBuilder Equal<T>(T expected)
    {
        return new FinishValidationBuilder(() => _actualValue == expected,
                () => BuildNotificationValidationMessage(ComparingOperators.EQUAL, expected),
                () => BuildFailedValidationMessage(ComparingOperators.EQUAL, expected));
    }

    public FinishValidationBuilder GreaterThan<T>(T expected)
    {
        return new FinishValidationBuilder(() => _actualValue > expected,
                () => BuildNotificationValidationMessage(ComparingOperators.GREATER_THAN, expected),
                () => BuildFailedValidationMessage(ComparingOperators.GREATER_THAN, expected));
    }

    public FinishValidationBuilder GreaterThanOrEqual<T>(T expected)
    {
        return new FinishValidationBuilder(() => _actualValue >= expected,
                () => BuildNotificationValidationMessage(ComparingOperators.GREATER_THAN_EQUAL, expected),
                () => BuildFailedValidationMessage(ComparingOperators.GREATER_THAN_EQUAL, expected));
    }

    public FinishValidationBuilder LessThan<T>(T expected)
    {
        return new FinishValidationBuilder(() => _actualValue < expected,
                () => BuildNotificationValidationMessage(ComparingOperators.LESS_THAN, expected),
                () => BuildFailedValidationMessage(ComparingOperators.LESS_THAN, expected));
    }

    public FinishValidationBuilder LessThanOrEqual<T>(T expected)
    {
        return new FinishValidationBuilder(() => _actualValue <= expected,
                () => BuildNotificationValidationMessage(ComparingOperators.LESS_THAN_EQAUL, expected),
                () => BuildFailedValidationMessage(ComparingOperators.LESS_THAN_EQAUL, expected));
    }

    private string BuildNotificationValidationMessage<T>(ComparingOperators comparingMessage, T expected)
    {
        // Get ENUM description
        return $"{_metricName} {comparingMessage} {expected}";
    }

    private string BuildFailedValidationMessage<T>(ComparingOperators comparingMessage, T expected)
    {
        // Get ENUM description
        return $"{_metricName} {comparingMessage} {expected}";
    }

    public void Perform()
    {
        try
        {
            Assert.That(_actualValue > 0, Is.True, _metricName);
        }
        catch (AssertionException ex)
        {
            throw new LighthouseAssertFailedException($"Assertion failed for metric: {_metricName}", ex);
        }

        AssertedLighthouseReportEventArgs?.Invoke(this, new LighthouseReportEventArgs(_metricName));
    }
}
