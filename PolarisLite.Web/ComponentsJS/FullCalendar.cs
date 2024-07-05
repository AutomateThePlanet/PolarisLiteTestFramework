using PolarisLite.Web.Components;

namespace PolarisLite.Web;
public class FullCalendar : WebComponent
{
    private readonly string _fullCalendarMethodJqueryExpression = "$('#{0}').fullCalendar('{1}')";

    public void ClickNextButton()
    {
        string scriptToBeExecuted = string.Format(_fullCalendarMethodJqueryExpression, FindStrategy.Value, "next");
        JavaScriptService.Execute(scriptToBeExecuted);
    }

    public void ClickPreviousButton()
    {
        string scriptToBeExecuted = string.Format(_fullCalendarMethodJqueryExpression, FindStrategy.Value, "prev");
        JavaScriptService.Execute(scriptToBeExecuted);
    }

    public void GoToToday()
    {
        string scriptToBeExecuted = string.Format(_fullCalendarMethodJqueryExpression, FindStrategy.Value, "today");
        JavaScriptService.Execute(scriptToBeExecuted); ;
    }

    public void GoToDate(DateTime date)
    {
        string scriptToBeExecuted = string.Format("$('#{0}').fullCalendar('gotoDate', $.fullCalendar.moment('{1}-{2}-{3}'))", FindStrategy.Value, date.Year, date.Month - 1, date.Day);
        JavaScriptService.Execute(scriptToBeExecuted);
    }
}
