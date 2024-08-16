namespace DemoSystemTests.Integrations.Authentication.Models;
public class TwoFA
{
    public string Secret { get; set; }
    public bool Enabled { get; set; }
}