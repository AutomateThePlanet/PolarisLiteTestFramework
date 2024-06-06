using DemoSystemTests;
using RestSharp;

namespace RepositoryDesignPatternTests.Data.Repositories;
public class CustomerRepository : HttpRepository<Customers>
{
    public CustomerRepository(string baseUrl)
        : base(baseUrl, "customers")
    {
    }

    // Method for searching customers by name
    public async Task<List<Customers>> SearchAsync(string searchTerm)
    {
        var request = new RestRequest($"{entityEndpoint}/search/{searchTerm}", Method.Get);
        var response = await client.GetAsync<List<Customers>>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error searching customers by name: {response.ErrorMessage}");
        }

        return response.Response.Data;
    }
}
