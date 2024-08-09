using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DemoSystemTests;


/// <summary>
/// 
/// </summary>
[DataContract]
public class ListExtensionResponseData
{
    /// <summary>
    /// Name of the extension
    /// </summary>
    /// <value>Name of the extension</value>
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
    /// url of the uploaded extension
    /// </summary>
    /// <value>url of the uploaded extension</value>
    [DataMember(Name = "s3_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "s3_url")]
    public string S3Url { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class ListExtensionResponseData {\n");
        sb.Append("  Key: ").Append(Key).Append("\n");
        sb.Append("  LastModifiedAt: ").Append(LastModifiedAt).Append("\n");
        sb.Append("  Size: ").Append(Size).Append("\n");
        sb.Append("  S3Url: ").Append(S3Url).Append("\n");
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
