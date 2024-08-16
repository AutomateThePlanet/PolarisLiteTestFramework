using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class ListPrerunFileResponseMeta
{
    /// <summary>
    /// base download url path
    /// </summary>
    /// <value>base download url path</value>
    [DataMember(Name = "download_url", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "download_url")]
    public string DownloadUrl { get; set; }

    /// <summary>
    /// Gets or Sets OrgId
    /// </summary>
    [DataMember(Name = "org_id", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "org_id")]
    public decimal? OrgId { get; set; }

    /// <summary>
    /// Gets or Sets Total
    /// </summary>
    [DataMember(Name = "total", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "total")]
    public decimal? Total { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class ListPrerunFileResponseMeta {\n");
        sb.Append("  DownloadUrl: ").Append(DownloadUrl).Append("\n");
        sb.Append("  OrgId: ").Append(OrgId).Append("\n");
        sb.Append("  Total: ").Append(Total).Append("\n");
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
