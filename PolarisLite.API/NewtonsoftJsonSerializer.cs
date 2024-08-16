using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;

namespace PolarisLite.API;


public class NewtonsoftRestSerializer : IRestSerializer
{
    private readonly JsonSerializerSettings _jsonSerializerSettings;

    public NewtonsoftRestSerializer(JsonSerializerSettings jsonSerializerSettings)
    {
        _jsonSerializerSettings = jsonSerializerSettings;
        Serializer = new NewtonsoftJsonSerializer(_jsonSerializerSettings);
        Deserializer = new NewtonsoftJsonDeserializer(_jsonSerializerSettings);
    }

    public ISerializer Serializer { get; }

    public IDeserializer Deserializer { get; }

    public string[] AcceptedContentTypes { get; } =
    {
        "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
    };

    public SupportsContentType SupportsContentType => contentType =>
        contentType.Contains("json");

    public DataFormat DataFormat => DataFormat.Json;

    public string? Serialize(Parameter parameter)
    {
        return JsonConvert.SerializeObject(parameter.Value, _jsonSerializerSettings);
    }
}
public class NewtonsoftJsonSerializer : ISerializer
{
    private readonly JsonSerializerSettings _jsonSerializerSettings;

    public NewtonsoftJsonSerializer(JsonSerializerSettings jsonSerializerSettings)
    {
        _jsonSerializerSettings = jsonSerializerSettings;
    }

    public string ContentType { get => "application/json"; set => value = "application/json"; }

    public string Serialize(object obj)
    {
        return JsonConvert.SerializeObject(obj, _jsonSerializerSettings);
    }
}

public class NewtonsoftJsonDeserializer : IDeserializer
{
    private readonly JsonSerializerSettings _jsonSerializerSettings;

    public NewtonsoftJsonDeserializer(JsonSerializerSettings jsonSerializerSettings)
    {
        _jsonSerializerSettings = jsonSerializerSettings;
    }

    public T Deserialize<T>(RestResponse response)
    {
        return JsonConvert.DeserializeObject<T>(response.Content, _jsonSerializerSettings);
    }
}