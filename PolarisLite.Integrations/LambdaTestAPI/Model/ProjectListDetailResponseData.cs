using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class ProjectListDetailResponseData
{
    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [DataMember(Name = "id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    /// <summary>
    /// Gets or Sets Name
    /// </summary>
    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets UserId
    /// </summary>
    [DataMember(Name = "userId", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "userId")]
    public int? UserId { get; set; }

    /// <summary>
    /// Gets or Sets Status
    /// </summary>
    [DataMember(Name = "status", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }

    /// <summary>
    /// Gets or Sets CreateTimestamp
    /// </summary>
    [DataMember(Name = "create_timestamp", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "create_timestamp")]
    public string CreateTimestamp { get; set; }

    /// <summary>
    /// Gets or Sets UpdateTimestamp
    /// </summary>
    [DataMember(Name = "update_timestamp", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "update_timestamp")]
    public string UpdateTimestamp { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class ProjectListDetailResponseData {\n");
        sb.Append("  Id: ").Append(Id).Append("\n");
        sb.Append("  Name: ").Append(Name).Append("\n");
        sb.Append("  UserId: ").Append(UserId).Append("\n");
        sb.Append("  Status: ").Append(Status).Append("\n");
        sb.Append("  CreateTimestamp: ").Append(CreateTimestamp).Append("\n");
        sb.Append("  UpdateTimestamp: ").Append(UpdateTimestamp).Append("\n");
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
