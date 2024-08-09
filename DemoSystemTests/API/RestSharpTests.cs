using AutoFixture;
using DemoSystemTests.Builder;
using Polaris.API.NUnit;
using PolarisLite.API;
using RestSharp;

namespace DemoSystemTests;

[TestFixture]
[OAuth2AuthorizationRequestHeaderAuthenticationStrategyAttribute("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJiZWxsYXRyaXhVc2VyIiwianRpIjoiNjEyYjIzOTktNDUzMS00NmU0LTg5NjYtN2UxYmRhY2VmZTFlIiwibmJmIjoxNTE4NTI0NDg0LCJleHAiOjE1MjM3MDg0ODQsImlzcyI6ImF1dG9tYXRldGhlcGxhbmV0LmNvbSIsImF1ZCI6ImF1dG9tYXRldGhlcGxhbmV0LmNvbSJ9.Nq6OXqrK82KSmWNrpcokRIWYrXHanpinrqwbUlKT_cs")]
public class UtilitiesTests : APITest
{
    private Fixture _fixture;

    protected override void ClassInitialize()
    {
        _fixture = new Fixture();
        App.ApiClient.WrappedClient.AddDefaultHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
    }

    [Test]
    [Ignore("Ignore until we implement head request for media store")]
    public async Task HeadRequestExample()
    {
        var request = new RestRequest("api/Albums", Method.Head);
        var response = await App.ApiClient.HeadAsync(request);

        Assert.That(response.IsSuccessful, Is.True);
    }

    [Test]
    [Ignore("Media store Api does not support get request with following parameters")]
    public async Task UpdateUserAgent()
    {
        var request = new RestRequest("api/Albums");
        request.AddQueryParameter("id", 12556);
        request.AddQueryParameter("shouldRegister", "True");
        request.AddQueryParameter("redirectUrl", "automatetheplanet");
        var response = await App.ApiClient.GetAsync(request);

        var request1 = new RestRequest("api/Albums");
        request1.AddParameter("firstName", "Anton");
        request1.AddParameter("lastName", "Angelov");
        request1.AddParameter("company", "Automate The Planet");

        var response1 = await App.ApiClient.PostAsync(request1);

        Assert.That(response1.IsSuccessful, Is.True);
    }

    [Test]
    public async Task ContentPopulated_When_GetAlbums()
    {
        var request = new RestRequest("api/Albums", Method.Get);
        var response = await App.ApiClient.GetAsync(request);

        Assert.That(response.Content, Is.Not.Null);
    }

    [Test]
    public async Task DataPopulatedAsList_When_GetGenericAlbums()
    {
        var request = new RestRequest("api/Albums", Method.Get);
        var response = await App.ApiClient.GetAsync<List<Album>>(request);

        Assert.That(response.Data.Count, Is.EqualTo(347));
    }

    [Test]
    public async Task DataPopulatedAsList_When_GetGenericAlbumsById()
    {
        var request = new RestRequest("api/Albums/10", Method.Get);
        var response = await App.ApiClient.GetAsync<Album>(request);

        Assert.That(response.Data.AlbumId, Is.EqualTo(10));
    }

    [Test]
    public async Task ContentPopulated_When_GetGenericAlbumsById()
    {
        var request = new RestRequest("api/Albums/10", Method.Get);
        var response = await App.ApiClient.GetAsync<Album>(request);

        Assert.That(response.Content, Is.Not.Null);
    }

    [Test]
    public async Task DataPopulatedAsGenres_When_PutModifiedContent()
    {
        var newGenres = await CreateUniqueGenres();

        var request = new RestRequest("api/Genres", Method.Post);
        request.AddJsonBody(newGenres);

        var insertedGenres = await App.ApiClient.PostAsync<Genre>(request);

        var putRequest = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}", Method.Put);
        string updatedName = Guid.NewGuid().ToString();
        insertedGenres.Data.Name = updatedName;
        putRequest.AddJsonBody(insertedGenres.Data);

