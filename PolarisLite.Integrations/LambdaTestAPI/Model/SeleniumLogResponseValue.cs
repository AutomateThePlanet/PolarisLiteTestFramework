using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class SeleniumLogResponseValue
{
    /// <summary>
    /// Gets or Sets Level
    /// </summary>
    [DataMember(Name = "level", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "level")]
    public string Level { get; set; }

    /// <summary>
    /// Gets or Sets Message
    /// </summary>
    [DataMember(Name = "message", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "message")]
    public string Message { get; set; }

    /// <summary>
    /// Gets or Sets Timestamp
    /// </summary>
    [DataMember(Name = "timestamp", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "timestamp")]
    public decimal? Timestamp { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class SeleniumLogResponseValue {\n");
        sb.Append("  Level: ").Append(Level).Append("\n");
        sb.Append("  Message: ").Append(Message).Append("\n");
        sb.Append("  Timestamp: ").Append(Timestamp).Append("\n");
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
