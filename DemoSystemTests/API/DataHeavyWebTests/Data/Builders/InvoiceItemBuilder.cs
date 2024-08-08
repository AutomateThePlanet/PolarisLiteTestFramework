using RepositoryDesignPatternTests;

namespace DemoSystemTests.Builder;
public class InvoiceItemBuilder
{
    private InvoiceItem _invoiceItem;
    private InvoiceItemRepository _invoiceItemRepository;

    public InvoiceItemBuilder()
    {
        _invoiceItem = new InvoiceItem();
        _invoiceItemRepository = new InvoiceItemRepository(Urls.BASE_API_URL);
    }

    public InvoiceItemBuilder WithDefaultConfiguration()
    {
        _invoiceItem = InvoiceItemFactory.GenerateInvoiceItem();
        return this;
    }

    public InvoiceItemBuilder WithInvoiceLineId(long invoiceLineId)
    {
        _invoiceItem.InvoiceLineId = invoiceLineId;
        return this;
    }

    public InvoiceItemBuilder ForInvoice(Invoice invoice)
    {
        _invoiceItem.Invoice = invoice;
        _invoiceItem.InvoiceId = invoice.InvoiceId;
        return this;
    }

    public InvoiceItemBuilder WithTrack(Track track)
    {
        _invoiceItem.Track = track;
        _invoiceItem.TrackId = track.TrackId;
        return this;
    }

    public InvoiceItemBuilder WithUnitPrice(string unitPrice)
    {
        _invoiceItem.UnitPrice = unitPrice;
        return this;
    }

    public InvoiceItemBuilder WithQuantity(long quantity)
    {
        _invoiceItem.Quantity = quantity;
        return this;
    }

    public InvoiceItem Build()
    {
        var savedInvoiceItem = _invoiceItemRepository.CreateAsync(_invoiceItem).Result;
        return savedInvoiceItem;
    }
}
