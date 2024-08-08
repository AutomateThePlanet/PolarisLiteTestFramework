using PolarisLite.API;

namespace DemoSystemTests.Builder;
public class CustomerRepository : HttpRepository<Customer>
{
    public CustomerRepository(string baseUrl)
        : base(baseUrl, "customers")
    {
    }
}
