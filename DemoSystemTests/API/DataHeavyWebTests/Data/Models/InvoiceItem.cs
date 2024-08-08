namespace DemoSystemTests.Builder;
public class InvoiceItem
{
    public long InvoiceLineId { get; set; }
    public long InvoiceId { get; set; }
    public long TrackId { get; set; }
    public string UnitPrice { get; set; }
    public long Quantity { get; set; }

    public Invoice Invoice { get; set; }
    public Track Track { get; set; }
}
