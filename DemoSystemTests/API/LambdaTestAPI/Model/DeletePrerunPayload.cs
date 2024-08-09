using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DemoSystemTests;


/// <summary>
/// 
/// </summary>
[DataContract]
public class DeletePrerunPayload
{
    /// <summary>
    /// file path of pre run file in our lambda storage. To delete a pre run, you can either specify pre_run or post_run file path. You can get file_path from the GET /files API
    /// </summary>
    /// <value>file path of pre run file in our lambda storage. To delete a pre run, you can either specify pre_run or post_run file path. You can get file_path from the GET /files API</value>
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
        sb.Append("class DeletePrerunPayload {\n");
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