        await App.ApiClient.PutAsync<Genre>(putRequest);

        request = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}", Method.Get);
        var getUpdatedResponse = await App.ApiClient.GetAsync<Genre>(request);

        Assert.That(getUpdatedResponse.Content, Is.Not.Null);
    }

    [Test]
    public async Task ContentPopulated_When_PutModifiedContent()
    {
        var newGenres = await CreateUniqueGenres();

        var request = new RestRequest("api/Genres", Method.Post);
        request.AddJsonBody(newGenres);

        var insertedGenres = await App.ApiClient.PostAsync<Genre>(request);

        var putRequest = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}", Method.Put);
        string updatedName = Guid.NewGuid().ToString();
        insertedGenres.Data.Name = updatedName;
        putRequest.AddJsonBody(insertedGenres.Data);

        await App.ApiClient.PutAsync(putRequest);

        request = new RestRequest($"api/Genres/{insertedGenres.Data.GenreId}", Method.Get);
        var getUpdatedResponse = await App.ApiClient.GetAsync<Genre>(request);

        Assert.That(getUpdatedResponse.Content, Is.Not.Null);
    }

    [Test]
    public async Task ContentPopulated_When_NewAlbumInsertedViaPost()
    {
        var newAlbum = await CreateUniqueGenres();

        var request = new RestRequest("api/Genres");
        request.AddJsonBody(newAlbum);

        var response = await App.ApiClient.PostAsync(request);

        Assert.That(response.IsSuccessful, Is.True);
    }

    [Test]
    public async Task DataPopulatedAsGenres_When_NewAlbumInsertedViaPost()
    {
        var newAlbum = await CreateUniqueGenres();

        var request = new RestRequest("api/Genres", Method.Post);
        request.AddJsonBody(newAlbum);

        var response = await App.ApiClient.PostAsync<Genre>(request);

        Assert.That(response.Data.Name, Is.EqualTo(newAlbum.Name));
    }

    [Test]
    public async Task ArtistsDeleted_When_PerformDeleteRequest()
    {
        var newArtist = await CreateUniqueArtists();
        var request = new RestRequest("api/Artists", Method.Post);
        request.AddJsonBody(newArtist);
        await App.ApiClient.PostAsync<Artist>(request);

        var deleteRequest = new RestRequest($"api/Artists/{newArtist.ArtistId}", Method.Delete);
        var response = await App.ApiClient.DeleteAsync(deleteRequest);

        Assert.That(response.IsSuccessful, Is.True);
    }

    [Test]
    public async Task ArtistsDeleted_When_PerformGenericDeleteRequest()
    {
        var newArtist = await CreateUniqueArtists();
        var request = new RestRequest("api/Artists", Method.Post);
        request.AddJsonBody(newArtist);
        await App.ApiClient.PostAsync<Artist>(request);

        var deleteRequest = new RestRequest($"api/Artists/{newArtist.ArtistId}", Method.Delete);
        var response = await App.ApiClient.PostAsync<Artist>(deleteRequest);

        Assert.That(response.IsSuccessful, Is.True);
    }

    private async Task<Artist> CreateUniqueArtists()
    {
        var artists = await App.ApiClient.GetAsync<List<Artist>>(new RestRequest("api/Artists"));
        var newArtists = new Artist
        {
            Name = Guid.NewGuid().ToString(),
            ArtistId = artists.Data.OrderBy(x => x.ArtistId).Last().ArtistId + 1,
        };
        return newArtists;
    }

    private async Task<Genre> CreateUniqueGenres()
    {
        var genres = await App.ApiClient.GetAsync<List<Genre>>(new RestRequest("api/Genres"));
        var newGenres = new Genre
        {
            Name = Guid.NewGuid().ToString(),
            GenreId = genres.Data.OrderBy(x => x.GenreId).Last().GenreId + 1,
        };
        return newGenres;
    }
}
