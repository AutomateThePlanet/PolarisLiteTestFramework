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

    public override string ToString()
    {
        var trackName = Track != null ? Track.Name : "Unknown Track";
        var invoiceIdDisplay = Invoice != null ? Invoice.InvoiceId.ToString() : "Unknown Invoice";
        return $"InvoiceLineId: {InvoiceLineId}, InvoiceId: {invoiceIdDisplay}, TrackId: {TrackId} ({trackName}), " +
               $"UnitPrice: {UnitPrice}, Quantity: {Quantity}";
    }
}
