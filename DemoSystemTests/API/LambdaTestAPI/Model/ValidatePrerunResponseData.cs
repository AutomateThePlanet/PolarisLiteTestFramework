using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DemoSystemTests;


/// <summary>
/// 
/// </summary>
[DataContract]
public class ValidatePrerunResponseData
{
    /// <summary>
    /// Gets or Sets PostRunFilePath
    /// </summary>
    [DataMember(Name = "post_run_file_path", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "post_run_file_path")]
    public string PostRunFilePath { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class ValidatePrerunResponseData {\n");
        sb.Append("  PostRunFilePath: ").Append(PostRunFilePath).Append("\n");
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
