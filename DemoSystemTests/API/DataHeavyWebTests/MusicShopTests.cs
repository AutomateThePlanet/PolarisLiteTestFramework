using System.Diagnostics;
using DemoSystemTests.Builder;
using PolarisLite.Web;
using PolarisLite.Web.Core.NUnit;
using PolarisLite.Web.Plugins;

namespace RepositoryDesignPatternTests;

[TestFixture]
[LambdaTest(BrowserType.Chrome, 125)]
public class MusicShopTests : WebTest
{
    private CustomerRepository _customerRepository;

    protected override void TestInitialize()
    {
        _customerRepository = new CustomerRepository(Urls.BASE_API_URL);
        var artistRepository = new ArtistRepository(Urls.BASE_API_URL);
        var albumRepository = new AlbumRepository(Urls.BASE_API_URL);
        var trackRepository = new TrackRepository(Urls.BASE_API_URL);
        var customerRepository = new CustomerRepository(Urls.BASE_API_URL);
        var invoiceRepository = new InvoiceRepository(Urls.BASE_API_URL);
        var invoiceItemRepository = new InvoiceItemRepository(Urls.BASE_API_URL);

        // Manually create an artist
        var artist = ArtistFactory.GenerateArtist();
        artist.Name = "Artist Name";
        artist = artistRepository.CreateAsync(artist).Result;

        for (int i = 0; i < 2; i++) // Assuming 2 albums
        {
            // Manually create an album
            var album = AlbumFactory.GenerateAlbum();
            album.Title = $"Album {i + 1}";
            album.ArtistId = artist.ArtistId;
            album = albumRepository.CreateAsync(album).Result;

            for (int j = 0; j < 5; j++) // Assuming 5 tracks per album
            {
                // Manually create a track
                var track = TrackFactory.GenerateTrack();
                track.Name = $"Track {j + 1}";
                track.AlbumId = album.AlbumId;
                track = trackRepository.CreateAsync(track).Result;

                for (int k = 0; k < 10; k++) // Assuming 10 customers per track
                {
                    // Manually create a customer
                    var customer = CustomerFactory.GenerateCustomer();
                    customer.FirstName = $"Customer {k + 1}";
                    customer.LastName = "Lastname";
                    customer.Email = $"customer{k + 1}@example.com";
                    customer.Phone = "555-0100+i";
                    customer = customerRepository.CreateAsync(customer).Result;

                    // Manually create an invoice for the customer
                    var invoice = InvoiceFactory.GenerateInvoice();
                    invoice.CustomerId = customer.CustomerId;
                    invoice.InvoiceDate = DateTime.Now.ToShortDateString();
                    invoice = invoiceRepository.CreateAsync(invoice).Result;

                    // Manually create an invoice item for the invoice and track
                    var invoiceItem = InvoiceItemFactory.GenerateInvoiceItem();
                    invoiceItem.InvoiceId = invoice.InvoiceId;
                    invoiceItem.TrackId = track.TrackId;
                    invoiceItem.Quantity = 1;
                    invoiceItem.UnitPrice = "9.99"; // Example unit price
                    invoiceItem = invoiceItemRepository.CreateAsync(invoiceItem).Result;
                }
            }
        }

        App.Navigation.GoToUrl(Urls.BASE_URL);
        App.Cookies.DeleteAllCookies();
    }

    [Test]
    public void RightCustomersDisplayed_When_SearchViaUI()
    {
        var director = new TestDataDirector();

        //// Now, use the director to create data with the complexity and relationships needed
        Artist artist = director.CreateArtistWithDiscographyAndSales("New Artist", 3, 10, 100);

        // Arrange
        var customer1 = _customerRepository.CreateAsync(CustomerFactory.GenerateCustomer(lastName: "Doe", email: "john.doe@example.com")).Result;
        var customer2 = _customerRepository.CreateAsync(CustomerFactory.GenerateCustomer(lastName: "Doe", email: "jane.doe@example.net")).Result;

        var customersTab = App.Elements.FindByXPath<Anchor>("//a[text()='Customers']");
        customersTab.Click();

        var customersSearchInput = App.Elements.FindById<TextField>("searchCustomerQuery");
        customersSearchInput.TypeText("LastName:Doe;AND;Email:.com");


        var searchButton = App.Elements.FindByXPath<Button>("//button[text()='Search']");
        searchButton.Click();

        var allHeaders = App.Elements.FindAllByXPath<Label>("//tbody[@id='customerList']/preceding-sibling::thead/tr/th").Select(x => x.Text).ToList();
        int indexOfFirstName = allHeaders.FindIndex(0, allHeaders.Count, s => s.Equals("First Name")) + 1;
        int indexOfLastName = allHeaders.FindIndex(0, allHeaders.Count, s => s.Equals("Last Name")) + 1;
        int indexOfEmail= allHeaders.FindIndex(0, allHeaders.Count, s => s.Equals("Email")) + 1;

        var allLastNames = App.Elements.FindAllByXPath<Label>($"//tbody[@id='customerList']/tr/td[{indexOfLastName}]");
        //allLastNames.ToList().ForEach(s => Logger.LogMessage(s.Text));
        var allEmails = App.Elements.FindAllByXPath<Label>($"//tbody[@id='customerList']/tr/td[{indexOfEmail}]");

        Assert.IsTrue(allLastNames.Any(c => c.Text.Contains("Doe")));
        Assert.IsTrue(allEmails.Any(c => c.Text.Contains(".com")));

        // Cleanup
        _customerRepository.DeleteAsync(customer1.CustomerId).Wait();
        _customerRepository.DeleteAsync(customer2.CustomerId).Wait();
    }
}
