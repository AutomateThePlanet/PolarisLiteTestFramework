

using PolarisLite.Web.Services;

namespace PolarisLite.Web.Troubshoting;

public class HtmlSourceExceptionHandler : ExceptionAnalysationHandler
{
    private readonly string _textToSearchInSource;
    public HtmlSourceExceptionHandler(string textToSearchInSource, string detailedIssueExplanation)
    {
        _textToSearchInSource = textToSearchInSource;
        DetailedIssueExplanation = detailedIssueExplanation;
    }

    public override string DetailedIssueExplanation { get; }

    public override bool IsApplicable(DriverAdapter driver, Exception ex = null, params object[] context)
    {
        if (driver == null)
        {
            throw new ArgumentNullException(nameof(driver));
        }

        // TODO: FIX
        //var result = driver.HtmlSource.Contains(_textToSearchInSource);
        return true;
    }
}
