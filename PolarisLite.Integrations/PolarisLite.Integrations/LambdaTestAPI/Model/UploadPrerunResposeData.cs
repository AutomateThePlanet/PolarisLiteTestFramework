using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class UploadPrerunResposeData
{
    /// <summary>
    /// Gets or Sets File
    /// </summary>
    [DataMember(Name = "file", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "file")]
    public string File { get; set; }

    /// <summary>
    /// error message if there is any error in uploading file. If file upload is success, then it will empty
    /// </summary>
    /// <value>error message if there is any error in uploading file. If file upload is success, then it will empty</value>
    [DataMember(Name = "error", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "error")]
    public string Error { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class UploadPrerunResposeData {\n");
        sb.Append("  File: ").Append(File).Append("\n");
        sb.Append("  Error: ").Append(Error).Append("\n");
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