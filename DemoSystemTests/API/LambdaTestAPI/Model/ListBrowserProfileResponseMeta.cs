using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DemoSystemTests;


/// <summary>
/// 
/// </summary>
[DataContract]
public class ListBrowserProfileResponseMeta
{
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
        sb.Append("class ListBrowserProfileResponseMeta {\n");
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
