using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class ProjectDatailResponseTestData
{
    /// <summary>
    /// Gets or Sets UserId
    /// </summary>
    [DataMember(Name = "user_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "user_id")]
    public decimal? UserId { get; set; }

    /// <summary>
    /// Gets or Sets OrgId
    /// </summary>
    [DataMember(Name = "org_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "org_id")]
    public decimal? OrgId { get; set; }

    /// <summary>
    /// Gets or Sets StatusInd
    /// </summary>
    [DataMember(Name = "status_ind", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "status_ind")]
    public string StatusInd { get; set; }

    /// <summary>
    /// Gets or Sets Name
    /// </summary>
    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets TestId
    /// </summary>
    [DataMember(Name = "test_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "test_id")]
    public string TestId { get; set; }

    /// <summary>
    /// Gets or Sets StartTimestamp
    /// </summary>
    [DataMember(Name = "start_timestamp", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "start_timestamp")]
    public string StartTimestamp { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class ProjectDatailResponseTestData {\n");
        sb.Append("  UserId: ").Append(UserId).Append("\n");
        sb.Append("  OrgId: ").Append(OrgId).Append("\n");
        sb.Append("  StatusInd: ").Append(StatusInd).Append("\n");
        sb.Append("  Name: ").Append(Name).Append("\n");
        sb.Append("  TestId: ").Append(TestId).Append("\n");
        sb.Append("  StartTimestamp: ").Append(StartTimestamp).Append("\n");
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
