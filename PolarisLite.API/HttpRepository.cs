namespace PolarisLite.API;


public abstract class HttpRepository<TEntity> where TEntity : new()
{
    protected ApiClientService client;
    protected string entityEndpoint;

    public HttpRepository(string baseUrl, string entityEndpoint)
    {
        this.client = new ApiClientService(baseUrl);
        this.entityEndpoint = entityEndpoint;
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        var request = new RestRequest($"{entityEndpoint}/{id}", Method.Get);
        var response = await client.GetAsync<TEntity>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error fetching entity by ID: {response.ErrorMessage}");
        }

        return response.Data;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        var request = new RestRequest(entityEndpoint, Method.Get);
        var response = await client.GetAsync<List<TEntity>>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error fetching entities: {response.ErrorMessage}");
        }

        return response.Data;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var request = new RestRequest(entityEndpoint, Method.Post);
        request.AddBody(entity, ContentType.Json);
        var response = await client.PostAsync<TEntity>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error creating entity: {response.ErrorMessage}");
        }

        return response.Data;
    }

    public async Task<TEntity> UpdateAsync(int id, TEntity entity)
    {
        var request = new RestRequest($"{entityEndpoint}/{id}", Method.Put);
        request.AddBody(entity, ContentType.Json);
        var response = await client.PutAsync<TEntity>(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error updating entity: {response.ErrorMessage}");
        }

        return response.Data;
    }

    public async Task DeleteAsync(int id)
    {
        var request = new RestRequest($"{entityEndpoint}/{id}", Method.Delete);
        var response = await client.DeleteAsync(request);

        if (!response.IsSuccessful)
        {
            throw new ApplicationException($"Error deleting entity: {response.ErrorMessage}");
        }
    }

    // Implement other necessary methods (e.g., aggregate queries) as needed
}

