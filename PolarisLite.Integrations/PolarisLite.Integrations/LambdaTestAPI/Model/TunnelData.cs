using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class TunnelData
{
    /// <summary>
    /// Gets or Sets Dns
    /// </summary>
    [DataMember(Name = "dns", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "dns")]
    public string Dns { get; set; }

    /// <summary>
    /// Gets or Sets Email
    /// </summary>
    [DataMember(Name = "email", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "email")]
    public string Email { get; set; }

    /// <summary>
    /// Gets or Sets Username
    /// </summary>
    [DataMember(Name = "username", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "username")]
    public string Username { get; set; }

    /// <summary>
    /// Gets or Sets SharedTunnel
    /// </summary>
    [DataMember(Name = "shared_tunnel", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "shared_tunnel")]
    public bool? SharedTunnel { get; set; }

    /// <summary>
    /// Gets or Sets FolderPath
    /// </summary>
    [DataMember(Name = "folder_path", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "folder_path")]
    public string FolderPath { get; set; }

    /// <summary>
    /// Gets or Sets LocalDomains
    /// </summary>
    [DataMember(Name = "local_domains", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "local_domains")]
    public string LocalDomains { get; set; }

    /// <summary>
    /// Gets or Sets OrgId
    /// </summary>
    [DataMember(Name = "org_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "org_id")]
    public int? OrgId { get; set; }

    /// <summary>
    /// Gets or Sets StartTimestamp
    /// </summary>
    [DataMember(Name = "start_timestamp", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "start_timestamp")]
    public string StartTimestamp { get; set; }

    /// <summary>
    /// Gets or Sets StatusInd
    /// </summary>
    [DataMember(Name = "status_ind", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "status_ind")]
    public string StatusInd { get; set; }

    /// <summary>
    /// Gets or Sets TunnelId
    /// </summary>
    [DataMember(Name = "tunnel_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "tunnel_id")]
    public int? TunnelId { get; set; }

    /// <summary>
    /// Gets or Sets TunnelName
    /// </summary>
    [DataMember(Name = "tunnel_name", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "tunnel_name")]
    public string TunnelName { get; set; }

    /// <summary>
    /// Gets or Sets UserId
    /// </summary>
    [DataMember(Name = "user_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "user_id")]
    public int? UserId { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class TunnelData {\n");
        sb.Append("  Dns: ").Append(Dns).Append("\n");
        sb.Append("  Email: ").Append(Email).Append("\n");
        sb.Append("  Username: ").Append(Username).Append("\n");
        sb.Append("  SharedTunnel: ").Append(SharedTunnel).Append("\n");
        sb.Append("  FolderPath: ").Append(FolderPath).Append("\n");
        sb.Append("  LocalDomains: ").Append(LocalDomains).Append("\n");
        sb.Append("  OrgId: ").Append(OrgId).Append("\n");
        sb.Append("  StartTimestamp: ").Append(StartTimestamp).Append("\n");
        sb.Append("  StatusInd: ").Append(StatusInd).Append("\n");
        sb.Append("  TunnelId: ").Append(TunnelId).Append("\n");
        sb.Append("  TunnelName: ").Append(TunnelName).Append("\n");
        sb.Append("  UserId: ").Append(UserId).Append("\n");
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
