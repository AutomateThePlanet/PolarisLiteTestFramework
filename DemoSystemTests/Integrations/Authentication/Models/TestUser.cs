using mailslurp.Model;

namespace DemoSystemTests.Integrations.Authentication.Models;

public class TestUser
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string Status { get; set; }
    public InboxDto UserInbox { get; set; }
    public TwoFA TwoFA { get; set; }
}