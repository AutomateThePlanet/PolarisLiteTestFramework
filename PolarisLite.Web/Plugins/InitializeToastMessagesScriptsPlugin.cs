using PolarisLite.Core;
using PolarisLite.Web.Configuration.StaticImplementation;
using PolarisLite.Web.Services;
using System.Reflection;

namespace PolarisLite.Web.Plugins;
public class InitializeToastMessagesScriptsPlugin : Plugin
{
    // TODO: Should be added to be executed after BrowserLifecyclePlugin
    public override void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        if (WebSettings.EnableToastMessages)
        {
            var devToolsService = new DriverAdapter();
            devToolsService.DevToolsSessionDomains.Page.Enable(new PAGE.EnableCommandSettings()).Wait();
            devToolsService.DevToolsSessionDomains.Page.AddScriptToEvaluateOnNewDocument(new PAGE.AddScriptToEvaluateOnNewDocumentCommandSettings()
            {
                Source = @"window.onload = () => {
                                if (!window.jQuery) {
                                    var jquery = document.createElement('script'); 
                                    jquery.type = 'text/javascript';
                                    jquery.src = 'https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js';
                                    document.getElementsByTagName('head')[0].appendChild(jquery);
                                } else {
                                    $ = window.jQuery;
                                }

                                $.getScript('https://cdnjs.cloudflare.com/ajax/libs/jquery-jgrowl/1.4.8/jquery.jgrowl.min.js');
                                $('head').append('<link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/jquery-jgrowl/1.4.8/jquery.jgrowl.min.css"" type=""text/css"" />');
                            };
                        "
            }).Wait();
        }
    }
}