namespace PolarisLite.Web.Services;
public interface IDialogService
{
    public void Handle(Action<IAlert> action = null, DialogButton dialogButton = DialogButton.Ok);

    public string GetText();
}
