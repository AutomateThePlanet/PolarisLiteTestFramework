﻿using PolarisLite.API;
using RestSharp.Authenticators;

namespace PolarisLite.Integrations.LambdaTestAPI;
public class LambdaTestApiClient
{
    protected readonly ApiClientAdapter _apiClientService;

    public LambdaTestApiClient(ApiClientAdapter apiClientService = null)
    {
        var userName = Environment.GetEnvironmentVariable("LT_USERNAME", EnvironmentVariableTarget.Machine);
        var accessKey = Environment.GetEnvironmentVariable("LT_ACCESSKEY", EnvironmentVariableTarget.Machine);
        _apiClientService = apiClientService ?? new ApiClientAdapter("https://api.lambdatest.com/automation/api/v1/", authenticator: new HttpBasicAuthenticator(userName, accessKey)); ;
    }
}
