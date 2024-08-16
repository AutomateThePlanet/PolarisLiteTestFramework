using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class SessionData
{
    /// <summary>
    /// Gets or Sets TestId
    /// </summary>
    [DataMember(Name = "test_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "test_id")]
    public string TestId { get; set; }

    /// <summary>
    /// Gets or Sets BuildId
    /// </summary>
    [DataMember(Name = "build_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "build_id")]
    public int? BuildId { get; set; }

    /// <summary>
    /// Gets or Sets Name
    /// </summary>
    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets UserId
    /// </summary>
    [DataMember(Name = "user_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "user_id")]
    public int? UserId { get; set; }

    /// <summary>
    /// Gets or Sets Username
    /// </summary>
    [DataMember(Name = "username", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "username")]
    public string Username { get; set; }

    /// <summary>
    /// Gets or Sets Duration
    /// </summary>
    [DataMember(Name = "duration", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "duration")]
    public int? Duration { get; set; }

    /// <summary>
    /// Gets or Sets Platform
    /// </summary>
    [DataMember(Name = "platform", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "platform")]
    public string Platform { get; set; }

    /// <summary>
    /// Gets or Sets Browser
    /// </summary>
    [DataMember(Name = "browser", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "browser")]
    public string Browser { get; set; }

    /// <summary>
    /// Gets or Sets BrowserVersion
    /// </summary>
    [DataMember(Name = "browser_version", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "browser_version")]
    public string BrowserVersion { get; set; }

    /// <summary>
    /// Gets or Sets Device
    /// </summary>
    [DataMember(Name = "device", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "device")]
    public string Device { get; set; }

    /// <summary>
    /// Gets or Sets StatusInd
    /// </summary>
    [DataMember(Name = "status_ind", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "status_ind")]
    public string StatusInd { get; set; }

    /// <summary>
    /// Gets or Sets SessionId
    /// </summary>
    [DataMember(Name = "session_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "session_id")]
    public string SessionId { get; set; }

    /// <summary>
    /// Gets or Sets BuildName
    /// </summary>
    [DataMember(Name = "build_name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "build_name")]
    public string BuildName { get; set; }

    /// <summary>
    /// Gets or Sets CreateTimestamp
    /// </summary>
    [DataMember(Name = "create_timestamp", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "create_timestamp")]
    public string CreateTimestamp { get; set; }

    /// <summary>
    /// Gets or Sets StartTimestamp
    /// </summary>
    [DataMember(Name = "start_timestamp", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "start_timestamp")]
    public string StartTimestamp { get; set; }

    /// <summary>
    /// Gets or Sets EndTimestamp
    /// </summary>
    [DataMember(Name = "end_timestamp", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "end_timestamp")]
    public string EndTimestamp { get; set; }

    /// <summary>
    /// Gets or Sets Remark
    /// </summary>
    [DataMember(Name = "remark", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "remark")]
    public string Remark { get; set; }

    /// <summary>
    /// Gets or Sets ConsoleLogsUrl
    /// </summary>
    [DataMember(Name = "console_logs_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "console_logs_url")]
    public string ConsoleLogsUrl { get; set; }

    /// <summary>
    /// Gets or Sets NetworkLogsUrl
    /// </summary>
    [DataMember(Name = "network_logs_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "network_logs_url")]
    public string NetworkLogsUrl { get; set; }

    /// <summary>
    /// Gets or Sets CommandLogsUrl
    /// </summary>
    [DataMember(Name = "command_logs_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "command_logs_url")]
    public string CommandLogsUrl { get; set; }

    /// <summary>
    /// Gets or Sets SeleniumLogsUrl
    /// </summary>
    [DataMember(Name = "selenium_logs_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "selenium_logs_url")]
    public string SeleniumLogsUrl { get; set; }

    /// <summary>
    /// Gets or Sets ScreenshotUrl
    /// </summary>
    [DataMember(Name = "screenshot_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "screenshot_url")]
    public string ScreenshotUrl { get; set; }

    /// <summary>
    /// Gets or Sets VideoUrl
    /// </summary>
    [DataMember(Name = "video_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "video_url")]
    public string VideoUrl { get; set; }

    /// <summary>
    /// Gets or Sets CustomData
    /// </summary>
    [DataMember(Name = "customData", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "customData")]
    public Object CustomData { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class SessionData {\n");
        sb.Append("  TestId: ").Append(TestId).Append("\n");
        sb.Append("  BuildId: ").Append(BuildId).Append("\n");
        sb.Append("  Name: ").Append(Name).Append("\n");
        sb.Append("  UserId: ").Append(UserId).Append("\n");
        sb.Append("  Username: ").Append(Username).Append("\n");
        sb.Append("  Duration: ").Append(Duration).Append("\n");
        sb.Append("  Platform: ").Append(Platform).Append("\n");
        sb.Append("  Browser: ").Append(Browser).Append("\n");
        sb.Append("  BrowserVersion: ").Append(BrowserVersion).Append("\n");
        sb.Append("  Device: ").Append(Device).Append("\n");
        sb.Append("  StatusInd: ").Append(StatusInd).Append("\n");
        sb.Append("  SessionId: ").Append(SessionId).Append("\n");
        sb.Append("  BuildName: ").Append(BuildName).Append("\n");
        sb.Append("  CreateTimestamp: ").Append(CreateTimestamp).Append("\n");
        sb.Append("  StartTimestamp: ").Append(StartTimestamp).Append("\n");
        sb.Append("  EndTimestamp: ").Append(EndTimestamp).Append("\n");
        sb.Append("  Remark: ").Append(Remark).Append("\n");
        sb.Append("  ConsoleLogsUrl: ").Append(ConsoleLogsUrl).Append("\n");
        sb.Append("  NetworkLogsUrl: ").Append(NetworkLogsUrl).Append("\n");
        sb.Append("  CommandLogsUrl: ").Append(CommandLogsUrl).Append("\n");
        sb.Append("  SeleniumLogsUrl: ").Append(SeleniumLogsUrl).Append("\n");
        sb.Append("  ScreenshotUrl: ").Append(ScreenshotUrl).Append("\n");
        sb.Append("  VideoUrl: ").Append(VideoUrl).Append("\n");
        sb.Append("  CustomData: ").Append(CustomData).Append("\n");
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
