namespace PolarisLite.Integrations;

public class TwilioSettings
{
    public string AccountSID { get; set; }
    public string AuthToken { get; set; }
    public string PhoneNumber { get; set; }
    public bool SendSmsOnFailure { get; set; } = false;
}
