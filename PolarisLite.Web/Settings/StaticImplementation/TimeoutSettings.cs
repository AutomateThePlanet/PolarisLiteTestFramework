namespace PolarisLite.Web.Configuration.StaticImplementation;
public class TimeoutSettings
{
    /// <summary>
    /// Gets or sets the page load timeout in seconds.
    /// </summary>
    public int PageLoadTimeout { get; set; } = 30;
    public int ScriptTimeout { get; set; } = 30;
    public int ValidationsTimeout { get; set; } = 30;

    public int WaitForAjaxTimeout { get; set; } = 30;
    public int SleepInterval { get; set; } = 10;

    public int ElementToBeVisibleTimeout { get; set; } = 30;
    public int ElementToExistTimeout { get; set; } = 30;
    public int ElementToNotExistTimeout { get; set; } = 10;
    public int ElementToBeClickableTimeout { get; set; } = 30;
    public int ElementNotToBeVisibleTimeout { get; set; } = 10;
    public int ElementToHaveContentTimeout { get; set; } = 30;
}
