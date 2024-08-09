namespace DemoSystemTests.Builder;
public class Invoice
{
    public Invoice() => InvoiceItems = new HashSet<InvoiceItem>();

    public long InvoiceId { get; set; }
    public long CustomerId { get; set; }
    public string InvoiceDate { get; set; }
    public string BillingAddress { get; set; }
    public string BillingCity { get; set; }
    public string BillingState { get; set; }
    public string BillingCountry { get; set; }
    public string BillingPostalCode { get; set; }
    public string Total { get; set; }

    public Customer Customer { get; set; }
    public ICollection<InvoiceItem> InvoiceItems { get; set; }

    public override string ToString()
    {
        var customerName = Customer != null ? $"{Customer.FirstName} {Customer.LastName}" : "Unknown Customer";
        return $"InvoiceId: {InvoiceId}, CustomerId: {CustomerId} ({customerName}), InvoiceDate: {InvoiceDate}, " +
               $"BillingAddress: {BillingAddress}, BillingCity: {BillingCity}, BillingState: {BillingState}, " +
               $"BillingCountry: {BillingCountry}, BillingPostalCode: {BillingPostalCode}, Total: {Total}, " +
               $"InvoiceItems Count: {InvoiceItems.Count}";
    }
}
