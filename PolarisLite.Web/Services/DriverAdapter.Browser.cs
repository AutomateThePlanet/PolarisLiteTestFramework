namespace PolarisLite.Web.Services;

public partial class DriverAdapter : IBrowserService
{
    public void Back()
    {
        _webDriver.Navigate().Back();
    }

    public void Forward()
    {
        _webDriver.Navigate().Forward();
    }

    public void Refresh()
    {
        _webDriver.Navigate().Refresh();
    }

    public void WaitForAjax()
    {
        _webDriverWait.Until(driver =>
        {
            var script = "return window.jQuery != undefined && jQuery.active == 0";
            return (bool)((IJavaScriptExecutor)driver).ExecuteScript(script);
        });
    }
}
