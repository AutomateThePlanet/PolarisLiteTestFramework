using OpenQA.Selenium.Support.UI;
using PolarisLite.Web.Configuration.StaticImplementation;
using System.Text;

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
        try
        {
            if (WebSettings.EnableToastMessages)
            {
                var webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
                webDriverWait.Until(d => (bool)((IJavaScriptExecutor)d).ExecuteScript("return typeof $.jGrowl === 'function';"));
                string escapedMessage = EscapeJavaScriptString(message);
                string script = string.Format("$.jGrowl('{0}', {{ header: 'Info', theme: 'info', life: 2000, speed: 'fast' }});", escapedMessage);
                Execute(script);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private string EscapeJavaScriptString(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        StringBuilder sb = new StringBuilder(value.Length);
        foreach (char c in value)
        {
            switch (c)
            {
                case '\'':
                    sb.Append("\\'");
                    break;
                case '"':
                    sb.Append("\\\"");
                    break;
                case '\\':
                    sb.Append("\\\\");
                    break;
                case '\n':
                    sb.Append("\\n");
                    break;
                case '\r':
                    sb.Append("\\r");
                    break;
                case '\t':
                    sb.Append("\\t");
                    break;
                case '\b':
                    sb.Append("\\b");
                    break;
                case '\f':
                    sb.Append("\\f");
                    break;
                default:
                    if (c < 32 || c > 126)
                    {
                        sb.AppendFormat("\\u{0:X4}", (int)c);
                    }
                    else
                    {
                        sb.Append(c);
                    }
                    break;
            }
        }
        return sb.ToString();
    }

    public void SuccessToastMessage(string message)
    {
        if (WebSettings.EnableToastMessages)
        {
            Execute(string.Format("$.jGrowl('{0}', {{ header: 'Success', theme: 'success' }});", message));
        }
    }
}
