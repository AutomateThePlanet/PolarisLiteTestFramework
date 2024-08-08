using PolarisLite.Core.Utilities;

namespace PolarisLite.API;

public class App
{
    private ApiClientService _apiClientService;

    public App()
    {
        _apiClientService = GetApiClientService();
    }

    public ApiClientService ApiClient
    {
        get => _apiClientService;
        init
        {
            _apiClientService = GetApiClientService();
        }
    }

    public bool ShouldReuseRestClient { get; set; } = true;

    public void AddAssertionsEventHandler<TApiAssertionsEventHandler>()
        where TApiAssertionsEventHandler : AssertExtensionsEventHandlers
    {
        var elementEventHandler = (TApiAssertionsEventHandler)Activator.CreateInstance(typeof(TApiAssertionsEventHandler));
        elementEventHandler?.SubscribeToAll();
    }

    public void RemoveAssertionsEventHandler<TApiAssertionsEventHandler>()
        where TApiAssertionsEventHandler : AssertExtensionsEventHandlers
    {
        var elementEventHandler = (TApiAssertionsEventHandler)Activator.CreateInstance(typeof(TApiAssertionsEventHandler));
        elementEventHandler?.UnsubscribeToAll();
    }

    public ApiClientService GetApiClientService(string url = null, bool sharedCookies = true, int maxRetryAttempts = 1, int pauseBetweenFailures = 1, TimeUnit timeUnit = TimeUnit.Seconds)
    {
        if (!ShouldReuseRestClient || _apiClientService == null)
        {
            var pauseBetweenFailuresConfig = TimeSpanConverter.Convert(pauseBetweenFailures, timeUnit);

            _apiClientService = new ApiClientService(url ?? ApiSettings.BaseUrl, maxRetryAttempts, pauseBetweenFailuresConfig.Milliseconds);
        }

        return _apiClientService;
    }
}