﻿using PolarisLite.Core;
using PolarisLite.Core.Infrastructure.NUnit;
using PolarisLite.Web.Plugins;
using Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;
using PolarisLite.Web.Extensions.Controls.Controls.EventHandlers;
using PolarisLite.Web.Configuration.StaticImplementation;
using PolarisLite.Web.Troubshoting;
using PolarisLite.Api.Configuration;
using PolarisLite.Web.Plugins.Browser;
using PolarisLite.Web.Plugins.Troubleshooting;

namespace PolarisLite.Web.Core.NUnit;
public class WebTest : BaseTest
{
    private static bool _arePluginsAlreadyInitialized = false;

    public App App => new App();
    public API.App ApiApp => new API.App();

    protected override void Configure()
    {
        WebConfigurationLoader.LoadConfiguration();
        ApiConfigurationLoader.LoadConfiguration();
        if (!_arePluginsAlreadyInitialized)
        {
            //PluginExecutionEngine.AddPlugin(new PerformanceTestingPlugin());
            //PluginExecutionEngine.AddPlugin(new JavaScriptErrorsPlugin());
            PluginExecutionEngine.AddPlugin(new LambdaTestResultsPlugin());
            //PluginExecutionEngine.AddPlugin(new ExceptionAnalysationPlugin());
            //PluginExecutionEngine.AddPlugin(new WebScreenshotPlugin());
            PluginExecutionEngine.AddPlugin(new BrowserLifecyclePlugin());
            //PluginExecutionEngine.AddPlugin(new InitializeHighlightScriptsPlugin());
            //PluginExecutionEngine.AddPlugin(new InitializeToastMessagesScriptsPlugin());
            PluginExecutionEngine.AddPlugin(new LogLifecyclePlugin());
            PluginExecutionEngine.AddPlugin(new LoggerFlushPlugin());
          

            //WebComponentPluginExecutionEngine.AddPlugin(new HighlightElementPlugin());
            //WebComponentPluginExecutionEngine.AddPlugin(new ScrollIntoViewPlugin());

            AddBddLogging();
            //AddToastMessages();

            //ExceptionAnalyser.AddExceptionAnalysationHandler(new OutDatedConsoleWarningHandler());
            //ExceptionAnalyser.AddExceptionAnalysationHandler<JavaScriptErrorsHandler>();
            //ExceptionAnalyser.AddExceptionAnalysationHandler<NoFailedRequestsHandler>();
            //ExceptionAnalyser.AddExceptionAnalysationHandler<ServiceUnavailableExceptionHandler>();
            _arePluginsAlreadyInitialized = true;
        }
    }

    public static void AddBddLogging()
    {
        if (WebSettings.EnableBDDLogging)
        {
            new BDDLoggingButtonEventHandlers().SubscribeToAll();
            new BDDLoggingAnchorEventHandlers().SubscribeToAll();
            new BDDLoggingTextFieldEventHandlers().SubscribeToAll();
            new BDDLoggingDateEventHandlers().SubscribeToAll();
            new BDDLoggingCheckboxEventHandlers().SubscribeToAll();
            new BDDLoggingEmailEventHandlers().SubscribeToAll();
            new BDDLoggingInputFileEventHandlers().SubscribeToAll();
            new BDDLoggingSelectEventHandlers().SubscribeToAll();
            new BDDLoggingValidateExtensionsService().SubscribeToAll();
        }
    }

    public static void AddToastMessages()
    {
        if (WebSettings.EnableToastMessages)
        {
            new ToastMessagesTextFieldEventHandlers().SubscribeToAll();
            new ToastMessagesDateEventHandlers().SubscribeToAll();
            new ToastMessagesCheckboxEventHandlers().SubscribeToAll();
            new ToastMessagesEmailEventHandlers().SubscribeToAll();
            new ToastMessagesInputFileEventHandlers().SubscribeToAll();
            new ToastMessagesSelectEventHandlers().SubscribeToAll();
            new ToastMessagesValidateExtensionsService().SubscribeToAll();
        }
    }
}