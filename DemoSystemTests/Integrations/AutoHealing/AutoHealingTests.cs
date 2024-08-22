using DemoSystemTests.Integrations.AutoHealing.Pages;
using PolarisLite.Web;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Plugins;

namespace DemoSystemTests.Integrations.AutoHealing;

[TestFixture]
[LambdaTest(BrowserType.Chrome, enableAutoHealing: true, useTunnel: true)]
//[LocalExecution(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
public class AutoHealingTests : WebTest
{
    public LocalPage LocalPage { get; private set; }

    protected override void TestInitialize()
    {
        LocalPage = App.Create<LocalPage>();
    }

    [Test]
    public void AssertFormSent_When_ValidInfoInput()
    {
        var localPage = new LocalPage();
        localPage.Navigate();

        var clientInfo = new ClientInfo
        {
            FirstName = "Anton",
            LastName = "Angelov",
            Username = "aangelov",
            Email = "info@berlinspaceflowers.com",
            Address1 = "1 Willi Brandt Avenue Tiergarten",
            Address2 = "Lützowplatz 17",
            Country = 1,
            State = 1,
            Zip = "10115",
            CardName = "Anton Angelov",
            CardNumber = "1234567890123456",
            CardExpiration = "12/23",
            CardCVV = "123"
        };

        localPage.FillInfo(clientInfo);

        localPage.VerifyFormSent();
    }
}