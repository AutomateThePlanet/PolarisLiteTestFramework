using PolarisLite.API;

namespace DemoSystemTests.Builder;
public class InvoiceItemRepository : HttpRepository<InvoiceItem>
{
    public InvoiceItemRepository(string baseUrl)
        : base(baseUrl, "invoiceitems")
    {
    }
}
