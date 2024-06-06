namespace PolarisLite.Web;

public interface IBrowserService
{
    void WaitForAjax();
    void Refresh();
    void Forward();
    void Back();
}
