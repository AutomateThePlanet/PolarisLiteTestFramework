using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class SingleBuildResponseData
{
    /// <summary>
    /// Gets or Sets BuildId
    /// </summary>
    [DataMember(Name = "build_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "build_id")]
    public decimal? BuildId { get; set; }

    /// <summary>
    /// Gets or Sets Name
    /// </summary>
    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets OrgId
    /// </summary>
    [DataMember(Name = "org_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "org_id")]
    public decimal? OrgId { get; set; }

    /// <summary>
    /// Gets or Sets UserId
    /// </summary>
    [DataMember(Name = "user_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "user_id")]
    public decimal? UserId { get; set; }

    /// <summary>
    /// Gets or Sets Username
    /// </summary>
    [DataMember(Name = "username", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "username")]
    public string Username { get; set; }

    /// <summary>
    /// Gets or Sets StatusInd
    /// </summary>
    [DataMember(Name = "status_ind", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "status_ind")]
    public string StatusInd { get; set; }

    /// <summary>
    /// Gets or Sets CreateTimestamp
    /// </summary>
    [DataMember(Name = "create_timestamp", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "create_timestamp")]
    public string CreateTimestamp { get; set; }

    /// <summary>
    /// Gets or Sets EndTimestamp
    /// </summary>
    [DataMember(Name = "end_timestamp", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "end_timestamp")]
    public string EndTimestamp { get; set; }

    /// <summary>
    /// Gets or Sets ProjectId
    /// </summary>
    [DataMember(Name = "project_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "project_id")]
    public decimal? ProjectId { get; set; }

    /// <summary>
    /// Gets or Sets Tags
    /// </summary>
    [DataMember(Name = "tags", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "tags")]
    public List<string> Tags { get; set; }

    /// <summary>
    /// Gets or Sets PublicUrl
    /// </summary>
    [DataMember(Name = "public_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "public_url")]
    public string PublicUrl { get; set; }

    /// <summary>
    /// Gets or Sets Duration
    /// </summary>
    [DataMember(Name = "duration", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "duration")]
    public decimal? Duration { get; set; }

    /// <summary>
    /// Gets or Sets DashboardUrl
    /// </summary>
    [DataMember(Name = "dashboard_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "dashboard_url")]
    public string DashboardUrl { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class SingleBuildResponseData {\n");
        sb.Append("  BuildId: ").Append(BuildId).Append("\n");
        sb.Append("  Name: ").Append(Name).Append("\n");
        sb.Append("  OrgId: ").Append(OrgId).Append("\n");
        sb.Append("  UserId: ").Append(UserId).Append("\n");
        sb.Append("  Username: ").Append(Username).Append("\n");
        sb.Append("  StatusInd: ").Append(StatusInd).Append("\n");
        sb.Append("  CreateTimestamp: ").Append(CreateTimestamp).Append("\n");
        sb.Append("  EndTimestamp: ").Append(EndTimestamp).Append("\n");
        sb.Append("  ProjectId: ").Append(ProjectId).Append("\n");
        sb.Append("  Tags: ").Append(Tags).Append("\n");
        sb.Append("  PublicUrl: ").Append(PublicUrl).Append("\n");
        sb.Append("  Duration: ").Append(Duration).Append("\n");
        sb.Append("  DashboardUrl: ").Append(DashboardUrl).Append("\n");
        sb.Append("}\n");
        return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
