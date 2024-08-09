using PolarisLite.API;

namespace DemoSystemTests;

/// <summary>
/// Represents a collection of functions to interact with the API endpoints
/// </summary>
public interface IProjectApiClient
{
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
    Task<MeasuredResponse<ProjectDetailResponse>> ProjectIdGetAsync(string id, int? offset, int? limit, string fromdate, string todate, string sort);

    /// <summary>
    /// Update a Created Project at Lambdatest. You can update your Project name and status by providing Project Id.
    /// </summary>
    /// <param name="body"></param>
    /// <param name="id">Project Id (unique in an organisation)</param>
    /// <returns>UpdateProjectResponse</returns>
    Task<MeasuredResponse<UpdateProjectResponse>> ProjectIdPutAsync(UpdateProjectPayload body, string id);

    /// <summary>
    /// Create a Project at Lambdatest. You can create your Project by providing Project Id and Name.
    /// </summary>
    /// <param name="body"></param>
    /// <returns>CreateProjectResponse</returns>
    Task<MeasuredResponse<CreateProjectResponse>> ProjectPostAsync(ProjectPayload body);

    /// <summary>
    /// Get a list of Projects. Get a list of Active/Inactive Projects in an Organisation.
    /// </summary>
    /// <param name="offset">It defines the number of lists on the basis of limit parameter. e.g offset=10</param>
    /// <param name="limit">To fetch specified number of records. e.g. default limit=10; max=100</param>
    /// <param name="fromdate">To fetch the list of projects that are created from the specified Start Date. The Date format must be YYYY-MM-DD. e.g. "2018-03-15".</param>
    /// <param name="todate">To fetch the list of projects that are created before the specified End Date. If both fromdate and todate value provided then it works as range filter. The Date format must be YYYY-MM-DD. e.g. "2018-03-15".</param>
    /// <returns>ProjectListResponse</returns>
    Task<MeasuredResponse<ProjectListResponse>> ProjectsGetAsync(int? offset, int? limit, string fromdate, string todate);
}
