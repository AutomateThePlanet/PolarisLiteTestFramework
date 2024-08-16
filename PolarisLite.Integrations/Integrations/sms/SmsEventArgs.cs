using Twilio.Rest.Api.V2010.Account;

namespace PolarisLite.Integrations;

public class SmsEventArgs : EventArgs
{
    public SmsListener SmsListener { get; }
    public MessageResource Message { get; }

    public SmsEventArgs(SmsListener smsListener, MessageResource message)
    {
        SmsListener = smsListener;
        Message = message;
    }
}