using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class ListsTestsResponseData
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
    /// Gets or Sets BuildName
    /// </summary>
    [DataMember(Name = "build_name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "build_name")]
    public string BuildName { get; set; }

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
    /// Gets or Sets StatusInd
    /// </summary>
    [DataMember(Name = "status_ind", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "status_ind")]
    public string StatusInd { get; set; }

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
    /// Gets or Sets Browser
    /// </summary>
    [DataMember(Name = "browser", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "browser")]
    public string Browser { get; set; }

    /// <summary>
    /// Gets or Sets Platform
    /// </summary>
    [DataMember(Name = "platform", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "platform")]
    public string Platform { get; set; }

    /// <summary>
    /// Gets or Sets Version
    /// </summary>
    [DataMember(Name = "version", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "version")]
    public string Version { get; set; }

    /// <summary>
    /// Gets or Sets Name
    /// </summary>
    [DataMember(Name = "name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets SessionId
    /// </summary>
    [DataMember(Name = "session_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "session_id")]
    public string SessionId { get; set; }

    /// <summary>
    /// Gets or Sets Device
    /// </summary>
    [DataMember(Name = "device", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "device")]
    public string Device { get; set; }

    /// <summary>
    /// Gets or Sets Duration
    /// </summary>
    [DataMember(Name = "duration", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "duration")]
    public string Duration { get; set; }

    /// <summary>
    /// Gets or Sets TestType
    /// </summary>
    [DataMember(Name = "test_type", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "test_type")]
    public string TestType { get; set; }

    /// <summary>
    /// Gets or Sets Tag
    /// </summary>
    [DataMember(Name = "tag", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "tag")]
    public List<string> Tag { get; set; }

    /// <summary>
    /// Gets or Sets Customdata
    /// </summary>
    [DataMember(Name = "customdata", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "customdata")]
    public Object Customdata { get; set; }

    /// <summary>
    /// Gets or Sets SeleniumLogs
    /// </summary>
    [DataMember(Name = "selenium_logs", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "selenium_logs")]
    public string SeleniumLogs { get; set; }

    /// <summary>
    /// Gets or Sets ConsoleLogs
    /// </summary>
    [DataMember(Name = "console_logs", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "console_logs")]
    public string ConsoleLogs { get; set; }

    /// <summary>
    /// Gets or Sets NetworkLogs
    /// </summary>
    [DataMember(Name = "network_logs", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "network_logs")]
    public string NetworkLogs { get; set; }

    /// <summary>
    /// Gets or Sets CommandLogs
    /// </summary>
    [DataMember(Name = "command_logs", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "command_logs")]
    public string CommandLogs { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class ListsTestsResponseData {\n");
        sb.Append("  TestId: ").Append(TestId).Append("\n");
        sb.Append("  BuildId: ").Append(BuildId).Append("\n");
        sb.Append("  BuildName: ").Append(BuildName).Append("\n");
        sb.Append("  UserId: ").Append(UserId).Append("\n");
        sb.Append("  Username: ").Append(Username).Append("\n");
        sb.Append("  StatusInd: ").Append(StatusInd).Append("\n");
        sb.Append("  CreateTimestamp: ").Append(CreateTimestamp).Append("\n");
        sb.Append("  StartTimestamp: ").Append(StartTimestamp).Append("\n");
        sb.Append("  EndTimestamp: ").Append(EndTimestamp).Append("\n");
        sb.Append("  Remark: ").Append(Remark).Append("\n");
        sb.Append("  Browser: ").Append(Browser).Append("\n");
        sb.Append("  Platform: ").Append(Platform).Append("\n");
        sb.Append("  Version: ").Append(Version).Append("\n");
        sb.Append("  Name: ").Append(Name).Append("\n");
        sb.Append("  SessionId: ").Append(SessionId).Append("\n");
        sb.Append("  Device: ").Append(Device).Append("\n");
        sb.Append("  Duration: ").Append(Duration).Append("\n");
        sb.Append("  TestType: ").Append(TestType).Append("\n");
        sb.Append("  Tag: ").Append(Tag).Append("\n");
        sb.Append("  Customdata: ").Append(Customdata).Append("\n");
        sb.Append("  SeleniumLogs: ").Append(SeleniumLogs).Append("\n");
        sb.Append("  ConsoleLogs: ").Append(ConsoleLogs).Append("\n");
        sb.Append("  NetworkLogs: ").Append(NetworkLogs).Append("\n");
        sb.Append("  CommandLogs: ").Append(CommandLogs).Append("\n");
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
