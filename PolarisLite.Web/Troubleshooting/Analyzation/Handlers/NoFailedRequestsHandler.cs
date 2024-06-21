

using PolarisLite.Web.Services;

namespace PolarisLite.Web.Troubshoting;

public class NoFailedRequestsHandler : ExceptionAnalysationHandler
{
    public override string DetailedIssueExplanation
    {
        get
        {
            return "There were multiple failed requests.";
        }
    }

    public override bool IsApplicable(DriverAdapter driver, Exception ex = null, params object[] context)
    {
        if (driver == null)
        {
            throw new ArgumentNullException(nameof(driver));
        }

        // TODO: fix
        //bool areThereErrorCodes = driver.ResponsesHistory.Any(r => r.StatusCode > 400 && r.StatusCode < 599);

        return false;
    }
}
