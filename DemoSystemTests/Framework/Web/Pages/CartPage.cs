using PolarisLite.Web;

namespace DemoSystemTests.Framework.Web.Pages;
public class CartPage : WebPage
{
    public Button ViewCartButton => App.Elements.FindByXPath<Button>("//a[normalize-space(.)='View Cart']");
    public Button CheckoutButton => App.Elements.FindAllByXPath<Button>("//a[normalize-space(.)='Checkout']").Last();
    public List<Div> CartItems => App.Elements.FindAllByCss<Div>("div.cart-item");
    public Div TotalPrice => App.Elements.FindAllByXPath<Div>("//td[text()='Total:']/following-sibling::td/strong").Last();

    public void ViewCart()
    {
        ViewCartButton.Click();
    }

    public void Checkout()
    {
        CheckoutButton.Click();
    }

    public void UpdateQuantity(int itemIndex, int quantity)
    {
        var quantityField = CartItems[itemIndex].FindByXPath<TextField>(".//input[@type='number']");
        //quantityField.Clear();
        quantityField.TypeText(quantity.ToString());
    }

    public void RemoveItem(int itemIndex)
    {
        var removeButton = CartItems[itemIndex].FindByXPath<Button>(".//button[@title='Remove']");
        removeButton.Click();
    }

    public void AssertTotalPrice(string expectedPrice)
    {
        Assert.That(TotalPrice.Text, Is.EqualTo(expectedPrice));
    }
}
