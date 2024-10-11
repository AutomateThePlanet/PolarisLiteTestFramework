using PolarisLite.Core.Utilities;

namespace PolarisLite.API;

public class App
{
    // Use ThreadLocal to ensure each thread has its own instance of ApiClientAdapter
    private static ThreadLocal<ApiClientAdapter> _apiClientService = new ThreadLocal<ApiClientAdapter>(() => CreateNewApiClientService());

    public App()
    {
        // Initialize the ApiClientService for the current thread
        _apiClientService.Value = GetApiClientService();
    }

    public ApiClientAdapter ApiClient
    {
        get => _apiClientService.Value;
        init
        {
            // Set a new instance for the current thread
            _apiClientService.Value = GetApiClientService();
        }
    }

    public bool ShouldReuseRestClient { get; set; } = true;

    public void AddApiClientExecutionPlugin<TApiClientPlugin>()
        where TApiClientPlugin : ApiClientPlugin
    {
        var apiClientPlugin = (TApiClientPlugin)Activator.CreateInstance(typeof(TApiClientPlugin));
        apiClientPlugin?.Enable();
    }

    public void RemoveApiClientExecutionPlugin<TApiClientPlugin>()
        where TApiClientPlugin : ApiClientPlugin
    {
        var apiClientPlugin = (TApiClientPlugin)Activator.CreateInstance(typeof(TApiClientPlugin));
        apiClientPlugin?.Disable();
    }

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

    public ApiClientAdapter GetApiClientService(string url = null, bool sharedCookies = true, int maxRetryAttempts = 1, int pauseBetweenFailures = 1, TimeUnit timeUnit = TimeUnit.Seconds)
    {
        if (!ShouldReuseRestClient || _apiClientService.Value == null)
        {
            _apiClientService.Value = CreateNewApiClientService(url, maxRetryAttempts, pauseBetweenFailures, timeUnit);
        }

        return _apiClientService.Value;
    }

    private static ApiClientAdapter CreateNewApiClientService(string url = null, int maxRetryAttempts = 1, int pauseBetweenFailures = 1, TimeUnit timeUnit = TimeUnit.Seconds)
    {
        var pauseBetweenFailuresConfig = TimeSpanConverter.Convert(pauseBetweenFailures, timeUnit);
        return new ApiClientAdapter(url ?? ApiSettings.BaseUrl, maxRetryAttempts, pauseBetweenFailuresConfig.Milliseconds);
    }
}
