namespace PolarisLite.API;

public class ClientEventArgs
{
    public ClientEventArgs(RestClient client) => Client = client;

    public RestClient Client { get; }
}