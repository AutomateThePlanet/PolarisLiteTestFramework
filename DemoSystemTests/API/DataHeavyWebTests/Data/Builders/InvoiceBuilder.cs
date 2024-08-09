using RepositoryDesignPatternTests;

namespace DemoSystemTests.Builder;
public class InvoiceBuilder
{
    private Invoice _invoice;
    private InvoiceRepository _invoiceRepository;

    public InvoiceBuilder()
    {
        _invoice = new Invoice();
        _invoiceRepository = new InvoiceRepository(Urls.BASE_API_URL);
    }

    public InvoiceBuilder WithDefaultConfiguration()
    {
        _invoice = InvoiceFactory.GenerateInvoice();
        return this;
    }

    public InvoiceBuilder WithInvoiceId(long invoiceId)
    {
        _invoice.InvoiceId = invoiceId;
        return this;
    }

    public InvoiceBuilder ForCustomer(Customer customer)
    {
        _invoice.Customer = customer;
        _invoice.CustomerId = _invoice.Customer.CustomerId;
        return this;
    }

    public InvoiceBuilder WithBillingAddress(string address, string city, string state, string postalCode, string country)
    {
        _invoice.BillingAddress = address;
        _invoice.BillingCity = city;
        _invoice.BillingState = state;
        _invoice.BillingPostalCode = postalCode;
        _invoice.BillingCountry = country;
        return this;
    }

    public InvoiceBuilder WithInvoiceDate(string invoiceDate)
    {
        _invoice.InvoiceDate = invoiceDate;
        return this;
    }

    public InvoiceBuilder AddInvoiceItem(InvoiceItem invoiceItem)
    {
        _invoice.InvoiceItems.Add(invoiceItem);
        return this;
    }

    public InvoiceBuilder WithInvoiceItems(IEnumerable<InvoiceItem> invoiceItems)
    {
        _invoice.InvoiceItems = invoiceItems.ToHashSet();
        return this;
    }

    public Invoice Build()
    {
        var savedInvoice = _invoiceRepository.CreateAsync(_invoice).Result;
        return savedInvoice;
    }
}
