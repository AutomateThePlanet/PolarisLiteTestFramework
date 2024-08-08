using RestSharp;

namespace PolarisLite.API;

public class ClientEventArgs
{
    public ClientEventArgs(IRestClient client) => Client = client;

    public IRestClient Client { get; }
}