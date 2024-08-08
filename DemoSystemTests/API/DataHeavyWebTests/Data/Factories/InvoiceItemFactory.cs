using Bogus;

namespace DemoSystemTests.Builder;
public static class InvoiceItemFactory
{
    public static InvoiceItem GenerateInvoiceItem()
    {
        var faker = new Faker<InvoiceItem>()
            .RuleFor(ii => ii.InvoiceLineId, f => f.Random.Long(1))
            .RuleFor(ii => ii.InvoiceId, f => f.Random.Long(1))
            .RuleFor(ii => ii.TrackId, f => f.Random.Long(1))
            .RuleFor(ii => ii.UnitPrice, f => f.Finance.Amount(0.99m, 15.99m).ToString())
            .RuleFor(ii => ii.Quantity, f => f.Random.Long(1, 10));

        return faker.Generate();
    }
}
