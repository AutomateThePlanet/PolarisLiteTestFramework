using PolarisLite.Web.Services;

namespace PolarisLite.Web.Troubshoting;

public abstract class ExceptionAnalysationHandler
{
    public abstract string DetailedIssueExplanation { get; }

    public abstract bool IsApplicable(DriverAdapter driver, Exception ex = null, params object[] context);
}
