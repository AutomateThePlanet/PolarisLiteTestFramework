namespace PolarisLite.API;


public abstract class HttpRepository<TEntity> 
    where TEntity : class
{
    protected ApiClientAdapter client;
    protected string entityEndpoint;

    public HttpRepository(ApiClientAdapter apiClientService, string baseUrl, string entityEndpoint)
    : this(baseUrl, entityEndpoint)
    {
        this.client = apiClientService;
    }

    public HttpRepository(string baseUrl, string entityEndpoint)
    {
        this.client = new ApiClientAdapter(baseUrl);
        this.entityEndpoint = entityEndpoint;
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        var request = new RestRequest($"{entityEndpoint}/{id}", Method.Get);
        var response = await client.GetAsync<TEntity>(request);

        return response.Data;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        var request = new RestRequest(entityEndpoint, Method.Get);
        var response = await client.GetAsync<List<TEntity>>(request);

        return response.Data;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var request = new RestRequest(entityEndpoint, Method.Post);
        request.AddBody(entity, ContentType.Json);
        var response = await client.PostAsync<TEntity>(request);

        return response.Data;
    }

    public async Task<TEntity> UpdateAsync(int id, TEntity entity)
    {
        var request = new RestRequest($"{entityEndpoint}/{id}", Method.Put);
        request.AddBody(entity, ContentType.Json);
        var response = await client.PutAsync<TEntity>(request);

        return response.Data;
    }

    public async Task DeleteAsync(int id)
    {
        var request = new RestRequest($"{entityEndpoint}/{id}", Method.Delete);
        var response = await client.DeleteAsync(request);
    }

    // Implement other necessary methods (e.g., aggregate queries) as needed
}

