using Newtonsoft.Json.Linq;
using NUnit.Engine;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Remote;
using PolarisLite.Core;
using PolarisLite.Secrets;
using PolarisLite.Web.Plugins.BrowserExecution;
using ReportPortal.Client.Abstractions.Models;
using ReportPortal.Client.Abstractions.Requests;
using ReportPortal.Client.Abstractions.Responses;
using ReportPortal.NUnitExtension;
using ReportPortal.Shared.Reporter;
using RestSharp;
using System.Text;
using LogLevel = ReportPortal.Client.Abstractions.Models.LogLevel;

namespace PolarisLite.Web.Plugins;

[NUnit.Engine.Extensibility.Extension]
public class ReportPortalTestLogExtension : ITestEventListener
{
    private readonly bool _reportPortalEnabled;
    public ReportPortalTestLogExtension()
    {
        var webSettings = ConfigurationService.GetSection<WebSettings>();
        _reportPortalEnabled = webSettings.ReportPortalEnabled;

        if (_reportPortalEnabled)
        {
            ReportPortalListener.BeforeRunStarted += ReportPortalListener_BeforeRunStarted;
            ReportPortalListener.AfterTestStarted += ReportPortalListener_AfterTestStarted;
            ReportPortalListener.AfterTestFinished += ReportPortalListener_AfterTestFinished;
        }
    }

    private void ReportPortalListener_BeforeRunStarted(object sender, ReportPortal.NUnitExtension.EventArguments.RunStartedEventArgs e)
    {
        if (_reportPortalEnabled)
        {
            // Add custom attributes
            e.StartLaunchRequest.Attributes.Add(new ItemAttribute { Value = "custom_tag" });

            // Change custom description
            e.StartLaunchRequest.Description += Environment.NewLine + Environment.OSVersion;
        }
    }

    private void ReportPortalListener_AfterTestStarted(object sender, ReportPortal.NUnitExtension.EventArguments.TestItemStartedEventArgs e)
    {
        if (_reportPortalEnabled)
        {
            e.TestReporter.Log(new CreateLogItemRequest
            {
                Level = LogLevel.Trace,
                Time = DateTime.UtcNow,
                Text = "This message is from 'ReportPortalListener_AfterTestStarted' event."
            });

            if (e.StartTestItemRequest.Name.StartsWith("Sync"))
            {
                // Wait until the test is being reported to the server and retrieve info
                e.TestReporter.StartTask.Wait();
                var infoTask = Task.Run(async () => await e.Service.TestItem.GetAsync(e.TestReporter.Info.Uuid));
                infoTask.Wait();
                var testInfo = infoTask.Result;
                e.TestReporter.Log(new CreateLogItemRequest
                {
                    Level = LogLevel.Trace,
                    Time = DateTime.UtcNow,
                    Text = $"Actual test ID: {testInfo.UniqueId}"
                });
            }
        }
    }

    private void ReportPortalListener_AfterTestFinished(object sender, ReportPortal.NUnitExtension.EventArguments.TestItemFinishedEventArgs e)
    {
        if (_reportPortalEnabled)
        {
            // Don't assign "To investigate" for skipped tests
            if (e.FinishTestItemRequest.Status == Status.Skipped)
            {
                e.FinishTestItemRequest.Issue = new Issue
                {
                    Type = WellKnownIssueType.NotDefect
                };
            }

            // Modify description of tests
            var pattern = "{MachineName}";
            if (e.FinishTestItemRequest.Description != null && e.FinishTestItemRequest.Description.Contains(pattern))
            {
                e.FinishTestItemRequest.Description = e.FinishTestItemRequest.Description.Replace(pattern, Environment.MachineName);
            }

            try
            {
                var webSettings = ConfigurationService.GetSection<WebSettings>();
                bool isLambdaTestRun = webSettings.ExecutionType.Equals("lambda test", StringComparison.OrdinalIgnoreCase);
                if (isLambdaTestRun)
                {
                    var app = new App();
                    var sessionId = ((RemoteWebDriver)DriverFactory.WrappedDriver).SessionId.ToString(); // FETCHING SESSION ID

                    var gridSettings = webSettings.GridSettings;
                    var x = gridSettings[0].Arguments;
                    var accessKey = SecretsResolver.GetSecret(x["accessKey"].ToString());
                    var username = SecretsResolver.GetSecret(x["username"].ToString());

                    // Combine the username and accessKey with a colon
                    string credentials = $"{username}:{accessKey}";

                    // Encode the credentials to Base64
                    string base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

                    string apiUrl = $"https://api.lambdatest.com/automation/api/v1/sessions/{sessionId}";
                    string authorization = $"Basic {base64Credentials}";

                    var client = new RestClient(apiUrl);
                    var request = new RestRequest(apiUrl, Method.Get);
                    request.AddHeader("accept", "application/json");
                    request.AddHeader("Authorization", authorization);

                    var sessionResponse = client.Get(request);
                    var sessionJson = JObject.Parse(sessionResponse.Content);
                    var setExpiryLimitBuildPublicUrl = GetExpiryLimit30Url(sessionJson, authorization);

                    e.TestReporter.Log(new CreateLogItemRequest
                    {
                        Level = LogLevel.Fatal,
                        Time = DateTime.UtcNow,
                        Text = $"LambdaTest Video URL = {setExpiryLimitBuildPublicUrl}"
                    });

                    var buildName = Environment.GetEnvironmentVariable("buildName");
                    string lambdaTestBuildUrl = $"https://automation.lambdatest.com/build?&searchText={buildName}";

                    e.TestReporter.Log(new CreateLogItemRequest
                    {
                        Level = LogLevel.Error,
                        Time = DateTime.UtcNow,
                        Text = $"LambdaTest Build URL = {lambdaTestBuildUrl}"
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    private async Task<string> GetExpiryLimit30Url(JObject response, string authorization)
    {
        var buildId = response["data"]["build_id"].ToString();

        var expiryLimitBuildUrl = $"https://api.lambdatest.com/automation/api/v1/builds/{buildId}?shareExpiryLimit=30";

        var client = new RestClient(expiryLimitBuildUrl);
        var request = new RestRequest(expiryLimitBuildUrl, Method.Get);
        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", authorization);

        var setExpiryLimitBuildResponse = await client.ExecuteAsync(request);

        var setExpiryLimitBuildJson = JObject.Parse(setExpiryLimitBuildResponse.Content);
        var setExpiryLimitBuildPublicUrl = setExpiryLimitBuildJson["data"]["public_url"].ToString();

        return setExpiryLimitBuildPublicUrl;
    }

    public void OnTestEvent(string report)
    {
    }
}
