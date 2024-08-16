using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class UploadTerminalFileResposeData
{
    /// <summary>
    /// Gets or Sets Data
    /// </summary>
    [DataMember(Name = "data", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "data")]
    public string Data { get; set; }

    /// <summary>
    /// Gets or Sets Status
    /// </summary>
    [DataMember(Name = "status", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "status")]
    public string Status { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class UploadTerminalFileResposeData {\n");
        sb.Append("  Data: ").Append(Data).Append("\n");
        sb.Append("  Status: ").Append(Status).Append("\n");
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
