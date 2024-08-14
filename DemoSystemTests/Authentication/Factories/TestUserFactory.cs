using Bogus;
using DemoSystemTests.Authentication.Models;
using PolarisLite;
using PolarisLite.API;
using PolarisLite.Integrations;
using PolarisLite.Integrations.Settings;
using RestSharp;

namespace DemoSystemTests.Authentication.Factories;
public static class TestUserFactory
{
    private static readonly Faker Faker = new Faker();
    private const string BaseUri = "https://chesstv.local:3000/";

    public static ApiClientService ApiClientService { get; set; }

    public static async Task<TestUser> CreateDefaultAsync()
    {
        return await CreateDefaultAsync(UserStatus.ACTIVE);
    }

    public static async Task<TestUser> CreateDefaultWithRealEmailAsync()
    {
        return await CreateDefaultWithRealEmailAsync(UserStatus.ACTIVE);
    }

    public static async Task<TestUser> CreateDefault2FAWithRealEmailAsync()
    {
        string username = Faker.Internet.UserName();
        string password = GeneratePassword();
        string phone = GeneratePhoneNumber();

        return await CreateTestUser2FAWithRealEmailAsync(username, password, phone, UserStatus.ACTIVE.GetDescription());
    }

    public static async Task<TestUser> CreateDefaultAsync(UserStatus status)
    {
        string username = Faker.Internet.UserName();
        string email = Faker.Internet.Email();
        string password = GeneratePassword();
        string phone = GeneratePhoneNumber();

        return await CreateTestUserAsync(username, email, password, phone, status.GetDescription());
    }

    public static TestUser CreateTestUserDto()
    {
        var newInbox = MailslurpService.CreateInbox();
        string username = GenerateUsername();
        string email = newInbox.EmailAddress;
        string password = GeneratePassword();
        string phone = GeneratePhoneNumber();

        var user = new TestUser
        {
            Username = username,
            Email = email,
            Password = password,
            Phone = phone,
            UserInbox = newInbox
        };

        return user;
    }

    public static async Task<TestUser> CreateDefaultWithRealEmailAsync(UserStatus status)
    {
        string username = Faker.Internet.UserName();
        string password = GeneratePassword();
        string phone = GeneratePhoneNumber();

        return await CreateTestUserWithRealEmailAsync(username, password, phone, status.GetDescription());
    }

    private static string GenerateUsername()
    {
        string username = Faker.Name.FirstName() + Faker.Random.Number(10000, 99999);
        return new string(username.Where(char.IsLetterOrDigit).ToArray());
    }

    private static string GeneratePassword()
    {
        return Faker.Internet.Password(10) + "Aa";
    }

    private static string GeneratePhoneNumber()
    {
        return IntegrationSettings.TwilioSettings.PhoneNumber;
    }

    private static async Task<TestUser> CreateTestUserAsync(string username, string email, string password, string phone, string status)
    {
        var user = new TestUser
        {
            Username = username,
            Email = email,
            Password = password,
            Phone = phone,
            Status = status
        };

        var request = new RestRequest("createTestUser", Method.Post)
            .AddJsonBody(user);

        var response = await ApiClientService.PostAsync<TestUser>(request);
        return response.Data;
    }

    private static async Task<TestUser> CreateTestUserWithRealEmailAsync(string username, string password, string phone, string status)
    {
        var newInbox = MailslurpService.CreateInbox(Guid.NewGuid().ToString());

        var user = new TestUser
        {
            Username = username,
            Email = newInbox.EmailAddress,
            Password = password,
            Phone = phone,
            Status = status
        };

        var request = new RestRequest("createTestUser", Method.Post)
            .AddJsonBody(user);

        var response = await ApiClientService.PostAsync<TestUser>(request);
        var createdUser = response.Data;
        createdUser.UserInbox = newInbox;
        return createdUser;
    }

    private static async Task<TestUser> CreateTestUser2FAWithRealEmailAsync(string username, string password, string phone, string status)
    {
        var newInbox = MailslurpService.CreateInbox(null);

        var user = new TestUser
        {
            Username = username,
            Email = newInbox.EmailAddress,
            Password = password,
            Phone = phone,
            Status = status
        };

        var request = new RestRequest("createTestUser2FA", Method.Post)
            .AddJsonBody(user);

        var response = await ApiClientService.PostAsync<TestUser>(request);
        var createdUser = response.Data;
        createdUser.UserInbox = newInbox;
        return createdUser;
    }
}
