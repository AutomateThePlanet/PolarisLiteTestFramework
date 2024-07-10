namespace PolarisLite.Web.Services;

public partial class DriverAdapter : IDialogService
{
    public void Back()
    {
        WrappedDriver.Navigate().Back();
    }

    public void Forward()
    {
        WrappedDriver.Navigate().Forward();
    }

    public void Refresh()
    {
        WrappedDriver.Navigate().Refresh();
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
