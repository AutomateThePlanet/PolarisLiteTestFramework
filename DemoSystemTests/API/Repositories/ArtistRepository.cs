namespace DemoSystemTests;

using PolarisLite.API;
using RestSharp;
using System.Collections.Generic;

public class ArtistRepository : HttpRepository<Artists>
{
    public ArtistRepository(string baseUrl)
        : base(baseUrl, "artists")
    {
    }

    public async Task<List<Artists>> SearchByNameAsync(string name)
    {
        var request = new RestRequest($"{entityEndpoint}/search/{name}", Method.Get);
        var response = await client.GetAsync<List<Artists>>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error searching artists by name: {response.ErrorMessage}");
        }

        return response.Data;
    }
}

