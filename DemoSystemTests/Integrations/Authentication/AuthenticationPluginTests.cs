using DemoSystemTests.Integrations.Authentication.Factories;
using DemoSystemTests.Integrations.Authentication.Services;
using DemoSystemTests.Integrations.Plugins.Auth;
using PolarisLite.API;
using PolarisLite.Core;
using PolarisLite.Web;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Plugins;

namespace DemoSystemTests.Integrations.Authentication;

[TestFixture]
[LocalExecution(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
//[LambdaTest(BrowserType.Chrome, enableAutoHealing: true, smartWait: 30)]
[Authentication(AuthType.EMAIL_PASSWORD_2FA, useNewUser: true)]
public class AuthenticationPluginTests : WebTest
{
    protected override void Configure()
    {
        base.Configure();
        PluginExecutionEngine.AddPlugin(new AuthenticationPlugin());
    }

    protected override void TestInitialize()
    {
        ApiSettings.BaseUrl = "https://chesstv.local:3000/";
        //TestUserFactory.ApiClient = ApiApp.ApiClient;
        //AuthBypassService.ApiClientService = ApiApp.ApiClient;

        App.Navigation.GoToUrl("https://chesstv.local:3000/profile");
    }

    [Test]
    public void LoginWithNewlyCreatedAccount()
    {
        var userName = App.Elements.FindById<Label>("username");
        Assert.That(userName.Text, Is.EqualTo(AuthenticationService.TestUser.Username));
    }
}
