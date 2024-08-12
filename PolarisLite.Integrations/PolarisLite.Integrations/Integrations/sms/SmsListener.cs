using Twilio.Rest.Api.V2010.Account;

namespace PolarisLite.Integrations;

public class SmsListener
{
    public static event EventHandler<SmsEventArgs> MessageReceived;

    private readonly string phoneNumber;
    private readonly List<MessageResource> messages = new List<MessageResource>();
    private CancellationTokenSource cancellationTokenSource;
    private DateTime start;

    public SmsListener(string phoneNumber = null)
    {
        this.phoneNumber = phoneNumber;
    }

    public List<MessageResource> GetMessages()
    {
        return new List<MessageResource>(messages);
    }

    public MessageResource GetLastMessage()
    {
        return messages.LastOrDefault();
    }

    public void Listen()
    {
        start = DateTime.UtcNow;
        cancellationTokenSource = new CancellationTokenSource();
        Task.Run(() => CheckForMessages(cancellationTokenSource.Token));
    }

    public void StopListening()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }

    private async Task CheckForMessages(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var messageReader = MessageResource.ReadAsync(
                dateSentAfter: start,
                from: phoneNumber,
                limit: 1
            );

            var foundMessages = await messageReader;

            if (foundMessages.Any())
            {
                var message = foundMessages.First();
                messages.Add(message);
                MessageReceived?.Invoke(this, new SmsEventArgs(this, message));
                start = DateTime.UtcNow;
            }

            await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationToken);
        }
    }
}
