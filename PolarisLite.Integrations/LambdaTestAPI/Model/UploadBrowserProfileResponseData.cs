using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class UploadBrowserProfileResponseData
{
    /// <summary>
    /// error message if there is any error in uploading file. If file upload is success, then it will empty
    /// </summary>
    /// <value>error message if there is any error in uploading file. If file upload is success, then it will empty</value>
    [DataMember(Name = "error", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "error")]
    public string Error { get; set; }

    /// <summary>
    /// Gets or Sets Url
    /// </summary>
    [DataMember(Name = "url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }

    /// <summary>
    /// Gets or Sets Message
    /// </summary>
    [DataMember(Name = "message", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "message")]
    public string Message { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class UploadBrowserProfileResponseData {\n");
        sb.Append("  Error: ").Append(Error).Append("\n");
        sb.Append("  Url: ").Append(Url).Append("\n");
        sb.Append("  Message: ").Append(Message).Append("\n");
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
