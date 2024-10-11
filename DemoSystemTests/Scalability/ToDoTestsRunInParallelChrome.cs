using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web;
using PolarisLite.Web.Assertions;
using PolarisLite.Web.Plugins;
using PolarisLite.Web.Plugins.Performance;
using Polaris.Plugins.Common.ExecutionTime;

[assembly: Parallelizable(ParallelScope.Fixtures)]
//[assembly: LevelOfParallelism(5)]

namespace DemoSystemTests.Web.Scalability;

[TestFixture]
[LambdaTest(BrowserType.Chrome)]
public class ToDoTestsRunInParallelChrome : WebTest
{
    [Test]
    public void VerifyToDoList_BackboneJS()
    {
        App.Navigation.GoToUrl("https://todomvc.com/");
        OpenTechnologyApp("Backbone.js");

        AddNewToDoItem("Buy groceries");
        AddNewToDoItem("Walk the dog");
        AddNewToDoItem("Read a book");

        GetItemCheckbox("Walk the dog").Check();
        AssertLeftItems(2);
    }

    [Test]
    public void VerifyToDoList_Angular()
    {
        App.Navigation.GoToUrl("https://todomvc.com/");
        OpenTechnologyApp("Angular");

        AddNewToDoItem("Buy a laptop");
        AddNewToDoItem("Prepare for meeting");
        AddNewToDoItem("Order food");

        GetItemCheckbox("Order food").Check();
        AssertLeftItems(2);
    }

    [Test]
    public void VerifyToDoList_React()
    {
        App.Navigation.GoToUrl("https://todomvc.com/");
        OpenTechnologyApp("React");

        AddNewToDoItem("Finish project");
        AddNewToDoItem("Submit report");
        AddNewToDoItem("Watch movie");

        GetItemCheckbox("Submit report").Check();
        AssertLeftItems(2);

        // Additional test: Uncheck an item and verify count
        GetItemCheckbox("Submit report").Uncheck();
        AssertLeftItems(3);
    }

    [Test]
    public void VerifyToDoList_VueJS()
    {
        App.Navigation.GoToUrl("https://todomvc.com/");
        OpenTechnologyApp("Vue.js");

        AddNewToDoItem("Practice coding");
        AddNewToDoItem("Clean desk");
        AddNewToDoItem("Schedule meeting");

        GetItemCheckbox("Clean desk").Check();
        AssertLeftItems(2);

        // Verify that completed tasks can be deleted
        DeleteCompletedItems();
        AssertLeftItems(2); // No change in item count since only completed items were removed
    }

    [Test]
    public void VerifyToDoList_EmberJS()
    {
        App.Navigation.GoToUrl("https://todomvc.com/");
        OpenTechnologyApp("Ember.js");

        AddNewToDoItem("Fix the car");
        AddNewToDoItem("Wash dishes");
        AddNewToDoItem("Call the plumber");

        GetItemCheckbox("Fix the car").Check();
        GetItemCheckbox("Call the plumber").Check();
        AssertLeftItems(1);

        // Verify clearing all completed tasks
        ClearCompletedTasks();
        AssertLeftItems(1); // Only one item remains
    }

    [Test]
    public void VerifyToDoList_MithrilJS()
    {
        App.Navigation.GoToUrl("https://todomvc.com/");
        OpenTechnologyApp("Mithril");

        AddNewToDoItem("Do the laundry");
        AddNewToDoItem("Finish homework");
        AddNewToDoItem("Read a novel");

        GetItemCheckbox("Finish homework").Check();
        AssertLeftItems(2);

        // Verify all items can be marked as completed
        MarkAllItemsAsComplete();
        AssertLeftItems(0); // All items completed
    }

    [Test]
    public void VerifyToDoList_Polymer()
    {
        App.Navigation.GoToUrl("https://todomvc.com/");
        OpenTechnologyApp("Polymer");

        AddNewToDoItem("Write tests");
        AddNewToDoItem("Review pull requests");
        AddNewToDoItem("Fix bugs");

        GetItemCheckbox("Fix bugs").Check();
        AssertLeftItems(2);

        // Verify re-opening a completed item
        GetItemCheckbox("Fix bugs").Uncheck();
        AssertLeftItems(3); // One item is reopened
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