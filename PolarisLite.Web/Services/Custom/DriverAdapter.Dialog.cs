namespace PolarisLite.Web.Services;

public partial class DriverAdapter : IDialogService
{
    public void Handle(Action<IAlert> action = null, DialogButton dialogButton = DialogButton.Ok)
    {
        var alert = WrappedDriver.SwitchTo().Alert();
        action?.Invoke(alert);
        if (dialogButton == DialogButton.Ok)
        {
            alert.Accept();
            WrappedDriver.SwitchTo().DefaultContent();
        }
        else
        {
            alert.Dismiss();
            WrappedDriver.SwitchTo().DefaultContent();
        }
    }

    public string GetText()
    {
        var alert = WrappedDriver.SwitchTo().Alert();

        return alert.Text;
    }
}