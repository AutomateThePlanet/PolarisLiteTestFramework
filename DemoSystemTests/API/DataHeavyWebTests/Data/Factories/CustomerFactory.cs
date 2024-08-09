using Bogus;

namespace DemoSystemTests.Builder;
public static class CustomerFactory
{
    // Generates a single Customer instance with fake data
    public static Customer GenerateCustomer(string email = null, string firstName = null, string lastName = null)
    {
        var faker = new Faker<Customer>()
            .RuleFor(c => c.CustomerId, f => f.IndexFaker + 1)
            .RuleFor(c => c.FirstName, f => firstName ?? f.Name.FirstName())
            .RuleFor(c => c.LastName, f => lastName ?? f.Name.LastName())
            .RuleFor(c => c.Email, (f, c) => email ?? f.Internet.Email(c.FirstName, c.LastName));
        return faker.Generate();
    }

    // Generates a list of Customer instances with fake data
    public static List<Customer> GenerateCustomers(int count = 1, string email = null, string firstName = null, string lastName = null)
    {
        var customers = new Faker<Customer>()
            .RuleFor(c => c.CustomerId, f => f.IndexFaker + 1)
            .RuleFor(c => c.FirstName, f => firstName ?? f.Name.FirstName())
            .RuleFor(c => c.LastName, f => lastName ?? f.Name.LastName())
            .RuleFor(c => c.Email, (f, c) => email ?? f.Internet.Email(c.FirstName, c.LastName))
            .Generate(count);

        return customers;
    }

    private static Random random = new Random();

    // Generates a single Customer instance with test data
    public static Customer GenerateCustomer1()
    {
        return new Customer
        {
            CustomerId = random.Next(1, 10000),
            FirstName = $"FirstName{random.Next(1, 100)}",
            LastName = $"LastName{random.Next(1, 100)}",
            Email = $"test{random.Next(1, 100)}@example.com"
        };
    }

    // Generates a list of Customer instances with test data
    public static List<Customer> GenerateCustomers1(int count)
    {
        var customers = new List<Customer>();

        for (int i = 0; i < count; i++)
        {
            customers.Add(new Customer
            {
                CustomerId = random.Next(1, 10000),
                FirstName = $"FirstName{random.Next(1, 100)}",
                LastName = $"LastName{random.Next(1, 100)}",
                Email = $"test{random.Next(1, 100)}@example.com"
            });
        }

        return customers;
    }
}
