namespace DemoSystemTests.Authentication.Models;
public enum UserStatus
{
    [System.ComponentModel.Description("active")]
    ACTIVE,
    [System.ComponentModel.Description("inactive")]
    INACTIVE,
    [System.ComponentModel.Description("pending")]
    PENDING
}