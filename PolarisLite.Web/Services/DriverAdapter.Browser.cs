using PolarisLite.Web.Configuration.StaticImplementation;

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
        var webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
        webDriverWait.Until(driver =>
        {
            var script = "return window.jQuery != undefined && jQuery.active == 0";
            return (bool)((IJavaScriptExecutor)driver).ExecuteScript(script);
        });
    }

    public void ErrorToastMessage(string message)
    {
        if (WebSettings.EnableToastMessages)
        {
            Execute(string.Format("$.jGrowl('{0}', {{ header: 'Error', theme: 'error' }});", message));
        }
    }

    public void WarningToastMessage(string message)
    {
        if (WebSettings.EnableToastMessages)
        {
            Execute(string.Format("$.jGrowl('{0}', {{ header: 'Warning', theme: 'warning' }});", message));
        }
    }

    public void InfoToastMessage(string message)
    {
        if (WebSettings.EnableToastMessages)
        {
            try
            {
                string script = string.Format("$.jGrowl('{0}', {{ header: 'Info', theme: 'info' }});", message);
                Execute(script);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
            }
        }
    }

    public void SuccessToastMessage(string message)
    {
        if (WebSettings.EnableToastMessages)
        {
            Execute(string.Format("$.jGrowl('{0}', {{ header: 'Success', theme: 'success' }});", message));
        }
    }
}
