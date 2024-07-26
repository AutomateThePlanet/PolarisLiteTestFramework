

using PolarisLite.Web.Services;

namespace PolarisLite.Web.Troubshoting;

public abstract class JavaScriptConsoleWarningsHandler : ExceptionAnalysationHandler
{
    protected abstract string TextToSearchInSource { get; }

    public override bool IsApplicable(DriverAdapter driver, Exception ex = null, params object[] context)
    {
        if (driver == null)
        {
            throw new ArgumentNullException(nameof(driver));
        }

        // TODO:
        //var result = driver.ConsoleMessages.Contains(TextToSearchInSource);

        return true;
    }
}
