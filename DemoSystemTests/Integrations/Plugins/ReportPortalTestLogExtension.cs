//using ReportPortal.Client.Abstractions.Models;
//using ReportPortal.Client.Abstractions.Requests;
//using ReportPortal.NUnitExtension;
//using LogLevel = ReportPortal.Client.Abstractions.Models.LogLevel;
//using NUnit.Engine;
//using PolarisLite.Integrations.Settings;
//using PolarisLite.Logging;
//using PolarisLite.Web.Plugins;

//namespace DemoSystemTests.Integrations.Plugins;

//[NUnit.Engine.Extensibility.Extension]
//public class ReportPortalTestLogExtension : ITestEventListener
//{
//    private readonly SessionApiClient _sessionApiClient;
//    private readonly BuildApiClient _buildApiClient;

//    public ReportPortalTestLogExtension()
//    {
//        try
//        {
//            if (IntegrationSettings.ReportPortalEnabled)
//            {
//                _sessionApiClient = new SessionApiClient();
//                _buildApiClient = new BuildApiClient();

//                ReportPortalListener.BeforeRunStarted += ReportPortalListener_BeforeRunStarted;
//                ReportPortalListener.BeforeTestFinished += ReportPortalListener_AfterTestFinished;
//            }
//        }
//        catch (Exception ex)
//        {
//            Logger.LogError(ex.ToString());
//        }
//    }

//    private void ReportPortalListener_BeforeRunStarted(object sender, ReportPortal.NUnitExtension.EventArguments.RunStartedEventArgs e)
//    {

//        var testCategory = System.Environment.GetEnvironmentVariable("TEST_CATEGORY", EnvironmentVariableTarget.Process);
//        var runEnvironment = System.Environment.GetEnvironmentVariable("RUN_ENVIRONMENT", EnvironmentVariableTarget.Process);
//        var releaseName = System.Environment.GetEnvironmentVariable("RELEASE_NAME", EnvironmentVariableTarget.Process);
//        var buildName = System.Environment.GetEnvironmentVariable("BUILD_NAME", EnvironmentVariableTarget.Process);

//        e.StartLaunchRequest.Attributes.Clear();
//        if (testCategory != null)
//        {
//            e.StartLaunchRequest.Attributes.Add(new ItemAttribute { Key = "suite", Value = testCategory });
//        }

//        if (runEnvironment != null)
//        {
//            e.StartLaunchRequest.Attributes.Add(new ItemAttribute { Key = "instance", Value = runEnvironment });
//        }

//        if (releaseName != null)
//        {
//            e.StartLaunchRequest.Attributes.Add(new ItemAttribute { Key = "release", Value = releaseName });
//        }

//        if (buildName != null)
//        {
//            e.StartLaunchRequest.Attributes.Add(new ItemAttribute { Key = "build", Value = buildName });

//            string lambdaTestBuildUrl = $"https://automation.lambdatest.com/build?&searchText={buildName}";

//            e.StartLaunchRequest.Description = System.Environment.NewLine + lambdaTestBuildUrl;
//        }
//    }

//    private void ReportPortalListener_AfterTestFinished(object sender, ReportPortal.NUnitExtension.EventArguments.TestItemFinishedEventArgs e)
//    {
//        if (IntegrationSettings.ReportPortalEnabled)
//        {
//            try
//            {
//                bool isLambdaTestRun = DriverFactory.ExecutionType.Equals(ExecutionType.LambdaTest);
//                if (isLambdaTestRun)
//                {
//                    var sessionId = DriverFactory.CurrentSessionId;

//                    var sessionDetailsResponse = _sessionApiClient.GetSessionDetailsAsync(sessionId, "30").Result;

//                    if (sessionDetailsResponse != null)
//                    {
//                        var setExpiryLimitBuildPublicUrl = GetExpiryLimit30Url(sessionDetailsResponse.Data.Data.BuildId.ToString());

//                        e.TestReporter.Log(new CreateLogItemRequest
//                        {
//                            Level = LogLevel.Debug,
//                            Time = DateTime.UtcNow,
//                            Text = $"LambdaTest Video URL = {setExpiryLimitBuildPublicUrl}&testID={sessionDetailsResponse.Data.Data.TestId}"
//                        });

//                        var buildName = DriverFactory.GridSettings.BuildName;
//                        string lambdaTestBuildUrl = $"https://automation.lambdatest.com/build?&searchText={buildName}";

//                        e.TestReporter.Log(new CreateLogItemRequest
//                        {
//                            Level = LogLevel.Debug,
//                            Time = DateTime.UtcNow,
//                            Text = $"LambdaTest Build URL = {lambdaTestBuildUrl}"
//                        });

//                        // Azure DevOps Build ID Logging
//                        var azureBuildId = System.Environment.GetEnvironmentVariable("AZURE_DEVOPS_BUILD_ID", EnvironmentVariableTarget.Process);
//                        if (!string.IsNullOrEmpty(azureBuildId))
//                        {
//                            Console.WriteLine($"Azure Build Id -> {azureBuildId}");
//                            string azureBuildUrl = $"{IntegrationSettings.AzureDevOpsBuildUrl}{azureBuildId}";

//                            e.TestReporter.Log(new CreateLogItemRequest
//                            {
//                                Level = LogLevel.Debug,
//                                Time = DateTime.UtcNow,
//                                Text = $"DevOps Build URL = {azureBuildUrl}"
//                            });
//                        }

//                        if (e.FinishTestItemRequest.Attributes == null)
//                        {
//                            e.FinishTestItemRequest.Attributes = new List<ItemAttribute>();
//                        }

//                        e.FinishTestItemRequest.Description = lambdaTestBuildUrl;
//                        e.FinishTestItemRequest.Attributes.Add(new ItemAttribute { Key = "build", Value = buildName });
//                        e.TestReporter.Finish(e.FinishTestItemRequest);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Logger.LogError(ex.ToString());
//            }
//        }
//    }

//    private string GetExpiryLimit30Url(string buildId)
//    {
//        var buildResponse = _buildApiClient.SinglebuildAsync(int.Parse(buildId), "30").Result;
//        if (buildResponse != null)
//        {
//            return buildResponse.Data.Data.PublicUrl;
//        }
//        return string.Empty;
//    }

//    public void OnTestEvent(string report)
//    {
//    }
//}
