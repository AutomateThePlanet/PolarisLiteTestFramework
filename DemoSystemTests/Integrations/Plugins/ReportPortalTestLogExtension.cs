﻿using PolarisLite.Integrations.LambdaTestAPI;
using ReportPortal.Client.Abstractions.Models;
using ReportPortal.Client.Abstractions.Requests;
using ReportPortal.NUnitExtension;
using OpenQA.Selenium.Remote;
using LogLevel = ReportPortal.Client.Abstractions.Models.LogLevel;
using NUnit.Engine;
using ReportPortal.Client.Abstractions.Responses;
using PolarisLite.Integrations.Settings;
using PolarisLite.Logging;
using PolarisLite.Web.Plugins;
using System.Diagnostics;

namespace DemoSystemTests.Integrations.Plugins;

[NUnit.Engine.Extensibility.Extension]
public class ReportPortalTestLogExtension : ITestEventListener
{
    private readonly SessionApiClient _sessionApiClient;
    private readonly BuildApiClient _buildApiClient;

    public ReportPortalTestLogExtension()
    {
        try
        {
            if (IntegrationSettings.ReportPortalEnabled)
            {
                _sessionApiClient = new SessionApiClient();
                _buildApiClient = new BuildApiClient();

                //ReportPortalListener.AfterRunStarted += ReportPortalListener_BeforeRunStarted;
                //ReportPortalListener.AfterTestStarted += ReportPortalListener_AfterTestStarted;
                ReportPortalListener.BeforeTestFinished += ReportPortalListener_AfterTestFinished;
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.ToString());
        }
    }

    //private void ReportPortalListener_BeforeRunStarted(object sender, ReportPortal.NUnitExtension.EventArguments.RunStartedEventArgs e)
    //{
    //    try
    //    {
    //        if (IntegrationSettings.ReportPortalEnabled)
    //        {
    //            var buildName = "random BUILD NAME";
    //            // Add custom attributes
    //            e.StartLaunchRequest.Attributes.Add(new ItemAttribute { Value = buildName });

    //            // Change custom description
    //            e.StartLaunchRequest.Description += Environment.NewLine + Environment.OSVersion;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.LogError(ex.ToString());
    //    }
    //}

    //private void ReportPortalListener_AfterTestStarted(object sender, ReportPortal.NUnitExtension.EventArguments.TestItemStartedEventArgs e)
    //{
    //    try
    //    {
    //        if (IntegrationSettings.ReportPortalEnabled)
    //        {
        
    //            e.TestReporter.Log(new CreateLogItemRequest
    //            {
    //                Level = LogLevel.Trace,
    //                Time = DateTime.UtcNow,
    //                Text = "This message is from 'ReportPortalListener_AfterTestStarted' event."
    //            });

    //            e.TestReporter.StartTask.Wait();
    //            var infoTask = Task.Run(async () => await e.Service.TestItem.GetAsync(e.TestReporter.Info.Uuid));
    //            infoTask.Wait();
    //            var testInfo = infoTask.Result;
    //            e.TestReporter.Log(new CreateLogItemRequest
    //            {
    //                Level = LogLevel.Trace,
    //                Time = DateTime.UtcNow,
    //                Text = $"Actual test ID: {testInfo.UniqueId}"
    //            });
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.LogError(ex.ToString());
    //    }
    //}

    private void ReportPortalListener_AfterTestFinished(object sender, ReportPortal.NUnitExtension.EventArguments.TestItemFinishedEventArgs e)
    {
        if (IntegrationSettings.ReportPortalEnabled)
        {
            try
            {
                bool isLambdaTestRun = DriverFactory.ExecutionType.Equals(ExecutionType.LambdaTest);
                if (isLambdaTestRun)
                {
                    var sessionId = DriverFactory.CurrentSessionId;

                    var sessionDetailsResponse = _sessionApiClient.GetSessionDetailsAsync(sessionId, "30").Result;

                    if (sessionDetailsResponse != null)
                    {
                        var setExpiryLimitBuildPublicUrl = GetExpiryLimit30Url(sessionDetailsResponse.Data.Data.BuildId.ToString());

                        e.TestReporter.Log(new CreateLogItemRequest
                        {
                            Level = LogLevel.Debug,
                            Time = DateTime.UtcNow,
                            Text = $"LambdaTest Video URL = {setExpiryLimitBuildPublicUrl}"
                        });

                        var buildName = DriverFactory.GridSettings.BuildName;
                        string lambdaTestBuildUrl = $"https://automation.lambdatest.com/build?&searchText={buildName}";

                        e.TestReporter.Log(new CreateLogItemRequest
                        {
                            Level = LogLevel.Debug,
                            Time = DateTime.UtcNow,
                            Text = $"LambdaTest Build URL = {lambdaTestBuildUrl}"
                        });

                        // Azure DevOps Build ID Logging
                        var azureBuildId = Environment.GetEnvironmentVariable("AZURE_DEVOPS_BUILD_ID", EnvironmentVariableTarget.Process);
                        if (!string.IsNullOrEmpty(azureBuildId))
                        {
                            Console.WriteLine($"Azure Build Id -> {azureBuildId}");
                            string azureBuildUrl = $"https://dev.azure.com/atp/QA/_build/results?buildId={azureBuildId}";

                            e.TestReporter.Log(new CreateLogItemRequest
                            {
                                Level = LogLevel.Debug,
                                Time = DateTime.UtcNow,
                                Text = $"DevOps Build URL = {azureBuildUrl}"
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
            }

            e.TestReporter.Log(new CreateLogItemRequest
            {
                Level = LogLevel.Debug,
                Time = DateTime.UtcNow,
                Text = "TEST FINISHED"
            });
        }
    }

    private string GetExpiryLimit30Url(string buildId)
    {
        var buildResponse = _buildApiClient.SinglebuildAsync(int.Parse(buildId), "30").Result;
        if (buildResponse != null)
        {
            return buildResponse.Data.Data.PublicUrl;
        }
        return string.Empty;
    }

    public void OnTestEvent(string report)
    {
    }
}
