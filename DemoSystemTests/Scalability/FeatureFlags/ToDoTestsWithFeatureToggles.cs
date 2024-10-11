using PolarisLite.Core;
using PolarisLite.Web.Plugins;
using PolarisLite.Web;
using PolarisLite.Web.Assertions;
using PolarisLite.Web.Core.NUnit;
using DemoSystemTests.Scalability.FeatureFlags;
using PolarisLite.Web.Plugins.Performance;

namespace DemoSystemTests;

[LambdaTest]
[FeatureFlag(FeatureFlags.EnablePromotions, true)]
[FeatureFlag(FeatureFlags.EnableCountrySpecificFeatures, true)]
[Geolocation(GeoLocations.Finland)]
//[Geolocation(63.246778, 25.920916, 1)]
[TimeZone(TimeZones.Bratislava)]
[Locale(LanguageCodes.Finnish)]
public class ToDoTestsWithFeatureToggles : WebTest
{
    protected override void Configure()
    {
        PluginExecutionEngine.AddPlugin(new FeatureTogglePlugin());
        base.Configure();
    }

    [Test]
    public void TestWithDarkModeEnabled()
    {
        // Set test-specific feature flag
        FeatureFlagService.SetFeatureFlag(FeatureFlags.EnableDarkMode, true);

        // Print feature flags to BDD logs
        PrintFeatureFlagsToLog();

        // Simulate web actions using the feature flags
        App.Navigation.GoToUrl("https://todomvc.com/");
        OpenTechnologyApp("Backbone.js");
        AddNewToDoItem("Test with dark mode");

        // Verify feature flags (for demo purposes, pretend this affects test behavior)
        Assert.That(FeatureFlagService.GetFeatureFlag(FeatureFlags.EnableDarkMode), Is.True);
    }

    [Test]
    public void TestWithBetaFeaturesEnabled()
    {
        // Set test-specific feature flag
        FeatureFlagService.SetFeatureFlag(FeatureFlags.EnableBetaFeatures, true);

        // Print feature flags to BDD logs
        PrintFeatureFlagsToLog();

        // Simulate web actions using the feature flags
        App.Navigation.GoToUrl("https://todomvc.com/");
        OpenTechnologyApp("Angular");
        AddNewToDoItem("Test with beta features enabled");

        Assert.That(FeatureFlagService.GetFeatureFlag(FeatureFlags.EnableBetaFeatures), Is.True);
    }

    protected override void TestCleanup()
    {
        // Restore default feature flags after test
        FeatureFlagService.RestoreDefaultFeatureFlag(FeatureFlags.EnableDarkMode);
        FeatureFlagService.RestoreDefaultFeatureFlag(FeatureFlags.EnableBetaFeatures);
    }

    private void PrintFeatureFlagsToLog()
    {
        var featureFlags = FeatureFlagService.GetAllFeatureFlags();
        foreach (var flag in featureFlags)
        {
            Logger.LogInfo($"Feature Flag {flag.Key}: {flag.Value}");
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
