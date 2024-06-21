using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace DemoSystemTests.Allure;

[TestFixture]
[Parallelizable(ParallelScope.Children)]
public class RunTestsInSelenoid
{
    private IWebDriver _driver;

    [SetUp]
    public void SetupTest()
    {
        var driverOptions = new ChromeOptions();

        var runName = GetType().Assembly.GetName().Name;
        var timestamp = $"{DateTime.Now:yyyyMMdd.HHmm}";

        driverOptions.AddAdditionalOption("name", runName);
        driverOptions.AddAdditionalOption("videoName", $"{runName}.{timestamp}.mp4");
        driverOptions.AddAdditionalOption("logName", $"{runName}.{timestamp}.log");
        driverOptions.AddAdditionalOption("enableVNC", true);
        driverOptions.AddAdditionalOption("enableVideo", true);
        driverOptions.AddAdditionalOption("enableLog", true);
        driverOptions.AddAdditionalOption("screenResolution", "1920x1080x24");

        _driver = new RemoteWebDriver(new Uri("http://127.0.0.1:4444/wd/hub"), driverOptions);
        _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);

        _driver.Navigate().GoToUrl("https://tinyurl.com/ycxx74ve");
    }

    [TearDown]
    public void TeardownTest()
    {
        _driver.Dispose();
    }

    [Test]
    public void FillAllTextFields()
    {
        var textBoxes = _driver.FindElements(By.Name("fname"));
        foreach (var textBox in textBoxes)
        {
            textBox.SendKeys(Guid.NewGuid().ToString());
        }
    }

    [Test]
    public void FillAllSelects()
    {
        var selects = _driver.FindElements(By.TagName("select"));
        foreach (var select in selects)
        {
            var selectElement = new SelectElement(select);
            selectElement.SelectByText("Mercedes");
        }
    }

    [Test]
    public void FillAllColors()
    {
        var colors = _driver.FindElements(By.XPath("//input[@type='color']"));
        foreach (var color in colors)
        {
            SetValueAttribute(_driver, color, "#000000");
        }
    }

    [Test]
    public void SetAllDates()
    {
        var dates = _driver.FindElements(By.XPath("//input[@type='date']"));
        foreach (var date in dates)
        {
            SetValueAttribute(_driver, date, "2020-06-01");
        }
    }

    [Test]
    public void ClickAllRadioButtons()
    {
        var radioButtons = _driver.FindElements(By.XPath("//input[@type='radio']"));
        foreach (var radio in radioButtons)
        {
            radio.Click();
        }
    }

    private void SetValueAttribute(IWebDriver driver, IWebElement element, string value)
    {
        SetAttribute(driver, element, "value", value);
    }

    private void SetAttribute(IWebDriver driver, IWebElement element, string attributeName, string attributeValue)
    {
        driver.ExecuteJavaScript($"arguments[0].setAttribute({attributeName}, {attributeValue});", element);
    }
}