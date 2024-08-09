using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DemoSystemTests;


/// <summary>
/// 
/// </summary>
[DataContract]
public class UploadPrerunPayload
{
    /// <summary>
    /// If your script requires some reference to other file that needs to be present in our machines then you can upload multiple pre_run_file and download those files in your script using download API
    /// </summary>
    /// <value>If your script requires some reference to other file that needs to be present in our machines then you can upload multiple pre_run_file and download those files in your script using download API</value>
    [DataMember(Name = "pre_run_file", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "pre_run_file")]
    public byte[] PreRunFile { get; set; }

    /// <summary>
    /// Name of your pre run executable
    /// </summary>
    /// <value>Name of your pre run executable</value>
    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// script file that will revert the actions performed by pre run file. If there is no post action that needs to performed then you can upload an empty file
    /// </summary>
    /// <value>script file that will revert the actions performed by pre run file. If there is no post action that needs to performed then you can upload an empty file</value>
    [DataMember(Name = "post_run_file", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "post_run_file")]
    public byte[] PostRunFile { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class UploadPrerunPayload {\n");
        sb.Append("  PreRunFile: ").Append(PreRunFile).Append("\n");
        sb.Append("  Name: ").Append(Name).Append("\n");
        sb.Append("  PostRunFile: ").Append(PostRunFile).Append("\n");
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
