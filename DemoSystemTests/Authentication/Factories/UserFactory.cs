using Bogus;
using DemoSystemTests.Authentication.Models;
using PolarisLite.Utilities;

namespace DemoSystemTests.Authentication.Factories;
public static class UserFactory
{
    private const string DefaultPassword = "thesecret";
    private static readonly Faker Faker;

    static UserFactory()
    {
        Faker = new Faker();
    }

    public static User CreateDefault()
    {
        var user = new User
        {
            Email = TimestampBuilder.BuildUniqueEmail("test", "mailsurp"),
            FirstName = Faker.Name.FirstName(),
            LastName = Faker.Name.LastName(),
            UserName = Faker.Internet.UserName(),
            Telephone = Faker.Phone.PhoneNumber(),
            Password = DefaultPassword,
            PasswordConfirm = DefaultPassword,
            AgreedPrivacyPolicy = true,
            ShouldSubscribe = false
        };
        return user;
    }
}