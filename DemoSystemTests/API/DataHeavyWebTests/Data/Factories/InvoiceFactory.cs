using Bogus;

namespace DemoSystemTests.Builder;
public static class InvoiceFactory
{
    public static Invoice GenerateInvoice()
    {
        var faker = new Faker<Invoice>()
            .RuleFor(i => i.InvoiceId, f => f.Random.Int(1))
            .RuleFor(i => i.CustomerId, f => f.Random.Int(1, 100))
            .RuleFor(i => i.InvoiceDate, f => f.Date.Past().ToLongDateString())
            .RuleFor(i => i.Total, f => f.Finance.Amount().ToString());

        return faker.Generate();
    }
}
