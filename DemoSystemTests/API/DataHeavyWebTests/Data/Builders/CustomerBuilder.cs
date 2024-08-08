using RepositoryDesignPatternTests;

namespace DemoSystemTests.Builder;
public class CustomerBuilder
{
    private Customer _customer;
    private readonly CustomerRepository _customerRepository;

    public CustomerBuilder()
    {
        _customer = new Customer();
        _customerRepository = new CustomerRepository(Urls.BASE_API_URL);
    }

    public CustomerBuilder WithDefaultConfiguration()
    {
        _customer = CustomerFactory.GenerateCustomer();
        return this;
    }

    public CustomerBuilder WithCustomerId(int customerId)
    {
        _customer.CustomerId = customerId;
        return this;
    }

    public CustomerBuilder WithName(string firstName, string lastName)
    {
        _customer.FirstName = firstName;
        _customer.LastName = lastName;
        return this;
    }

    public CustomerBuilder WithContactInfo(string email, string phone)
    {
        _customer.Email = email;
        _customer.Phone = phone;
        return this;
    }

    public CustomerBuilder WithAddress(string address, string city, string state, string postalCode, string country)
    {
        _customer.Address = address;
        _customer.City = city;
        _customer.State = state;
        _customer.PostalCode = postalCode;
        _customer.Country = country;
        return this;
    }

    public CustomerBuilder AddInvoice(Invoice invoice)
    {
        _customer.Invoices.Add(invoice);
        return this;
    }

    public CustomerBuilder WithInvoices(IEnumerable<Invoice> invoices)
    {
        _customer.Invoices = invoices.ToList();
        return this;
    }

    public Customer Build()
    {
        return _customerRepository.CreateAsync(_customer).Result;
    }
}
