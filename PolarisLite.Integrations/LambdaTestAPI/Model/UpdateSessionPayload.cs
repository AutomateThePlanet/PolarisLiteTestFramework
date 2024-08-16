using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class UpdateSessionPayload
{
    /// <summary>
    /// Gets or Sets Name
    /// </summary>
    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets StatusInd
    /// </summary>
    [DataMember(Name = "status_ind", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "status_ind")]
    public string StatusInd { get; set; }

    /// <summary>
    /// Gets or Sets Reason
    /// </summary>
    [DataMember(Name = "reason", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "reason")]
    public string Reason { get; set; }

    /// <summary>
    /// Gets or Sets CustomData
    /// </summary>
    [DataMember(Name = "custom_data", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "custom_data")]
    public Object CustomData { get; set; }

    /// <summary>
    /// Gets or Sets Tags
    /// </summary>
    [DataMember(Name = "tags", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "tags")]
    public List<string> Tags { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class UpdateSessionPayload {\n");
        sb.Append("  Name: ").Append(Name).Append("\n");
        sb.Append("  StatusInd: ").Append(StatusInd).Append("\n");
        sb.Append("  Reason: ").Append(Reason).Append("\n");
        sb.Append("  CustomData: ").Append(CustomData).Append("\n");
        sb.Append("  Tags: ").Append(Tags).Append("\n");
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
