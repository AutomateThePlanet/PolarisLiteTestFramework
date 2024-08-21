using PolarisLite.Integrations.LambdaTestAPI;
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

                ReportPortalListener.BeforeRunStarted += ReportPortalListener_BeforeRunStarted;
                ReportPortalListener.AfterTestStarted += ReportPortalListener_AfterTestStarted;
                ReportPortalListener.AfterTestFinished += ReportPortalListener_AfterTestFinished;
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.ToString());
        }
    }

    private void ReportPortalListener_BeforeRunStarted(object sender, ReportPortal.NUnitExtension.EventArguments.RunStartedEventArgs e)
    {
        try
        {
            if (IntegrationSettings.ReportPortalEnabled)
            {
                var buildName = Environment.GetEnvironmentVariable("BUILD_NAME");
                // Add custom attributes
                e.StartLaunchRequest.Attributes.Add(new ItemAttribute { Value = buildName });

                // Change custom description
                e.StartLaunchRequest.Description += Environment.NewLine + Environment.OSVersion;
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.ToString());
        }
    }

    private void ReportPortalListener_AfterTestStarted(object sender, ReportPortal.NUnitExtension.EventArguments.TestItemStartedEventArgs e)
    {
        try
        {
            if (IntegrationSettings.ReportPortalEnabled)
            {
                e.TestReporter.Log(new CreateLogItemRequest
                {
                    Level = LogLevel.Trace,
                    Time = DateTime.UtcNow,
                    Text = "This message is from 'ReportPortalListener_AfterTestStarted' event."
                });

                if (e.StartTestItemRequest.Name.StartsWith("Sync"))
                {
                    e.TestReporter.StartTask.Wait();
                    var testInfo = e.Service.TestItem.GetAsync(e.TestReporter.Info.Uuid).Result;
                    e.TestReporter.Log(new CreateLogItemRequest
                    {
                        Level = LogLevel.Trace,
                        Time = DateTime.UtcNow,
                        Text = $"Actual test ID: {testInfo.UniqueId}"
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.ToString());
        }
    }

    private void ReportPortalListener_AfterTestFinished(object sender, ReportPortal.NUnitExtension.EventArguments.TestItemFinishedEventArgs e)
    {
        if (IntegrationSettings.ReportPortalEnabled)
        {
            if (e.FinishTestItemRequest.Status == Status.Skipped)
            {
                e.FinishTestItemRequest.Issue = new Issue
                {
                    Type = WellKnownIssueType.NotDefect
                };
            }

            try
            {
                bool isLambdaTestRun = DriverFactory.ExecutionType.Equals(ExecutionType.LambdaTest);
                if (isLambdaTestRun)
                {
                    var sessionId = ((RemoteWebDriver)DriverFactory.WrappedDriver).SessionId.ToString();

                    var sessionDetailsResponse = _sessionApiClient.GetSessionDetailsAsync(sessionId, "30").Result;

                    if (sessionDetailsResponse.IsSuccessful)
                    {
                        var setExpiryLimitBuildPublicUrl = GetExpiryLimit30Url(sessionDetailsResponse.Data.Data.BuildId.ToString());

                        e.TestReporter.Log(new CreateLogItemRequest
                        {
                            Level = LogLevel.Fatal,
                            Time = DateTime.UtcNow,
                            Text = $"LambdaTest Video URL = {setExpiryLimitBuildPublicUrl}"
                        });

                        var buildName = Environment.GetEnvironmentVariable("BUILD_NAME");
                        string lambdaTestBuildUrl = $"https://automation.lambdatest.com/build?&searchText={buildName}";

                        e.TestReporter.Log(new CreateLogItemRequest
                        {
                            Level = LogLevel.Error,
                            Time = DateTime.UtcNow,
                            Text = $"LambdaTest Build URL = {lambdaTestBuildUrl}"
                        });

                        // Azure DevOps Build ID Logging
                        var azureBuildId = Environment.GetEnvironmentVariable("AZURE_DEVOPS_BUILD_ID");
                        if (!string.IsNullOrEmpty(azureBuildId))
                        {
                            Console.WriteLine($"Azure Build Id -> {azureBuildId}");
                            string azureBuildUrl = $"https://dev.azure.com/atp/QA/_build/results?buildId={azureBuildId}";

                            e.TestReporter.Log(new CreateLogItemRequest
                            {
                                Level = LogLevel.Fatal,
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
        }
    }

    private string GetExpiryLimit30Url(string buildId)
    {
        var buildResponse = _buildApiClient.SinglebuildAsync(int.Parse(buildId), "30").Result;
        if (buildResponse.IsSuccessful)
        {
            return buildResponse.Data.Data.PublicUrl;
        }
        return string.Empty;
    }

    public void OnTestEvent(string report)
    {
    }
}
