

using PolarisLite.Web.Services;

namespace PolarisLite.Web.Troubshoting;

public class JavaScriptErrorsHandler : ExceptionAnalysationHandler
{
    public override string DetailedIssueExplanation
    {
        get
        {
            return "There were multiple JavaScript errors, check console logs for more information.";
        }
    }

    public override bool IsApplicable(DriverAdapter driver, Exception ex = null, params object[] context)
    {
        if (driver == null)
        {
            throw new ArgumentNullException(nameof(driver));
        }

        // TODO: fix
        //if (driver.JavaScriptErrors.Any())
        //{
        //    driver.JavaScriptErrors.ForEach(e => Console.WriteLine(e));
        //}

        //return driver.JavaScriptErrors.Any();
        return true;
    }
}
