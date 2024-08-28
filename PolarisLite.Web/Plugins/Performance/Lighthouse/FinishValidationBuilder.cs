﻿using NUnit.Framework;

namespace PolarisLite.Web.Plugins;

public class FinishValidationBuilder
{
    public static event EventHandler<LighthouseReportEventArgs> AssertedLighthouseReportEventArgs;

    private Func<bool> _comparingFunction;
    private string _notificationMessage;
    private string _failedAssertionMessage;

    public FinishValidationBuilder(Func<bool> comparingFunction) => _comparingFunction = comparingFunction;

    public FinishValidationBuilder(Func<bool> comparingFunction, Func<string> notificationMessageFunction, Func<string> failedAssertionMessageFunction)
    {
        _comparingFunction = comparingFunction;
        _notificationMessage = notificationMessageFunction.Invoke();
        _failedAssertionMessage = failedAssertionMessageFunction.Invoke();
    }

    public void Perform()
    {
        try
        {
            Assert.That(_comparingFunction.Invoke(), Is.True);
        }
        catch (AssertionException ex)
        {
            throw new LighthouseAssertFailedException(_failedAssertionMessage, ex);
        }

        AssertedLighthouseReportEventArgs?.Invoke(this, new LighthouseReportEventArgs(_notificationMessage));
    }
}
