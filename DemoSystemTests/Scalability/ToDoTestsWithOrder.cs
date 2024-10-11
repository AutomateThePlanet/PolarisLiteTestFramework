using PolarisLite.Web.Plugins;
using PolarisLite.Web;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Assertions;

namespace DemoSystemTests.Scalability;

[TestFixture]
[LambdaTest(BrowserType.Chrome)]
public class ToDoTestsWithOrder : WebTest
{
    // Test to add TODO items (executed first)
    [Test, Order(1)]
    public void AddToDoItems()
    {
        App.Navigation.GoToUrl("https://todomvc.com/");
        OpenTechnologyApp("React");

        AddNewToDoItem("Buy milk");
        AddNewToDoItem("Walk the dog");
        AddNewToDoItem("Finish the report");

        AssertLeftItems(3); // Verify 3 items are added
    }

    // Test to verify TODO items (depends on AddToDoItems)
    [Test, Order(2)]
    public void VerifyToDoItems()
    {
        App.Navigation.GoToUrl("https://todomvc.com/");
        OpenTechnologyApp("React");

        // Assumes items were added in the AddToDoItems test
        AssertLeftItems(3); // Verifies that 3 items are still left
    }

    // Test to delete TODO items (depends on VerifyToDoItems)
    [Test, Order(3)]
    public void DeleteToDoItems()
    {
        App.Navigation.GoToUrl("https://todomvc.com/");
        OpenTechnologyApp("React");

        // Assumes that items from the previous tests exist
        DeleteCompletedItems();
        AssertLeftItems(0); // Verifies all items are cleared
    }

    // Utility methods (same as before)
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

    private void AssertLeftItems(int expectedCount)
    {
        var resultSpan = App.Elements.FindByXPath<PolarisLite.Web.Label>("//footer/*/span | //footer/span");
        var expectedText = expectedCount == 1 ? $"{expectedCount} item left" : $"{expectedCount} items left";
        resultSpan.ValidateInnerTextContains(expectedText);
    }

    private void DeleteCompletedItems()
    {
        var deleteButton = App.Elements.FindByXPath<Button>("//button[@class='clear-completed']");
        deleteButton.Click();
    }
}

