using PolarisLite.Core.Settings.StaticSettings;
using PolarisLite.Integrations.Settings;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace PolarisLite.Integrations;

public class SmsService
{
    private static readonly TwilioSettings settings;

    static SmsService()
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
}