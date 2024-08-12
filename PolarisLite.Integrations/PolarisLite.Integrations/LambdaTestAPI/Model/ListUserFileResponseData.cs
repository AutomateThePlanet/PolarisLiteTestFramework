using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class ListUserFileResponseData
{
    /// <summary>
    /// Name of the file
    /// </summary>
    /// <value>Name of the file</value>
    [DataMember(Name = "key", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "key")]
    public string Key { get; set; }

    /// <summary>
    /// Gets or Sets LastModifiedAt
    /// </summary>
    [DataMember(Name = "last_modified_at", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "last_modified_at")]
    public string LastModifiedAt { get; set; }

    /// <summary>
    /// file size
    /// </summary>
    /// <value>file size</value>
    [DataMember(Name = "size", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "size")]
    public decimal? Size { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class ListUserFileResponseData {\n");
        sb.Append("  Key: ").Append(Key).Append("\n");
        sb.Append("  LastModifiedAt: ").Append(LastModifiedAt).Append("\n");
        sb.Append("  Size: ").Append(Size).Append("\n");
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
