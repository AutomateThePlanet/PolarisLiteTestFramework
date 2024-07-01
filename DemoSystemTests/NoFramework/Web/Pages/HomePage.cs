namespace DemoSystemTests.NoFramework;
public class HomePage : WebPage
{
    public HomePage(IWebDriver driver, WebDriverWait wait, Actions actions) 
        : base(driver, wait, actions)
    {
    }

    public IWebElement SearchInput => _driver.FindElement(By.XPath("//input[@name='search']"));

    public void SearchProduct(string searchText)
    {
        SearchInput.Clear();
        SearchInput.SendKeys(searchText);
        //SearchInput.SendKeys(Keys.Enter);
    }
}
