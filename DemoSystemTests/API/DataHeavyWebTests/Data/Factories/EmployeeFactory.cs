using Bogus;

namespace DemoSystemTests.Builder;
public class EmployeeFactory
{
    public static Employee GenerateEmployee()
    {
        var faker = new Faker<Employee>()
            .RuleFor(e => e.EmployeeId, f => f.Random.Long(1))
            .RuleFor(e => e.LastName, f => f.Name.LastName())
            .RuleFor(e => e.FirstName, f => f.Name.FirstName())
            .RuleFor(e => e.Title, f => f.Name.JobTitle())
            .RuleFor(e => e.ReportsTo, f => f.Random.Long(1, 10)) // Optional: adjust based on your hierarchy depth
            .RuleFor(e => e.BirthDate, f => f.Date.Past(50, DateTime.Now.AddYears(-18)).ToString("yyyy-MM-dd"))
            .RuleFor(e => e.HireDate, f => f.Date.Past(20).ToString("yyyy-MM-dd"))
            .RuleFor(e => e.Address, f => f.Address.StreetAddress())
            .RuleFor(e => e.City, f => f.Address.City())
            .RuleFor(e => e.State, f => f.Address.State())
            .RuleFor(e => e.Country, f => f.Address.Country())
            .RuleFor(e => e.PostalCode, f => f.Address.ZipCode())
            .RuleFor(e => e.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(e => e.Fax, f => f.Phone.PhoneNumber())
            .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.FirstName, e.LastName));

        var employee = faker.Generate();

        return employee;
    }
}
