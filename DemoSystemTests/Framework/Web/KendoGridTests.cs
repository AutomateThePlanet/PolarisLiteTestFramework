using DemoSystemTests.Framework.Web.Pages.Models;
using PolarisLite.Web;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Plugins;

namespace DemoSystemTests.Web;

[TestFixture]
[LocalExecution(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
public class KendoGridTests : WebTest
{
    private KendoGrid _kendoGrid;

    protected override void TestInitialize()
    {
        App.Navigation.GoToUrl("https://demos.telerik.com/kendo-ui/grid/basic-usage");
        var consentButton = App.Elements.FindById<Button>("onetrust-accept-btn-handler");
        consentButton.Click();

        _kendoGrid = App.Elements.FindById<KendoGrid>("grid");
    }

    [Test]
    public void FilterContactName()
    {
        _kendoGrid.Filter("ContactName", FilterOperator.Contains, "Thomas");
        var items = _kendoGrid.GetItems<GridItem>();

        Assert.That(items.Count, Is.EqualTo(1));
    }

    [Test]
    public void SortContactTitleDesc()
    {
        _kendoGrid.Sort("ContactTitle", SortType.Desc);
        var items = _kendoGrid.GetItems<GridItem>();

        Assert.That(items[0].ContactTitle, Is.EqualTo("Sales Representative"));
        Assert.That(items[1].ContactTitle, Is.EqualTo("Sales Representative"));
    }

    [Test]
    public void TestCurrentPage()
    {
        var pageNumber = _kendoGrid.GetCurrentPageNumber();

        Assert.That(pageNumber, Is.EqualTo(1));
    }

    [Test]
    public void GetPageSize()
    {
        var pageNumber = _kendoGrid.GetPageSize();

        Assert.That(pageNumber, Is.EqualTo(20));
    }

    [Test]
    public void GetAllItems()
    {
        var items = _kendoGrid.GetItems<GridItem>();

        Assert.That(items.Count, Is.EqualTo(20));
    }
}
