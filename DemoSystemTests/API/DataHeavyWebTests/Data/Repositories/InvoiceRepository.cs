using PolarisLite.API;
using RestSharp;

namespace DemoSystemTests.Builder;
public class InvoiceRepository : HttpRepository<Invoice>
{
    public InvoiceRepository(string baseUrl)
        : base(baseUrl, "invoices")
    {
    }

    public List<InvoiceItem> GetInvoiceItems(int invoiceId)
    {
        var request = new RestRequest($"{entityEndpoint}/{invoiceId}/invoiceitems", Method.Get);
        var response = client.GetAsync<List<InvoiceItem>>(request).Result;

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error fetching invoice items: {response.ErrorMessage}");
        }

        return response.Data;
    }
}
