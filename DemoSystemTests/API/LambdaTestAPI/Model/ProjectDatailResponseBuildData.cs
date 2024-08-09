using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DemoSystemTests;


/// <summary>
/// 
/// </summary>
[DataContract]
public class ProjectDatailResponseBuildData
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
    /// Gets or Sets UpdateTimestamp
    /// </summary>
    [DataMember(Name = "update_timestamp", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "update_timestamp")]
    public string UpdateTimestamp { get; set; }

    /// <summary>
    /// Gets or Sets Test
    /// </summary>
    [DataMember(Name = "test", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "test")]
    public List<ProjectDatailResponseTestData> Test { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class ProjectDatailResponseBuildData {\n");
        sb.Append("  BuildId: ").Append(BuildId).Append("\n");
        sb.Append("  Name: ").Append(Name).Append("\n");
        sb.Append("  UserId: ").Append(UserId).Append("\n");
        sb.Append("  Username: ").Append(Username).Append("\n");
        sb.Append("  StatusInd: ").Append(StatusInd).Append("\n");
        sb.Append("  UpdateTimestamp: ").Append(UpdateTimestamp).Append("\n");
        sb.Append("  Test: ").Append(Test).Append("\n");
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
