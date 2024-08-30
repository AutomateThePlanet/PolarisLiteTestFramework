using PolarisLite.Core;
using PolarisLite.Web.Configuration.StaticImplementation;
using PolarisLite.Web.Services;
using System.Reflection;

namespace PolarisLite.Web.Plugins.Troubleshooting;
public class InitializeHighlightScriptsPlugin : Plugin
{
    // TODO: Should be added to be executed after BrowserLifecyclePlugin
    public override void OnBeforeTestInitialize(MethodInfo memberInfo)
    {
        if (WebSettings.EnableHighlight)
        {
            var devToolsService = new DriverAdapter();
            devToolsService.DevToolsSessionDomains.Page.Enable(new PAGE.EnableCommandSettings());
            devToolsService.DevToolsSessionDomains.Page.AddScriptToEvaluateOnNewDocument(new PAGE.AddScriptToEvaluateOnNewDocumentCommandSettings()
            {
                Source = @"
                            function highlight(element){
                                let defaultBG = element.style.backgroundColor;
                                let defaultOutline = element.style.outline;
                                element.style.backgroundColor = '#FDFF47';
                                element.style.outline = '#f00 solid 2px';

                                setTimeout(function(){
                                    element.style.backgroundColor = defaultBG;
                                    element.style.outline = defaultOutline;
                                }, 1000);
                            }
                        "
            });
        }
    }
}