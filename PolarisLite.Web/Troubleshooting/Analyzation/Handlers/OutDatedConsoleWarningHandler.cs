namespace PolarisLite.Web.Troubshoting;
public class OutDatedConsoleWarningHandler : JavaScriptConsoleWarningsHandler
{
    public override string DetailedIssueExplanation
    {
        get
        {
            return "There were some console warning about outdated libraries.";
        }
    }

    protected override string TextToSearchInSource
    {
        get
        {
            return "outdated";
        }
    }
}
