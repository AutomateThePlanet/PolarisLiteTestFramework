using PolarisLite.API;
using RestSharp;

namespace DemoSystemTests.Integrations.Authentication.Services;

using RestSharp;
using System.Threading.Tasks;

public static class AuthBypassService
{
    public static ApiClientAdapter ApiClientService { get; set; } = new ApiClientAdapter("http://chesstv.local:3000/");

    public static async Task<string> GenerateAuthCookieAsync(string displayName, string password, string userid)
    {
        var request = new RestRequest("generate-auth-cookie", Method.Post);
        request.AddJsonBody(new
        {
            displayName,
            password,
            userid
        });

        var response = await ApiClientService.PostAsync(request);

        var authCookie = response.Response.Cookies?.FirstOrDefault(c => c.Name == "auth");
        return authCookie?.Value ?? throw new ApplicationException("Failed to generate auth cookie.");
    }

    public static async Task<string> Generate2FATokenAsync(int userId)
    {
        var request = new RestRequest($"2fa/generate-token/{userId}", Method.Get);

        var response = await ApiClientService.GetAsync(request);

        return response.Response.Content ?? throw new ApplicationException("Failed to generate 2FA token.");
    }
}


