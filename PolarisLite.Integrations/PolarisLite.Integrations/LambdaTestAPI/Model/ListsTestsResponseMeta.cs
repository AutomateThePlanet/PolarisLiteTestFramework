using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PolarisLite.Integrations;


/// <summary>
/// 
/// </summary>
[DataContract]
public class ListsTestsResponseMeta
{
    /// <summary>
    /// Gets or Sets Attributes
    /// </summary>
    [DataMember(Name = "attributes", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "attributes")]
    public ListsTestsResponseMetaAttributes Attributes { get; set; }

    /// <summary>
    /// Gets or Sets ResultSet
    /// </summary>
    [DataMember(Name = "result_set", EmitDefaultValue = false)]
    [JsonProperty(PropertyName = "result_set")]
    public ListsTestsResponseMetaResultSet ResultSet { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class ListsTestsResponseMeta {\n");
        sb.Append("  Attributes: ").Append(Attributes).Append("\n");
        sb.Append("  ResultSet: ").Append(ResultSet).Append("\n");
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
