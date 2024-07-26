using PolarisLite.Web.Plugins;

namespace PolarisLite.Web.Services;

public partial class DriverAdapter
{
    private IWebDriver _webDriver;
    
    public DriverAdapter()
    {
        _webDriver = DriverFactory.WrappedDriver;
        if (_webDriver != null)
        {
            if (_webDriver is IDevTools)
            {
                DevToolsSession = ((IDevTools)_webDriver).GetDevToolsSession();
                DevToolsSessionDomains = DevToolsSession.GetVersionSpecificDomains<DevToolsSessionDomains>();

                //DevToolsSessionDomains.Page.Enable(new PAGE.EnableCommandSettings());

                //// Add script to evaluate on new document
                //DevToolsSessionDomains.Page.AddScriptToEvaluateOnNewDocument(new PAGE.AddScriptToEvaluateOnNewDocumentCommandSettings()
                //{
                //    Source = @"window.onload = () => {
                //                    if (!window.jQuery) {
                //                        var jquery = document.createElement('script'); 
                //                        jquery.type = 'text/javascript';
                //                        jquery.src = 'https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js';
                //                        document.getElementsByTagName('head')[0].appendChild(jquery);
                //                    } else {
                //                        $ = window.jQuery;
                //                    }

                //                    $.getScript('https://cdnjs.cloudflare.com/ajax/libs/jquery-jgrowl/1.4.8/jquery.jgrowl.min.js');
                //                    $('head').append('<link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/jquery-jgrowl/1.4.8/jquery.jgrowl.min.css"" type=""text/css"" />');
                //                };

                //                function highlight(element){
                //                    let defaultBG = element.style.backgroundColor;
                //                    let defaultOutline = element.style.outline;
                //                    element.style.backgroundColor = '#FDFF47';
                //                    element.style.outline = '#f00 solid 2px';

                //                    setTimeout(function(){
                //                        element.style.backgroundColor = defaultBG;
                //                        element.style.outline = defaultOutline;
                //                    }, 1000);
                //                }
                //            "
                //});
            }
        }
    }

    public DriverAdapter(IWebDriver driver)
    {
        _webDriver = driver;
    }

    public IWebDriver WrappedDriver => _webDriver;
}