using DemoSystemTests.AutoHealing.Pages;
using PolarisLite.Web;

namespace DemoSystemTests.Framework.Web.Pages;

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
    public TextField CardName => App.Elements.FindById<TextField>("cc-name");
    public TextField CardNumber => App.Elements.FindById<TextField>("cc-number");
    public TextField CardExpiration => App.Elements.FindById<TextField>("cc-expiration");
    public TextField CardCVV => App.Elements.FindById<TextField>("cc-cvv");
    public Button SubmitButton => App.Elements.FindByXPath<Button>("//button[text()='Continue to checkout']");
    public TextField PromoCode => App.Elements.FindByXPath<TextField>("//input[@id='email']/ancestor::form/parent::div/preceding-sibling::div/form/div/input");

    public void Navigate()
    {
        App.Navigation.GoToUrl("https://e44f-5-53-134-19.ngrok-free.app/checkout/");
        App.Cookies.AddCookie("abuse_interstitial", "e44f-5-53-134-19.ngrok-free.app");
        App.Browser.Refresh();
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