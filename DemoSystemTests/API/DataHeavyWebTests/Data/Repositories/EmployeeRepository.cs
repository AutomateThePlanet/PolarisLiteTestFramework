using PolarisLite.API;

namespace DemoSystemTests.Builder;
public class EmployeeRepository : HttpRepository<Employee>
{
    public EmployeeRepository(string baseUrl)
        : base(baseUrl, "employees")
    {
    }
}
