

using PolarisLite.Web.Services;

namespace PolarisLite.Web.Troubshoting;

public class UrlExceptionHandler : ExceptionAnalysationHandler
{
    private readonly string _textToSearchInUrl;

    public UrlExceptionHandler(string textToSearchInUrl, string detailedIssueExplanation)
    {
        _textToSearchInUrl = textToSearchInUrl;
        DetailedIssueExplanation = detailedIssueExplanation;

    }

    public override string DetailedIssueExplanation { get; }

    public override bool IsApplicable(DriverAdapter driver, Exception ex = null, params object[] context)
    {
        if (driver == null)
        {
            throw new ArgumentNullException(nameof(driver));
        }

        var result = driver.Url.Contains(_textToSearchInUrl);
        return result;
    }
}
