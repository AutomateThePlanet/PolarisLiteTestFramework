namespace PolarisLite.Web;

public interface IBrowserService
{
    void WaitForAjax();
    void Refresh();
    void Forward();
    void Back();

    void ErrorToastMessage(string message);
    void WarningToastMessage(string message);
    void InfoToastMessage(string message);
    void SuccessToastMessage(string message);
}
