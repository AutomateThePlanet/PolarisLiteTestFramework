using PolarisLite.Core.Settings.StaticSettings;
using PolarisLite.Integrations.Settings;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace PolarisLite.Integrations;

public class TwillioService
{
    private static readonly TwilioSettings settings;

    static TwillioService()
    {
        settings = IntegrationSettings.TwilioSettings;
        TwilioClient.Init(settings.AccountSID, settings.AuthToken);
    }

    public static SmsListener ListenForSms(string fromNumber)
    {
        var smsListener = new SmsListener(fromNumber);
        smsListener.Listen();
        return smsListener;
    }

    public static SmsListener ListenForSms()
    {
        var smsListener = new SmsListener();
        smsListener.Listen();
        return smsListener;
    }

    public static void StopListeningForSms(SmsListener smsListener)
    {
        smsListener.StopListening();
    }

    public static List<MessageResource> GetMessages(SmsListener smsListener)
    {
        return smsListener.GetMessages();
    }

    public static MessageResource GetLastMessage(SmsListener smsListener)
    {
        return smsListener.GetLastMessage();
    }

    // New method to send SMS
    public static MessageResource SendSms(string toNumber, string messageBody)
    {
        // Create the message object
        var message = MessageResource.Create(
            body: messageBody,
            from: new PhoneNumber(IntegrationSettings.TwilioSettings.PhoneNumber), // The Twilio number you've registered
            to: new PhoneNumber(toNumber) // The recipient's phone number
        );

        return message;
    }

    // Overload to send SMS with media URLs (MMS)
    public static MessageResource SendSms(string toNumber, string messageBody, List<Uri> mediaUrls)
    {
        // Create the message object with media URLs for MMS
        var message = MessageResource.Create(
            body: messageBody,
            from: new PhoneNumber(IntegrationSettings.TwilioSettings.PhoneNumber),
            to: new PhoneNumber(toNumber),
            mediaUrl: mediaUrls
        );

        return message;
    }
}