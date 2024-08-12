using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class ListsTestsResponseMetaResultSet
{
    /// <summary>
    /// Gets or Sets Count
    /// </summary>
    [DataMember(Name = "count", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "count")]
    public decimal? Count { get; set; }

    /// <summary>
    /// Gets or Sets Limit
    /// </summary>
    [DataMember(Name = "limit", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "limit")]
    public decimal? Limit { get; set; }

    /// <summary>
    /// Gets or Sets Offset
    /// </summary>
    [DataMember(Name = "offset", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "offset")]
    public decimal? Offset { get; set; }

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
        sb.Append("class ListsTestsResponseMetaResultSet {\n");
        sb.Append("  Count: ").Append(Count).Append("\n");
        sb.Append("  Limit: ").Append(Limit).Append("\n");
        sb.Append("  Offset: ").Append(Offset).Append("\n");
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
