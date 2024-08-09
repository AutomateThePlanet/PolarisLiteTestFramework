using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DemoSystemTests;


/// <summary>
/// 
/// </summary>
[DataContract]
public class GetResolutionsResolutions
{
    /// <summary>
    /// Gets or Sets BigSur
    /// </summary>
    [DataMember(Name = "BigSur", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "BigSur")]
    public List<string> BigSur { get; set; }

    /// <summary>
    /// Gets or Sets Catalina
    /// </summary>
    [DataMember(Name = "Catalina", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "Catalina")]
    public List<string> Catalina { get; set; }

    /// <summary>
    /// Gets or Sets Windows11
    /// </summary>
    [DataMember(Name = "Windows11", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "Windows11")]
    public List<string> Windows11 { get; set; }

    /// <summary>
    /// Gets or Sets Windows10
    /// </summary>
    [DataMember(Name = "Windows10", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "Windows10")]
    public List<string> Windows10 { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class GetResolutionsResolutions {\n");
        sb.Append("  BigSur: ").Append(BigSur).Append("\n");
        sb.Append("  Catalina: ").Append(Catalina).Append("\n");
        sb.Append("  Windows11: ").Append(Windows11).Append("\n");
        sb.Append("  Windows10: ").Append(Windows10).Append("\n");
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
