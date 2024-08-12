using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class PlatformResponseData
{
    /// <summary>
    /// Gets or Sets Platform
    /// </summary>
    [DataMember(Name = "platform", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "platform")]
    public string Platform { get; set; }

    /// <summary>
    /// Gets or Sets Browsers
    /// </summary>
    [DataMember(Name = "browsers", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "browsers")]
    public List<BrowserResponseData> Browsers { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class PlatformResponseData {\n");
        sb.Append("  Platform: ").Append(Platform).Append("\n");
        sb.Append("  Browsers: ").Append(Browsers).Append("\n");
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
