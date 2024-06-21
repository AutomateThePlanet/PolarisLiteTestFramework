using PolarisLite.Web.Services;

namespace PolarisLite.Web.Troubshoting;

public abstract class ExceptionAnalysationHandler
{
    private ExceptionAnalysationHandler _nextExceptionAnalysationHandler;

    public abstract string DetailedIssueExplanation { get; }

    public abstract bool IsApplicable(DriverAdapter driver, Exception ex = null, params object[] context);

    public void Analyse(DriverAdapter driver, Exception ex = null, params object[] context)
    {
        if (IsApplicable(driver, ex, context))
        {
            if (driver != null)
            {
                string url = driver.Url.ToString();
                throw new AnalyzedTestException(DetailedIssueExplanation, url, ex);
            }
            else
            {
                throw new AnalyzedTestException(DetailedIssueExplanation);
            }
        }
        else if (_nextExceptionAnalysationHandler != null)
        {
            _nextExceptionAnalysationHandler.Analyse(driver, ex, context);
        }
    }

    public void AddExceptionAnalysationHandler<TExceptionAnalysationHandler>(
        TExceptionAnalysationHandler exceptionAnalysationHandler)
        where TExceptionAnalysationHandler : ExceptionAnalysationHandler
    {
        _nextExceptionAnalysationHandler = exceptionAnalysationHandler;
    }
}
