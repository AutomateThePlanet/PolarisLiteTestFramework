using PolarisLite.API;
using RestSharp;

namespace PolarisLite.Integrations.LambdaTestAPI;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public class ProjectApiClient : IProjectApiClient
{
    private readonly ApiClientAdapter _ApiClientAdapter;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectApiClient"/> class.
    /// </summary>
    /// <param name="ApiClientAdapter">An instance of ApiClientAdapter (optional)</param>
    public ProjectApiClient(ApiClientAdapter ApiClientAdapter = null)
    {
        _ApiClientAdapter = ApiClientAdapter ?? throw new ArgumentNullException(nameof(ApiClientAdapter));
    }

    /// <summary>
    /// Get the details of a particular Project. Get the details of a particular Project in an Organisation.
    /// </summary>
    /// <param name="id">Project Id (unique in an organisation)</param>
    /// <param name="offset">It defines the number of lists on the basis of limit parameter. e.g offset=10</param>
    /// <param name="limit">To fetch specified number of records. e.g. limit=10</param>
    /// <param name="fromdate">To fetch the list of project builds that are created from the specified Start Date. The Date format must be YYYY-MM-DD. e.g. "2018-03-15".</param>
    /// <param name="todate">To fetch the list of project builds that are created before the specified End Date. If both fromdate and todate value provided then it works as range filter. The Date format must be YYYY-MM-DD. e.g. "2018-03-15".</param>
    /// <param name="sort">To sort the list in ascending or descending order using multiple keys. e.g. "asc.user_id,desc.org_id" default="desc.create_timestamp"</param>
    /// <returns>ProjectDetailResponse</returns>
    public async Task<MeasuredResponse<ProjectDetailResponse>> ProjectIdGetAsync(string id, int? offset, int? limit, string fromdate, string todate, string sort)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ApiException(400, "Missing required parameter 'id' when calling ProjectIdGet");
        }

        var request = new RestRequest($"/project/{id}", Method.Get);

        if (offset.HasValue)
        {
            request.AddParameter("offset", offset.Value);
        }

        if (limit.HasValue)
        {
            request.AddParameter("limit", limit.Value);
        }

        if (!string.IsNullOrEmpty(fromdate))
        {
            request.AddParameter("fromdate", fromdate);
        }

        if (!string.IsNullOrEmpty(todate))
        {
            request.AddParameter("todate", todate);
        }

        if (!string.IsNullOrEmpty(sort))
        {
            request.AddParameter("sort", sort);
        }

        var response = await _ApiClientAdapter.GetAsync<ProjectDetailResponse>(request);
        return response;
    }

    /// <summary>
    /// Update a Created Project at Lambdatest. You can update your Project name and status by providing Project Id.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="id">Project Id (unique in an organisation)</param>
    /// <returns>UpdateProjectResponse</returns>
    public async Task<MeasuredResponse<UpdateProjectResponse>> ProjectIdPutAsync(UpdateProjectPayload body, string id)
    {
        if (body == null)
        {
            throw new ApiException(400, "Missing required parameter 'body' when calling ProjectIdPut");
        }
        if (string.IsNullOrEmpty(id))
        {
            throw new ApiException(400, "Missing required parameter 'id' when calling ProjectIdPut");
        }

        var request = new RestRequest($"/project/{id}", Method.Put)
        {
            RequestFormat = DataFormat.Json
        };
        request.AddJsonBody(body);

        var response = await _ApiClientAdapter.PutAsync<UpdateProjectResponse>(request);
        return response;
    }

    /// <summary>
    /// Create a Project at Lambdatest. You can create your Project by providing Project Id and Name.
    /// </summary>
    /// <param name="body"></param>
    /// <returns>CreateProjectResponse</returns>
    public async Task<MeasuredResponse<CreateProjectResponse>> ProjectPostAsync(ProjectPayload body)
    {
        if (body == null)
        {
            throw new ApiException(400, "Missing required parameter 'body' when calling ProjectPost");
        }

        var request = new RestRequest("/project", Method.Post)
        {
            RequestFormat = DataFormat.Json
        };
        request.AddJsonBody(body);

        var response = await _ApiClientAdapter.PostAsync<CreateProjectResponse>(request);
        return response;
    }

    /// <summary>
    /// Get a list of Projects. Get a list of Active/Inactive Projects in an Organisation.
    /// </summary>
    /// <param name="offset">It defines the number of lists on the basis of limit parameter. e.g offset=10</param>
    /// <param name="limit">To fetch specified number of records. e.g. default limit=10; max=100</param>
    /// <param name="fromdate">To fetch the list of projects that are created from the specified Start Date. The Date format must be YYYY-MM-DD. e.g. "2018-03-15".</param>
    /// <param name="todate">To fetch the list of projects that are created before the specified End Date. If both fromdate and todate value provided then it works as range filter. The Date format must be YYYY-MM-DD. e.g. "2018-03-15".</param>
    /// <returns>ProjectListResponse</returns>
    public async Task<MeasuredResponse<ProjectListResponse>> ProjectsGetAsync(int? offset, int? limit, string fromdate, string todate)
    {
        var request = new RestRequest("/projects", Method.Get);

        if (offset.HasValue)
        {
            request.AddParameter("offset", offset.Value);
        }

        if (limit.HasValue)
        {
            request.AddParameter("limit", limit.Value);
        }

        if (!string.IsNullOrEmpty(fromdate))
        {
            request.AddParameter("fromdate", fromdate);
        }

        if (!string.IsNullOrEmpty(todate))
        {
            request.AddParameter("todate", todate);
        }

        var response = await _ApiClientAdapter.GetAsync<ProjectListResponse>(request);
        return response;
    }
}
