using PolarisLite.Web;

namespace DemoSystemTests.Integrations.AutoHealing.Pages;

public class LocalPage : WebPage
{
    public TextField FirstName => App.Elements.FindById<TextField>("firstName");
    public TextField LastName => App.Elements.FindById<TextField>("lastName");
    public TextField Username => App.Elements.FindById<TextField>("username");
    public TextField Email => App.Elements.FindById<TextField>("email");
    public TextField Address1 => App.Elements.FindById<TextField>("address");
    public TextField Address2 => App.Elements.FindById<TextField>("address2");
    public Select Country => App.Elements.FindById<Select>("country");
    public Select State => App.Elements.FindById<Select>("state");
    public TextField Zip => App.Elements.FindById<TextField>("zip");
    public TextField CardName => App.Elements.FindById<TextField>("ccName");
    public TextField CardNumber => App.Elements.FindById<TextField>("ccNumber");
    public TextField CardExpiration => App.Elements.FindById<TextField>("ccExpiration");
    public TextField CardCVV => App.Elements.FindById<TextField>("ccCVV");
    public Button SubmitButton => App.Elements.FindByXPath<Button>("//button[text()='Proceed to checkout']");
    public TextField PromoCode => App.Elements.FindByXPath<TextField>("//input[@id='email']/ancestor::form/parent::div/preceding-sibling::div/form/div/input");

    public void Navigate()
    {
        //string relativePath = "Resources\\checkout\\index.html";
        //string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //string fullPath = Path.Combine(baseDirectory, relativePath);
        //string fileUrl = new Uri(fullPath).AbsoluteUri;


        App.Navigation.GoToUrl("https://chesstv.local:3000/checkout/index.html");
        //App.Cookies.AddCookie("abuse_interstitial", "e44f-5-53-134-19.ngrok-free.app");
        //App.Browser.Refresh();
    }

    public void FillInfo(ClientInfo clientInfo)
    {
        FirstName.TypeText(clientInfo.FirstName);
        LastName.TypeText(clientInfo.LastName);
        Username.TypeText(clientInfo.Username);
        Email.TypeText(clientInfo.Email);
        Address1.TypeText(clientInfo.Address1);
        Address2.TypeText(clientInfo.Address2);
        Country.SelectByIndex(clientInfo.Country);
        State.SelectByIndex(clientInfo.State);
        Zip.TypeText(clientInfo.Zip);
        CardName.TypeText(clientInfo.CardName);
        CardNumber.TypeText(clientInfo.CardNumber);
        CardExpiration.TypeText(clientInfo.CardExpiration);
        CardCVV.TypeText(clientInfo.CardCVV);

        PromoCode.TypeText("TEST");

        SubmitButton.Click();
    }

    public void VerifyFormSent()
    {
        Assert.That(App.Navigation.Url.Contains("paymentMethod=on"), "Form not sent");
    }
}