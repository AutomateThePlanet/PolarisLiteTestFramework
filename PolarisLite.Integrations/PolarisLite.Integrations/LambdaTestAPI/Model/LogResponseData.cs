using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class LogResponseData
{
    /// <summary>
    /// Gets or Sets LogType
    /// </summary>
    [DataMember(Name = "logType", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "logType")]
    public string LogType { get; set; }

    /// <summary>
    /// Gets or Sets TestID
    /// </summary>
    [DataMember(Name = "testID", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "testID")]
    public string TestID { get; set; }

    /// <summary>
    /// Gets or Sets Status
    /// </summary>
    [DataMember(Name = "status", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "status")]
    public decimal? Status { get; set; }

    /// <summary>
    /// Gets or Sets Timestamp
    /// </summary>
    [DataMember(Name = "timestamp", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "timestamp")]
    public decimal? Timestamp { get; set; }

    /// <summary>
    /// Gets or Sets Value
    /// </summary>
    [DataMember(Name = "Value", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "Value")]
    public LogResponseValue Value { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class LogResponseData {\n");
        sb.Append("  LogType: ").Append(LogType).Append("\n");
        sb.Append("  TestID: ").Append(TestID).Append("\n");
        sb.Append("  Status: ").Append(Status).Append("\n");
        sb.Append("  Timestamp: ").Append(Timestamp).Append("\n");
        sb.Append("  Value: ").Append(Value).Append("\n");
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
