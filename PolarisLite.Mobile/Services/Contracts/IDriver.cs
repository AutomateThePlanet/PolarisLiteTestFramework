using PolarisLite.Mobile.Services.Contracts;

namespace PolarisLite.Mobile;

public interface IDriver : IKeyboardService, IDeviceService, IFileSystemService, IElementFindService, IAppService, IWebService, ITouchActionsService
{
}
