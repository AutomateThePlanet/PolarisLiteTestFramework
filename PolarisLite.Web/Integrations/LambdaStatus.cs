using System.ComponentModel;

namespace PolarisLite.Web.Integrations;
public enum LambdaStatus
{
    [Description("passed")]
    PASSED,
    [Description("failed")]
    FAILED,
    [Description("error")]
    ERROR,
    [Description("unknown")]
    UNKNOWN,
    [Description("ignored")]
    IGNORED,
    [Description("skipped")]
    SKIPPED
}
