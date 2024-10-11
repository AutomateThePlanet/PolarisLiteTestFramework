using PolarisLite.Core;
using PolarisLite.Web.Plugins;
using PolarisLite.Web;
using PolarisLite.Web.Assertions;
using PolarisLite.Web.Core.NUnit;

namespace DemoSystemTests;

[LambdaTest]
[Settings(Settings.EnablePromotions, true)]
[Settings(Settings.Currency, CurrencyType.USD)]
public class ToDoTestsWithSettings : WebTest
{
    private ISettingsService _settingsService;

    protected override void Configure()
    {
        PluginExecutionEngine.AddPlugin(new SettingsPlugin());
        base.Configure();
    }

    protected override void TestInitialize()
    {
        _settingsService = new MockSettingsService();
    }

    [Test]
    public void TestWithDarkModeEnabled()
    {
        // Set test-specific setting
        _settingsService.SetSetting(Settings.EnableDarkMode, true);
        _settingsService.SetSetting(Settings.MaxItemsPerPage, 20);

        // Print settings to BDD logs
        PrintSettingsToLog();

        // Simulate web actions using the settings
        App.Navigation.GoToUrl("https://todomvc.com/");
        OpenTechnologyApp("Backbone.js");
        AddNewToDoItem("Test with dark mode");

        // Verify settings (for demo purposes, pretend this affects test behavior)
        Assert.That(_settingsService.GetSetting<bool>(Settings.EnableDarkMode), Is.True);
    }

    [Test]
    public void TestWithCurrencyEUR()
    {
        // Set test-specific setting
        _settingsService.SetSetting(Settings.Currency, CurrencyType.EUR);

        // Print settings to BDD logs
        PrintSettingsToLog();

        // Simulate web actions using the settings
        App.Navigation.GoToUrl("https://todomvc.com/");
        OpenTechnologyApp("Angular");
        AddNewToDoItem("Test with currency EUR");

        Assert.That(_settingsService.GetSetting<CurrencyType>(Settings.Currency), Is.EqualTo(CurrencyType.EUR));
    }

    protected override void TestCleanup()
    {
        // Restore default settings after test
        _settingsService.RestoreDefault(Settings.EnableDarkMode);
        _settingsService.RestoreDefault(Settings.Currency);
        _settingsService.RestoreDefault(Settings.MaxItemsPerPage);
    }

    private void PrintSettingsToLog()
    {
        var settings = _settingsService.GetAllSettings();
        foreach (var setting in settings)
        {
            Logger.LogInfo($"Setting {setting.Key}: {setting.Value}");
        }
    }

    // Utility methods
    private void OpenTechnologyApp(string technologyName)
    {
        var technologyLink = App.Elements.FindByXPath<Anchor>($"//span[text()='{technologyName}']");
        technologyLink.Click();
    }

    private void AddNewToDoItem(string todoItem)
    {
        var todoInput = App.Elements.FindByXPath<TextField>("//input[@placeholder='What needs to be done?']");
        todoInput.TypeText(todoItem);
        App.Interactions.Click(todoInput).SendKeys(Keys.Enter).Perform();
    }

    private CheckBox GetItemCheckbox(string todoItem)
    {
        var xpathLocator = $"//label[text()='{todoItem}']/preceding-sibling::input";
        return App.Elements.FindByXPath<CheckBox>(xpathLocator);
    }

    private void AssertLeftItems(int expectedCount)
    {
        var resultSpan = App.Elements.FindByXPath<PolarisLite.Web.Label>("//footer/*/span | //footer/span");
        var expectedText = expectedCount == 1 ? $"{expectedCount} item left" : $"{expectedCount} items left";
        resultSpan.ValidateInnerTextContains(expectedText);
    }

    private int GetTodoListSize()
    {
        var items = App.Elements.FindAllByXPath<CheckBox>("//ul[@class='todo-list']/li");
        return items.Count;
    }

    private void DeleteCompletedItems()
    {
        var deleteButton = App.Elements.FindByXPath<Button>("//button[@class='clear-completed']");
        deleteButton.Click();
    }

    private void ClearCompletedTasks()
    {
        var clearButton = App.Elements.FindByXPath<Button>("//button[@class='clear-completed']");
        clearButton.Click();
    }

    private void MarkAllItemsAsComplete()
    {
        var toggleAll = App.Elements.FindByXPath<CheckBox>("//input[@class='toggle-all']");
        toggleAll.Check();
    }
}
