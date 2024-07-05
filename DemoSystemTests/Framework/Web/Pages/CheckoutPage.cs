using DemoSystemTests.Framework.Web.Pages.Models;
using PolarisLite.Web;

namespace DemoSystemTests.Framework.Web.Pages;
public class CheckoutPage : WebPage
{
    public TextField FirstNameInput => App.Elements.FindById<TextField>("input-payment-firstname");
    public TextField LastNameInput => App.Elements.FindById<TextField>("input-payment-lastname");
    public Email EmailInput => App.Elements.FindById<Email>("input-payment-email");
    public TextField TelephoneInput => App.Elements.FindById<TextField>("input-payment-telephone");
    public Password PasswordInput => App.Elements.FindById<Password>("input-payment-password");
    public TextField ConfirmPasswordInput => App.Elements.FindById<TextField>("input-payment-confirm");
    public TextField CompanyInput => App.Elements.FindById<TextField>("input-payment-company");
    public TextField Address1Input => App.Elements.FindById<TextField>("input-payment-address-1");
    public TextField Address2Input => App.Elements.FindById<TextField>("input-payment-address-2");
    public TextField CityInput => App.Elements.FindById<TextField>("input-payment-city");
    public TextField PostCodeInput => App.Elements.FindById<TextField>("input-payment-postcode");
    public Select ShippingAddressCountrySelect => App.Elements.FindById<Select>("input-payment-country");
    public Button ShippingAddressCountryOption(string country) =>
        ShippingAddressCountrySelect.FindByXPath<Button>($".//option[contains(text(), '{country}')]");
    public Select BillingAddressRegionSelect => App.Elements.FindById<Select>("input-payment-zone");
    public Button BillingAddressRegionOption(string region) =>
        BillingAddressRegionSelect.FindByXPath<Button>($".//option[contains(text(), '{region}')]");
    public CheckBox TermsAgreeCheckbox => App.Elements.FindByXPath<CheckBox>("//input[@id='input-agree']//following-sibling::label");
    public Button ContinueButton => App.Elements.FindByXPath<Button>("//button[@id='button-save']");
    public Label TotalPrice => App.Elements.FindAllByXPath<Label>("//td[text()='Total:']/following-sibling::td/strong").Last();

    public void FillUserDetails(UserDetails userDetails)
    {
        FirstNameInput.TypeText(userDetails.FirstName);
        LastNameInput.TypeText(userDetails.LastName);
        EmailInput.SetEmail(userDetails.Email);
        TelephoneInput.TypeText(userDetails.Telephone);
        PasswordInput.SetPassword(userDetails.Password);
        ConfirmPasswordInput.TypeText(userDetails.ConfirmPassword);
    }

    public void FillBillingAddress(BillingAddress billingAddress)
    {
        CompanyInput.TypeText(billingAddress.Company);
        Address1Input.TypeText(billingAddress.Address1);
        Address2Input.TypeText(billingAddress.Address2);
        CityInput.TypeText(billingAddress.City);
        PostCodeInput.TypeText(billingAddress.PostCode);
        ShippingAddressCountrySelect.Click();
        ShippingAddressCountryOption(billingAddress.Country).Click();
        App.Browser.WaitForAjax();
        BillingAddressRegionSelect.Click();
        BillingAddressRegionOption(billingAddress.Region).Click();
    }

    public void AgreeToTerms()
    {
        App.Browser.WaitForAjax();
        // TODO: Move to separate method in decorator
        App.JavaScript.Execute("arguments[0].scrollIntoView(true);", TermsAgreeCheckbox);
        TermsAgreeCheckbox.Check(true);
    }

    public void ClickContinue()
    {
        ContinueButton.Click();
    }

    public void AssertTotalPrice(string expectedPrice)
    {
        Assert.That(TotalPrice.Text, Is.EqualTo(expectedPrice));
    }

    public void CompleteCheckout()
    {
        var continueButton = App.Elements.FindByXPath<Button>("//button[@id='button-save']");
        continueButton.Click();
    }
}
