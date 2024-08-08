using DemoSystemTests;
using PolarisLite.API;
using RestSharp;

namespace RepositoryDesignPatternTests.Data.Repositories;
public class InvoiceRepository : HttpRepository<Invoices>
{
    public InvoiceRepository(string baseUrl)
        : base(baseUrl, "invoices")
    {
    }

    public async Task<List<InvoiceItems>> GetInvoiceItemsAsync(int invoiceId)
    {
        var request = new RestRequest($"{entityEndpoint}/{invoiceId}/invoiceitems", Method.Get);
        var response = await client.GetAsync<List<InvoiceItems>>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error fetching invoice items: {response.ErrorMessage}");
        }

        return response.Data;
    }
}
