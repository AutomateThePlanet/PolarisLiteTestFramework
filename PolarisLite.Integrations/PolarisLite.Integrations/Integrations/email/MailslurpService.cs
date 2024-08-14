using mailslurp.Api;
using mailslurp.Client;
using mailslurp.Model;
using PolarisLite.Core.Utilities;
using PolarisLite.Integrations.Settings;

namespace PolarisLite.Integrations;

public static class MailslurpService
{
    private const long TIMEOUT = 30000L;
    private static InboxControllerApi inboxControllerApi;

    static MailslurpService()
    {
        var config = new Configuration();
        config.ApiKey.Add("x-api-key", IntegrationSettings.MailslurpSettings.ApiKey);
        inboxControllerApi = new InboxControllerApi(config);
    }

    public static InboxDto CreateInbox()
    {
        var inbox = inboxControllerApi.CreateInbox();

        return inbox;
    }

    public static InboxDto CreateInbox(string name)
    {
        var inbox = inboxControllerApi.CreateInbox(name: name);

        return inbox;
    }

    public static mailslurp.Model.Email WaitForLatestEmail(InboxDto inbox, DateTime? since = null)
    {
        if (since == null)
        {
            since = DateTime.UtcNow.AddMinutes(-5);
        }

        var waitForControllerApi = new WaitForControllerApi();
        mailslurp.Model.Email receivedEmail = waitForControllerApi.WaitForLatestEmail(inbox.Id, TIMEOUT, false, null, since, null, 10000L);

        return receivedEmail;
    }

    public static void SendEmail(InboxDto inbox, string toEmail, string subject, string templateName)
    {
        var emailBody = EmbeddedResourcesService.FromFile(templateName);
        // send HTML body email
        var sendEmailOptions = new SendEmailOptions();
        sendEmailOptions.To = new List<string> { toEmail };
        sendEmailOptions.Subject = subject;
        sendEmailOptions.Body = emailBody;

        inboxControllerApi.SendEmail(inbox.Id, sendEmailOptions);
    }
}
