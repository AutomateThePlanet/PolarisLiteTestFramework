using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class GetOrgConcurrencyData
{
    /// <summary>
    /// Gets or Sets Queued
    /// </summary>
    [DataMember(Name = "queued", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "queued")]
    public int? Queued { get; set; }

    /// <summary>
    /// Gets or Sets Running
    /// </summary>
    [DataMember(Name = "running", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "running")]
    public int? Running { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class GetOrgConcurrencyData {\n");
        sb.Append("  Queued: ").Append(Queued).Append("\n");
        sb.Append("  Running: ").Append(Running).Append("\n");
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
