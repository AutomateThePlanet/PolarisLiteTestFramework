namespace DemoSystemTests.Builder;
public class TestDataDirector
{
    public Artist CreateArtistWithDiscographyAndSales(string artistName, int albumCount, int trackCountPerAlbum, int customerCount)
    {
        // Start building the artist
        var artist = GetArtistBuilder()
            .WithDefaultConfiguration()
            .WithName(artistName)
            .Build();

        for (int i = 0; i < albumCount; i++)
        {
            // Start building each album
            var album = GetAlbumBuilder()
                .WithTitle($"Album {i + 1}")
                .WithArtist(artist)
                .Build();

            for (int j = 0; j < trackCountPerAlbum; j++)
            {
                // Build and add each track to the album
                var track = GetTrackBuilder()
                    .WithName($"Track {j + 1}")
                    .WithDuration(200000) // Example duration
                    .WithAlbum(album)
                    .Build();

                for (int k = 0; k < customerCount; k++)
                {
                    // Build customer
                    var customer = GetCustomerBuilder()
                        .WithName($"Customer {k + 1}", "Lastname")
                        .WithContactInfo($"customer{k + 1}@example.com", "555-0100+i")
                        .Build();

                    // Build invoice for the customer
                    var invoice = GetInvoiceBuilder()
                        .ForCustomer(customer)
                        .WithInvoiceDate(DateTime.Now.ToString("yyyy-MM-dd"))
                        .Build();

                    // Add an invoice item for the track to the invoice
                    var invoiceItem = GetInvoiceItemBuilder()
                        .ForInvoice(invoice)
                        .WithTrack(track)
                        .WithUnitPrice("9.99") // Example unit price
                        .WithQuantity(1)
                        .Build();
                }
            }
        }

        return artist;
    }

    private ArtistBuilder GetArtistBuilder()
    {
        return new ArtistBuilder();
    }

    private AlbumBuilder GetAlbumBuilder()
    {
        return new AlbumBuilder();
    }

    private TrackBuilder GetTrackBuilder()
    {
        return new TrackBuilder();
    }

    private CustomerBuilder GetCustomerBuilder()
    {
        return new CustomerBuilder();
    }

    private InvoiceBuilder GetInvoiceBuilder()
    {
        return new InvoiceBuilder();
    }

    private InvoiceItemBuilder GetInvoiceItemBuilder()
    {
        return new InvoiceItemBuilder();
    }
}
