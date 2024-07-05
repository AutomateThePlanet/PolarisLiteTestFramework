namespace PolarisLite.Web.Core;
public enum Lifecycle
{
    NotSet = 0,
    ReuseIfStarted = 1,
    RestartEveryTime = 2,
    RestartOnFail = 3,
}
