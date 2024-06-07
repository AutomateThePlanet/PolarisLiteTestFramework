namespace PolarisLite.Mobile;

public interface IFileSystemService
{
    byte[] PullFile(string pathOnDevice);
    byte[] PullFolder(string remotePath);
    void PushFile(string pathOnDevice, FileInfo file);
    void PushFile(string pathOnDevice, byte[] base64Data);
}
