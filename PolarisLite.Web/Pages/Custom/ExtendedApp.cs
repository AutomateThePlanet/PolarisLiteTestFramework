using PolarisLite.Web.Services;

namespace PolarisLite.Web;
public partial class App
{
    public IDialogService Dialogs => _driver;
}
