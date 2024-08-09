using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DemoSystemTests;


/// <summary>
/// 
/// </summary>
[DataContract]
public class BrowserResponseData
{
    /// <summary>
    /// Gets or Sets BrowserName
    /// </summary>
    [DataMember(Name = "browser_name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "browser_name")]
    public string BrowserName { get; set; }

    /// <summary>
    /// Gets or Sets Version
    /// </summary>
    [DataMember(Name = "version", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "version")]
    public string Version { get; set; }

    /// <summary>
    /// Gets or Sets Type
    /// </summary>
    [DataMember(Name = "type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    /// <summary>
    /// Gets or Sets Slug
    /// </summary>
    [DataMember(Name = "slug", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "slug")]
    public string Slug { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class BrowserResponseData {\n");
        sb.Append("  BrowserName: ").Append(BrowserName).Append("\n");
        sb.Append("  Version: ").Append(Version).Append("\n");
        sb.Append("  Type: ").Append(Type).Append("\n");
        sb.Append("  Slug: ").Append(Slug).Append("\n");
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
