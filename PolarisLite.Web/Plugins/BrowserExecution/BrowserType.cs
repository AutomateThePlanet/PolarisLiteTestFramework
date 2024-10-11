using System.ComponentModel;

namespace PolarisLite.Web.Plugins;

public enum BrowserType
{
    NotSet,
    [Description("Chrome")]
    Chrome,
    ChromeHeadless,
    [Description("Firefox")]
    Firefox,
    FirefoxHeadless,
    [Description("Edge")]
    Edge,
    EdgeHeadless,
    [Description("Safari")]
    Safari
}
