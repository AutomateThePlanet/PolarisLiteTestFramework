namespace PolarisLite.Mobile.Services;

public partial class DriverAdapter : IFileSystemService
{
    public byte[] PullFile(string pathOnDevice)
    {
        return _androidDriver.PullFile(pathOnDevice);
    }

    public byte[] PullFolder(string remotePath)
    {
        return _androidDriver.PullFolder(remotePath);
    }

    public void PushFile(string pathOnDevice, FileInfo file)
    {
        _androidDriver.PushFile(pathOnDevice, file);
    }

    public void PushFile(string pathOnDevice, byte[] base64Data)
    {
        _androidDriver.PushFile(pathOnDevice, base64Data);
    }
}
