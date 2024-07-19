namespace PolarisLite.Mobile;

public class TimeoutSettings
{
    public int PageLoadTimeout { get; set; } = 30000;
    public int ScriptTimeout { get; set; } = 30000;
    public int ValidationsTimeout { get; set; } = 30000;

    public int ElementToBeVisibleTimeout { get; set; } = 30000;
    public int ElementToExistTimeout { get; set; } = 30000;
    public int ElementToNotExistTimeout { get; set; } = 10000;
    public int ElementToBeClickableTimeout { get; set; } = 30000;
    public int ElementNotToBeVisibleTimeout { get; set; } = 10000;
    public int ElementToHaveContentTimeout { get; set; } = 30000;
}