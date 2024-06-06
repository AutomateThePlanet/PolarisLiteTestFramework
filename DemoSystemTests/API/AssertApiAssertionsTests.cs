using AutoFixture;
using Dasync.Collections;
using Newtonsoft.Json;
using PolarisLite.API;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using RestSharp.Serializers;
using RestSharp.Serializers.NewtonsoftJson;
using System.Net;
using System.Threading.Tasks;

namespace DemoSystemTests;

[TestFixture]
public class AssertApiAssertionsTests
{
    private const string BASE_URL = "http://localhost:60715/";
    private static ApiClientService _restClient;
    private Fixture _fixture;

    [OneTimeSetUp]
    public void ClassSetup()
    {
        var authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                 "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJiZWxsYXRyaXhVc2VyIiwianRpIjoiNjEyYjIzOTktNDUzMS00NmU0LTg5NjYtN2UxYmRhY2VmZTFlIiwibmJmIjoxNTE4NTI0NDg0LCJleHAiOjE1MjM3MDg0ODQsImlzcyI6ImF1dG9tYXRldGhlcGxhbmV0LmNvbSIsImF1ZCI6ImF1dG9tYXRldGhlcGxhbmV0LmNvbSJ9.Nq6OXqrK82KSmWNrpcokRIWYrXHanpinrqwbUlKT_cs",
                 "Bearer");
        _restClient = new ApiClientService("http://localhost:60715/", authenticator: authenticator);

        _restClient.WrappedClient.AddDefaultHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
        _fixture = new Fixture();
    }

    [OneTimeTearDown]
    public void TestCleanup()
    {
        _restClient.Dispose();
    }

    [Test]
    public async Task AssertSuccessStatusCode()
    {
        var request = new RestRequest("api/Albums", Method.Get);

        var response = await _restClient.GetAsync(request);

        response.AssertSuccessStatusCode();
    }

    [Test]
    public async Task AssertStatusCodeOk()
    {
        var request = new RestRequest("api/Albums", Method.Get);

        var response = await _restClient.GetAsync(request);

        response.AssertStatusCode(HttpStatusCode.OK);
    }

    [Test]
    public async Task AssertResponseHeaderServerIsEqualToKestrel()
    {
        var request = new RestRequest("api/Albums", Method.Get);

        var response = await _restClient.GetAsync(request);

        response.AssertResponseHeader("server", "Kestrel");
    }

    [Test]
    public async Task AssertContentTypeJson()
    {
        var request = new RestRequest("api/Albums/10", Method.Get);

        var response = await _restClient.GetAsync<Albums>(request);

        response.AssertContentType("application/json");
    }

    [Test]
    public async Task AssertContentContainsAudioslave()
    {
        var request = new RestRequest("api/Albums/10", Method.Get);

        var response = await _restClient.GetAsync<Albums>(request);

        response.AssertContentContains("Audioslave");
    }

    [Test]
    [Ignore("Do not execute")]
    public async Task AssertContentEncodingUtf8()
    {
        var request = new RestRequest("api/Albums/10", Method.Get);

        var response = await _restClient.GetAsync<Albums>(request).ConfigureAwait(false);

        response.AssertContentEncoding("gzip");
    }

    [Test]
    public async Task AssertContentEquals()
    {
        var request = new RestRequest("api/Albums/10", Method.Get);

        var response = await _restClient.GetAsync<Albums>(request);

        response.AssertContentEquals("{\"albumId\":10,\"title\":\"Audioslave\",\"artistId\":8,\"artist\":null,\"tracks\":[]}");
    }

    [Test]
    public async Task AssertContentNotContainsRammstein()
    {
        var request = new RestRequest("api/Albums/10", Method.Get);

        var response = await _restClient.GetAsync<Albums>(request);

        response.AssertContentNotContains("Rammstein");
    }

    [Test]
    public async Task AssertContentNotEqualsRammstein()
    {
        var request = new RestRequest("api/Albums/10", Method.Get);

        var response = await _restClient.GetAsync<Albums>(request);

        response.AssertContentNotEquals("Rammstein");
    }

    [Test]
    public async Task AssertResultEquals()
    {
        var expectedAlbum = new Albums
        {
            AlbumId = 10,
        };
        var request = new RestRequest("api/Albums/10", Method.Get);

        var response = await _restClient.GetAsync<Albums>(request);

        response.Response.AssertResultEquals(expectedAlbum);
    }

    [Test]
    public async Task AssertResultNotEquals()
    {
        var expectedAlbum = new Albums
        {
            AlbumId = 11,
        };
        var request = new RestRequest("api/Albums/10", Method.Get);

        var response = await _restClient.GetAsync<Albums>(request);

        response.Response.AssertResultNotEquals(expectedAlbum);
    }

    [Test]
    [Ignore("the followin functionality is not implemented in mediaStoreAPI")]
    public async Task AssertCookieExists()
    {
        var request = new RestRequest("api/Albums/10");

        var response = await _restClient.GetAsync<Albums>(request);

        response.AssertCookieExists("whoIs");
    }

    [Test]
    [Ignore("the followin functionality is not implemented in mediaStoreAPI")]
    public async Task AssertCookieWhoIsBella()
    {
        var request = new RestRequest("api/Albums/10", Method.Get);

        var response = await _restClient.GetAsync<Albums>(request);

        response.AssertCookie("whoIs", "Bella");
    }

    [Test]
    public async Task AssertJsonSchema()
    {
        var request = new RestRequest("api/Albums/10");

        var response = await _restClient.GetAsync<Albums>(request).ConfigureAwait(false);

        // http://json-schema.org/examples.html
        var expectedSchema = @"{
                                    ""title"": ""Albums"",
                                    ""type"": ""object"",
                                    ""properties"": {
                                                ""albumId"": {
                                                    ""type"": ""integer""
                                                },
                                        ""title"": {
                                                    ""type"": ""string""
                                        },
                                        ""artistId"": {
                                                    ""type"": ""integer""
                                        },
 	                                ""artist"": {
                                                    ""type"": [""object"", ""null""]
                                        },
	                                ""tracks"": {
                                                    ""type"": ""array""
                                        }
                                            },
                                    ""required"": [""albumId""]
                                  }";
        response.AssertSchema(expectedSchema);
    }
}