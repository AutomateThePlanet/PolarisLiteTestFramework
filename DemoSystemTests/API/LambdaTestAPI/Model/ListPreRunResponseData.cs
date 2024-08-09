using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DemoSystemTests;


/// <summary>
/// 
/// </summary>
[DataContract]
public class ListPreRunResponseData
{
    /// <summary>
    /// Name of the pre run
    /// </summary>
    /// <value>Name of the pre run</value>
    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

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
    /// capability url
    /// </summary>
    /// <value>capability url</value>
    [DataMember(Name = "capability_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "capability_url")]
    public string CapabilityUrl { get; set; }

    /// <summary>
    /// path of the file in lambda storage
    /// </summary>
    /// <value>path of the file in lambda storage</value>
    [DataMember(Name = "file_path", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "file_path")]
    public string FilePath { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class ListPreRunResponseData {\n");
        sb.Append("  Name: ").Append(Name).Append("\n");
        sb.Append("  LastModifiedAt: ").Append(LastModifiedAt).Append("\n");
        sb.Append("  Size: ").Append(Size).Append("\n");
        sb.Append("  CapabilityUrl: ").Append(CapabilityUrl).Append("\n");
        sb.Append("  FilePath: ").Append(FilePath).Append("\n");
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
