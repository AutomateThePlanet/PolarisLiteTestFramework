namespace PolarisLite.Web;

public interface IDialogService
{
    void Handle(Action<IAlert> action = null, DialogButton dialogButton = DialogButton.Ok);
}
