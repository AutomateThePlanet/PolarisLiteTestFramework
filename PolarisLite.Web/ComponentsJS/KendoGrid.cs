using AngleSharp.Html;
using Newtonsoft.Json;
using PolarisLite.Web.Components;
using PolarisLite.Web.Services;
using System.Text;

namespace PolarisLite.Web;

public class KendoGrid : WebComponent
{
    private WebDriverWait _wait;

    public KendoGrid()
    {
        _wait = new WebDriverWait(WrappedDriver, TimeSpan.FromSeconds(30));
        _wait.IgnoreExceptionTypes(typeof(JavaScriptException));
    }

    public void RemoveFilters()
    {
        var jsToBeExecuted = GetGridReference();
        jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.filter([]);");
        JavaScriptService.Execute(jsToBeExecuted);
        WaitForAjax();
    }

    public int TotalNumberRows()
    {
        WaitForAjax();
        var jsToBeExecuted = GetGridReference();
        jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.total();");
        var jsResult = JavaScriptService.Execute(jsToBeExecuted);
        return int.Parse(jsResult.ToString());
    }

    public void Reload()
    {
        var jsToBeExecuted = GetGridReference();
        jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.read();");
        JavaScriptService.Execute(jsToBeExecuted);
        WaitForAjax();
    }

    public int GetPageSize()
    {
        var jsToBeExecuted = GetGridReference();
        jsToBeExecuted = string.Concat(jsToBeExecuted, "return grid.dataSource.pageSize();");
        var currentResponse = JavaScriptService.Execute(jsToBeExecuted, WrappedElement);
        var pageSize = int.Parse(currentResponse.ToString());
        return pageSize; 
    }

    public void ChangePageSize(int newSize)
    {
        var jsToBeExecuted = GetGridReference();
        jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.pageSize(", newSize, ");");
        JavaScriptService.Execute(jsToBeExecuted);
        WaitForAjax();
    }

    public void NavigateToPage(int pageNumber)
    {
        var jsToBeExecuted = GetGridReference();
        jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.page(", pageNumber, ");");
        JavaScriptService.Execute(jsToBeExecuted);
    }

    public void Sort(string columnName, SortType sortType)
    {
        var jsToBeExecuted = GetGridReference();
        WaitForDataSourceToBeInitialized();
        jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.sort({field: '", columnName, "', dir: '", sortType.ToString().ToLower(), "'});");
        JavaScriptService.Execute(jsToBeExecuted);
        WaitForAjax();
    }


    public List<T> GetItems<T>() where T : class
    {
        WaitForDataItemsToBeInitialized();
        var jsToBeExecuted = GetGridReference();
        jsToBeExecuted = string.Concat(jsToBeExecuted, "return JSON.stringify(grid.dataItems());");
        var jsResults = JavaScriptService.Execute(jsToBeExecuted);
        var items = JsonConvert.DeserializeObject<List<T>>(jsResults.ToString());
        return items;
    }

    public void Filter(string columnName, FilterOperator filterOperator, string filterValue)
    {
        Filter(new GridFilter(columnName, filterOperator, filterValue));
    }

    public void Filter(params GridFilter[] gridFilters)
    {
        var jsToBeExecuted = GetGridReference();
        WaitForDataSourceToBeInitialized();

        var sb = new StringBuilder();
        sb.Append(jsToBeExecuted);
        sb.Append("grid.dataSource.filter({ logic: \"and\", filters: [");
        foreach (var currentFilter in gridFilters)
        {
            DateTime filterDateTime;
            var isFilterDateTime = DateTime.TryParse(currentFilter.FilterValue, out filterDateTime);
            var filterValueToBeApplied = isFilterDateTime ? $"new Date({filterDateTime.Year}, {filterDateTime.Month - 1}, {filterDateTime.Day})" :
                                            $"""{currentFilter.FilterValue}""";
            var kendoFilterOperator = ConvertFilterOperatorToKendoOperator(currentFilter.FilterOperator);
            sb.Append(string.Concat("{ field: \"", currentFilter.ColumnName, "\", operator: \"", kendoFilterOperator, "\", value: \"", filterValueToBeApplied, "\" },"));
        }
        sb.Append("] });");
        jsToBeExecuted = sb.ToString().Replace(",]", "]");
        JavaScriptService.Execute(jsToBeExecuted);
        WaitForAjax();
    }

    public int GetCurrentPageNumber()
    {
        WaitForDataSourceToBeInitialized();
        var jsToBeExecuted = GetGridReference();
        jsToBeExecuted = string.Concat(jsToBeExecuted, "return grid.dataSource.page();");
        var result = JavaScriptService.Execute(jsToBeExecuted);
        var pageNumber = int.Parse(result.ToString());
        return pageNumber;
    }

    private string GetGridReference()
    {
        var initializeKendoGrid = string.Format("var grid = $('#{0}').data('kendoGrid');", FindStrategy.Value);

        return initializeKendoGrid;
    }

    private string ConvertFilterOperatorToKendoOperator(FilterOperator filterOperator)
    {
        var kendoFilterOperator = string.Empty;
        switch (filterOperator)
        {
            case FilterOperator.EqualTo:
                kendoFilterOperator = "eq";
                break;
            case FilterOperator.NotEqualTo:
                kendoFilterOperator = "neq";
                break;
            case FilterOperator.LessThan:
                kendoFilterOperator = "lt";
                break;
            case FilterOperator.LessThanOrEqualTo:
                kendoFilterOperator = "lte";
                break;
            case FilterOperator.GreaterThan:
                kendoFilterOperator = "gt";
                break;
            case FilterOperator.GreaterThanOrEqualTo:
                kendoFilterOperator = "gte";
                break;
            case FilterOperator.StartsWith:
                kendoFilterOperator = "startswith";
                break;
            case FilterOperator.EndsWith:
                kendoFilterOperator = "endswith";
                break;
            case FilterOperator.Contains:
                kendoFilterOperator = "contains";
                break;
            case FilterOperator.NotContains:
                kendoFilterOperator = "doesnotcontain";
                break;
            case FilterOperator.IsAfter:
                kendoFilterOperator = "gt";
                break;
            case FilterOperator.IsAfterOrEqualTo:
                kendoFilterOperator = "gte";
                break;
            case FilterOperator.IsBefore:
                kendoFilterOperator = "lt";
                break;
            case FilterOperator.IsBeforeOrEqualTo:
                kendoFilterOperator = "lte";
                break;
            default:
                throw new ArgumentException("The specified filter operator is not supported.");
        }

        return kendoFilterOperator;
    }

    private void WaitForAjax()
    {
        IBrowserService browserService = new DriverAdapter();
        browserService.WaitForAjax();
    }

    private void WaitForDataSourceToBeInitialized()
    {
        _wait.Until(driver =>
        {
            var jsToBeExecuted = GetGridReference() + "return grid.dataItems() !== null && grid.dataItems().length > 0;";
            return (bool)(driver as IJavaScriptExecutor).ExecuteScript(jsToBeExecuted);
        });
    }

    private void WaitForDataItemsToBeInitialized()
    {
        _wait.Until(driver =>
        {
            var jsToBeExecuted = GetGridReference() + "return grid.dataItems() !== null && grid.dataItems().length > 0;";
            return (bool)(driver as IJavaScriptExecutor).ExecuteScript(jsToBeExecuted);
        });
    }
}