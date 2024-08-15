using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polly;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace PolarisLite.API;
public class ApiClientAdapter : IDisposable
{
    private bool _isDisposed;

    public ApiClientAdapter(string baseUrl, int maxRetryAttempts = 3, int pauseBetweenFailuresMilliseconds = 500, IAuthenticator authenticator = null)
    {
        var options = new RestClientOptions()
        {
            ThrowOnAnyError = true,
            FollowRedirects = true,
            MaxRedirects = 10,
            BaseUrl = new Uri(baseUrl)
        };
        if (authenticator != null)
        {
            // TODO: to be extended to support parallel test execution.
            options.Authenticator = authenticator ?? Authenticator;
        }
        var settings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };
        WrappedClient = new RestClient(options, configureSerialization: s => s.UseNewtonsoftJson(settings));

        Policy.Timeout(ApiSettings.ClientTimeoutSeconds, onTimeout: (context, timespan, task) =>
        {
            task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    ApiClientPluginExecutionEngine.OnRequestTimeout(WrappedClient);
                }
            });
        });

        MaxRetryAttempts = maxRetryAttempts;
        PauseBetweenFailures = TimeSpan.FromMilliseconds(pauseBetweenFailuresMilliseconds);

        _isDisposed = false;
    }

    public static int MaxRetryAttempts { get; set; }
    public static TimeSpan PauseBetweenFailures { get; set; }
    public static IAuthenticator Authenticator { get; set; }

    public RestClient WrappedClient { get; set; }

    public async Task<byte[]?> DownloadAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
    {
        if (cancellationTokenSource == null)
        {
            cancellationTokenSource = new CancellationTokenSource();
        }

        var retryPolicy = Policy.Handle<NotSuccessfulRequestException>().WaitAndRetryAsync(MaxRetryAttempts, i => PauseBetweenFailures);

        var result = await retryPolicy.ExecuteAsync(async () =>
        {
            var downloadedData = await WrappedClient.DownloadDataAsync(request, cancellationTokenSource.Token);

            return downloadedData;
        });

        return result;
    }

    public async Task<MeasuredResponse> GetAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
    {
        return await ExecuteMeasuredRequestAsync(request, Method.Get, cancellationTokenSource);
    }

    public async Task<MeasuredResponse<TReturnType>> GetAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : class
    {
        return await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Get, cancellationTokenSource);
    }

    public async Task<MeasuredResponse> PutAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
    {
        return await ExecuteMeasuredRequestAsync(request, Method.Put, cancellationTokenSource);
    }

    public async Task<MeasuredResponse<TReturnType>> PutAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : class
    {
        return await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Put, cancellationTokenSource);
    }

    public async Task<MeasuredResponse> PostAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
    {
        return await ExecuteMeasuredRequestAsync(request, Method.Post, cancellationTokenSource);
    }

    public async Task<MeasuredResponse<TReturnType>> PostAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : class
    {
        return await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Post, cancellationTokenSource);
    }

    public async Task<MeasuredResponse> DeleteAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
    {
        return await ExecuteMeasuredRequestAsync(request, Method.Delete, cancellationTokenSource);
    }

    public async Task<MeasuredResponse<TReturnType>> DeleteAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : class
    {
        return await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Delete, cancellationTokenSource);
    }

    public async Task<MeasuredResponse> CopyAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
    {
        return await ExecuteMeasuredRequestAsync(request, Method.Copy, cancellationTokenSource);
    }

    public async Task<MeasuredResponse<TReturnType>> CopyAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : class
    {
        return await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Copy, cancellationTokenSource);
    }

    public async Task<MeasuredResponse> HeadAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
    {
        return await ExecuteMeasuredRequestAsync(request, Method.Head, cancellationTokenSource);
    }

    public async Task<MeasuredResponse<TReturnType>> HeadAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : class
    {
        return await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Head, cancellationTokenSource);
    }

    public async Task<MeasuredResponse> MergeAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
    {
        return await ExecuteMeasuredRequestAsync(request, Method.Merge, cancellationTokenSource);
    }

    public async Task<MeasuredResponse<TReturnType>> MergeAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : class
    {
        return await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Merge, cancellationTokenSource);
    }

    public async Task<MeasuredResponse> OptionsAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
    {
        return await ExecuteMeasuredRequestAsync(request, Method.Options, cancellationTokenSource);
    }

    public async Task<MeasuredResponse<TReturnType>> OptionsAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : class
    {
        return await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Options, cancellationTokenSource);
    }

    public async Task<MeasuredResponse> PatchAsync(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
    {
        return await ExecuteMeasuredRequestAsync(request, Method.Patch, cancellationTokenSource);
    }

    public async Task<MeasuredResponse<TReturnType>> PatchAsync<TReturnType>(RestRequest request, CancellationTokenSource cancellationTokenSource = null)
        where TReturnType : class
    {
        return await ExecuteMeasuredRequestAsync<TReturnType>(request, Method.Patch, cancellationTokenSource);
    }

    private async Task<MeasuredResponse<TReturnType>> ExecuteMeasuredRequestAsync<TReturnType>(RestRequest request, Method method, CancellationTokenSource cancellationTokenSource = null)
      where TReturnType : class
    {
        if (cancellationTokenSource == null)
        {
            cancellationTokenSource = new CancellationTokenSource();
        }

        var retryPolicy = Policy.Handle<NotSuccessfulRequestException>().WaitAndRetryAsync(MaxRetryAttempts, i => PauseBetweenFailures);

        var response = await retryPolicy.ExecuteAsync(async () =>
        {
            var watch = Stopwatch.StartNew();

            ApiClientPluginExecutionEngine.OnMakingRequest(request, request.Resource);

            request.Method = method;
            var measuredResponse = default(MeasuredResponse<TReturnType>);
            var response = await WrappedClient.ExecuteAsync<TReturnType>(request, cancellationTokenSource.Token);

            ApiClientPluginExecutionEngine.OnRequestMade(response, request.Resource);

            watch.Stop();
            measuredResponse = new MeasuredResponse<TReturnType>(response, watch.Elapsed);

            if (!measuredResponse.Response.IsSuccessful)
            {
                ApiClientPluginExecutionEngine.OnRequestFailed(response, request.Resource);
                throw new NotSuccessfulRequestException();
            }

            return measuredResponse;
        });

        return response;
    }

    private async Task<MeasuredResponse> ExecuteMeasuredRequestAsync(RestRequest request, Method method, CancellationTokenSource cancellationTokenSource = null)
    {
        if (cancellationTokenSource == null)
        {
            cancellationTokenSource = new CancellationTokenSource();
        }

        var retryPolicy = Policy.Handle<NotSuccessfulRequestException>().WaitAndRetryAsync(MaxRetryAttempts, i => PauseBetweenFailures);

        var response = await retryPolicy.ExecuteAsync(async () =>
        {
            var watch = Stopwatch.StartNew();

            request.Method = method;

            ApiClientPluginExecutionEngine.OnMakingRequest(request, request.Resource);

            var response = await WrappedClient.ExecuteAsync(request, cancellationTokenSource.Token);

            ApiClientPluginExecutionEngine.OnRequestMade(response, request.Resource);

            watch.Stop();
            var measuredResponse = new MeasuredResponse(response, watch.Elapsed);

            if (!measuredResponse.Response.IsSuccessful)
            {
                ApiClientPluginExecutionEngine.OnRequestFailed(response, request.Resource);
                throw new NotSuccessfulRequestException();
            }

            return measuredResponse;
        });

        return response;
    }

    public void Dispose()
    {
        if (!_isDisposed)
        {
            WrappedClient.Dispose();
            _isDisposed = true;
        }
    }
}