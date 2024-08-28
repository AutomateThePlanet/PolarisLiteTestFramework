using AngleSharp.Dom;
using NUnit.Framework;
using PolarisLite.Integrations;
using PolarisLite.Integrations.LambdaTestAPI;
using PolarisLite.Web.Integrations;
using PolarisLite.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarisLite.Web.Plugins.Performance;
public static class PerformanceService
{
    public static List<SeleniumHarLogResponseData> GetSeleniumHarLogs()
    {
        var driverAdapter = new DriverAdapter();
        var driver = DriverFactory.WrappedDriver;
        bool isLambdaTestRun = DriverFactory.ExecutionType.Equals(ExecutionType.LambdaTest);

        if (isLambdaTestRun)
        {
            var sessionId = DriverFactory.CurrentSessionId;
            var sessionApiClient = new SessionApiClient();
            var response = sessionApiClient.SessionHarLogAsync(sessionId).Result;
            return response.Data.Data;
        }
        else
        {
            throw new NotSupportedException("not supported for local execution");
        }
    }

    public static void AssertResponse404ErrorCodeRecievedByPartialUrl(string partialUrl)
    {
        var driverAdapter = new DriverAdapter();
        var responseStatusCode = driverAdapter.ResponsesHistory.ToList().Find(r => r.ResponseUrl.Contains(partialUrl)).ResponseStatusCode;
        Assert.That(responseStatusCode, Is.EqualTo(404), "404 Error code not detected on the page.");
    }

    public static void AssertNoErrorCodes()
    {
        var driverAdapter = new DriverAdapter();
        bool hasErrorCode = driverAdapter.ResponsesHistory.Any(r => r.ResponseStatusCode > 400 && r.ResponseStatusCode < 599);
        Assert.That(hasErrorCode, Is.False, "Error codes detected on the page.");
    }

    public static void AssertRequestMade(string url)
    {
        var driverAdapter = new DriverAdapter();
        bool isRequestMade = driverAdapter.ResponsesHistory.Any(r => r.ResponseUrl.Contains(url));
        Assert.That(isRequestMade, Is.True, $"Request {url} was not made.");
    }

    public static void AssertRequestNotMade(string url)
    {
        var driverAdapter = new DriverAdapter();
        bool areRequestsMade = driverAdapter.ResponsesHistory.Any(r => r.ResponseUrl.Contains(url));
        Assert.That(areRequestsMade, Is.False, $"Request {url} was made.");
    }
}
