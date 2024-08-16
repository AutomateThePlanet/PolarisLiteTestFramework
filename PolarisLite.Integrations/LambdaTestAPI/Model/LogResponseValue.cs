using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class LogResponseValue
{
    /// <summary>
    /// Gets or Sets RequestId
    /// </summary>
    [DataMember(Name = "requestId", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "requestId")]
    public string RequestId { get; set; }

    /// <summary>
    /// Gets or Sets RequestStartTime
    /// </summary>
    [DataMember(Name = "RequestStartTime", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "RequestStartTime")]
    public decimal? RequestStartTime { get; set; }

    /// <summary>
    /// Gets or Sets RequestMethod
    /// </summary>
    [DataMember(Name = "requestMethod", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "requestMethod")]
    public string RequestMethod { get; set; }

    /// <summary>
    /// Gets or Sets RequestPath
    /// </summary>
    [DataMember(Name = "requestPath", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "requestPath")]
    public string RequestPath { get; set; }

    /// <summary>
    /// Gets or Sets Duration
    /// </summary>
    [DataMember(Name = "duration", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "duration")]
    public decimal? Duration { get; set; }

    /// <summary>
    /// Gets or Sets RequestBody
    /// </summary>
    [DataMember(Name = "requestBody", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "requestBody")]
    public string RequestBody { get; set; }

    /// <summary>
    /// Gets or Sets ResponseBody
    /// </summary>
    [DataMember(Name = "responseBody", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "responseBody")]
    public string ResponseBody { get; set; }

    /// <summary>
    /// Gets or Sets ResponseStatus
    /// </summary>
    [DataMember(Name = "responseStatus", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "responseStatus")]
    public string ResponseStatus { get; set; }

    /// <summary>
    /// Gets or Sets ScreenshotId
    /// </summary>
    [DataMember(Name = "screenshotId", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "screenshotId")]
    public string ScreenshotId { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class LogResponseValue {\n");
        sb.Append("  RequestId: ").Append(RequestId).Append("\n");
        sb.Append("  RequestStartTime: ").Append(RequestStartTime).Append("\n");
        sb.Append("  RequestMethod: ").Append(RequestMethod).Append("\n");
        sb.Append("  RequestPath: ").Append(RequestPath).Append("\n");
        sb.Append("  Duration: ").Append(Duration).Append("\n");
        sb.Append("  RequestBody: ").Append(RequestBody).Append("\n");
        sb.Append("  ResponseBody: ").Append(ResponseBody).Append("\n");
        sb.Append("  ResponseStatus: ").Append(ResponseStatus).Append("\n");
        sb.Append("  ScreenshotId: ").Append(ScreenshotId).Append("\n");
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
