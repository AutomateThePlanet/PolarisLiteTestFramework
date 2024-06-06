using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace PolarisLite.API;
public static class ApiAssertExtensions
{
    private static List<string> _xmlSchemaValidationErrors;

    // Assert.IsTrue(response.Content.Contains(contentPart), $"Request's Content did not contain {contentPart}. URL = {response.ResponseUri}");
    public static void AssertContentContains(this RestResponse response, string contentPart)
    {
        if (!response.Content.Contains(contentPart))
        {
            throw new AssertionException($"Request's Content did not contain {contentPart}. URL = {response.ResponseUri}");
        }
    }

    //Assert.IsFalse(response.Content.Contains(contentPart), $"Request's Content contained {contentPart}. URL = {response.ResponseUri}");
    public static void AssertContentNotContains(this RestResponse response, string contentPart)
    {
        if (response.Content.Contains(contentPart))
        {
            throw new AssertionException($"Request's Content contained {contentPart}. URL = {response.ResponseUri}");
        }
    }

    //Assert.AreEqual(content, response.Content, $"Request's Content was not equal to {content}. URL = {response.ResponseUri}");
    public static void AssertContentEquals(this RestResponse response, string content)
    {
        if (!response.Content.Equals(content))
        {
            throw new AssertionException($"Request's Content was not equal to {content}. URL = {response.ResponseUri}");
        }
    }

    // Assert.AreNotEqual(content, response.Content, $"Request's Content was equal to {content}. URL = {response.ResponseUri}");
    public static void AssertContentNotEquals(this RestResponse response, string content)
    {
        if (response.Content.Equals(content))
        {
            throw new AssertionException($"Request's Content was equal to {content}. URL = {response.ResponseUri}");
        }
    }

    // Assert.AreEqual(result, response.Data, $"Request's Data was not equal to {result}. URL = {response.ResponseUri}");
    public static void AssertResultEquals<TResultType>(this RestResponse<TResultType> response, TResultType result)
        where TResultType : IEquatable<TResultType>, new()
    {
        if (!response.Data.Equals(result))
        {
            throw new AssertionException($"Request's Data was not equal to {result}. URL = {response.ResponseUri}");
        }
    }

    // Assert.AreNotEqual(result, response.Data, $"Request's Data was equal to {result}. URL = {response.ResponseUri}");
    public static void AssertResultNotEquals<TResultType>(this RestResponse<TResultType> response, TResultType result)
        where TResultType : IEquatable<TResultType>, new()
    {
        if (response.Data.Equals(result))
        {
            throw new AssertionException($"Request's Data was equal to {result}. URL = {response.ResponseUri}");
        }
    }

    // Assert.IsTrue((int) response.StatusCode <= 200 && (int) response.StatusCode >= 299, $"Request's status code was not successful - {response.StatusCode}. URL = {response.ResponseUri}");
    public static void AssertSuccessStatusCode(this RestResponse response)
    {
        if ((int)response.StatusCode <= 200 && (int)response.StatusCode >= 299)
        {
            throw new AssertionException($"Request's status code was not successful - {response.StatusCode}. URL = {response.ResponseUri}");
        }
    }

    // Assert.IsTrue(response.StatusCode != statusCode, $"Request's status code {response.StatusCode} was not equal to {statusCode}. URL = {response.ResponseUri}");
    public static void AssertStatusCode(this RestResponse response, HttpStatusCode statusCode)
    {
        if (response.StatusCode != statusCode)
        {
            throw new AssertionException($"Request's status code {response.StatusCode} was not equal to {statusCode}. URL = {response.ResponseUri}");
        }
    }

    // Assert.IsTrue(headerParameter.Value.ToString() != headerExpectedValue, $"Response's header {headerName} with value {headerParameter.Value} was not equal to {headerExpectedValue}. URL = {response.ResponseUri}");
    public static void AssertResponseHeader(this RestResponse response, string headerName, string headerExpectedValue)
    {
        var headerParameter = response.Headers.FirstOrDefault(x => x.Name.ToLower().Equals(headerName.ToLower()));
        if (headerParameter == null)
        {
            throw new ArgumentNullException($"No header was present with name {headerName}");
        }

        if (headerParameter.Value.ToString() != headerExpectedValue)
        {
            throw new AssertionException($"Response's header {headerName} with value {headerParameter.Value} was not equal to {headerExpectedValue}. URL = {response.ResponseUri}");
        }
    }

    public static void AssertContentType(this RestResponse response, string expectedContentType)
    {
        Assert.AreEqual(expectedContentType, response.ContentType);
    }

    public static void AssertContentEncoding(this RestResponse response, string expectedContentEncoding)
    {
        Assert.AreEqual(expectedContentEncoding, response.ContentEncoding);
    }

    // Assert.IsTrue(response.Cookies.Any(x => x.Name.Equals(cookieName)), $"Response's cookie with name {cookieName} was not present. URL = {response.ResponseUri}");
    public static void AssertCookieExists(this RestResponse response, string cookieName)
    {
        if (!response.Cookies.Any(x => x.Name.Equals(cookieName)))
        {
            throw new AssertionException($"Response's cookie with name {cookieName} was not present. URL = {response.ResponseUri}");
        }
    }

    // Assert.AreEqual(cookieValue, response.Cookies.FirstOrDefault(x => x.Name.Equals(cookieName)).Value, $"Response's cookie with name {cookieName}={cookie.Value} was not equal to {cookieName}={cookieValue}. URL = {response.ResponseUri}");
    public static void AssertCookie(this RestResponse response, string cookieName, string cookieValue)
    {
        response.AssertCookieExists(cookieName);
        var cookie = response.Cookies.FirstOrDefault(x => x.Name.Equals(cookieName));
        if (!cookie.Value.Equals(cookieValue))
        {
            throw new AssertionException($"Response's cookie with name {cookieName}={cookie.Value} was not equal to {cookieName}={cookieValue}. URL = {response.ResponseUri}");
        }
    }

    public static void AssertSchema(this RestResponse response, string schemaContent)
    {
        if (response.Request.RequestFormat == DataFormat.Json)
        {
            AssertJsonSchema(response, schemaContent);
        }
        else
        {
            AssertXmlSchema(response, schemaContent);
        }
    }

    public static void AssertSchema(this RestResponse response, Uri schemaUri)
    {
        if (response.Request.RequestFormat == DataFormat.Json)
        {
            AssertJsonSchema(response, schemaUri.AbsoluteUri);
        }
        else
        {
            AssertXmlSchema(response, schemaUri);
        }
    }

    private static void AssertJsonSchema(RestResponse response, string schemaContent)
    {
        JSchema jsonSchema;

        try
        {
            jsonSchema = JSchema.Parse(schemaContent);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Schema is not valid schema", ex);
        }

        AssertJsonSchema(response, jsonSchema);
    }

    private static async Task AssertJsonSchemaAsync(RestResponse response, Uri schemaUri)
    {
        var client = new RestClient();
        var schemaResponse = await client.ExecuteAsync(new RestRequest(schemaUri));

        AssertJsonSchema(response, schemaResponse.Content);
    }

    private static void AssertJsonSchema(RestResponse response, JSchema jsonSchema)
    {
        IList<string> messages;

        var trimmedContent = response.Content.TrimStart();

        bool isSchemaValid =
            trimmedContent.StartsWith("{", StringComparison.Ordinal)
                ? JObject.Parse(response.Content).IsValid(jsonSchema, out messages)
                : JArray.Parse(response.Content).IsValid(jsonSchema, out messages);

        if (!isSchemaValid)
        {
            var sb = new StringBuilder();
            sb.AppendLine("JSON Schema is not valid. Error Messages:");
            foreach (var errorMessage in messages)
            {
                sb.AppendLine(errorMessage);
            }

            throw new AssertionException(sb.ToString());
        }
    }

    private static void AssertXmlSchema(RestResponse response, Uri schemaUri)
    {
        var schemaSet = new XmlSchemaSet();
        schemaSet.Add(string.Empty, schemaUri.ToString());

        AssertXmlSchema(response, schemaSet);
    }

    private static void AssertXmlSchema(RestResponse response, string schema)
    {
        var schemaSet = new XmlSchemaSet();
        schemaSet.Add(string.Empty, XmlReader.Create(new StringReader(schema)));

        AssertXmlSchema(response, schemaSet);
    }

    private static void AssertXmlSchema(RestResponse response, XmlSchemaSet xmlSchemaSet)
    {
        _xmlSchemaValidationErrors = new List<string>();

        var trimmedContent = response.Content.TrimStart();

        var xml = new XmlDocument();
        xml.LoadXml(trimmedContent);
        xml.Schemas.Add(xmlSchemaSet);

        xml.Validate(ValidationCallBack);

        if (_xmlSchemaValidationErrors.Any())
        {
            var sb = new StringBuilder();
            sb.AppendLine("XML Schema is not valid. Error Messages:");
            foreach (var errorMessage in _xmlSchemaValidationErrors)
            {
                sb.AppendLine(errorMessage);
            }

            throw new AssertionException(sb.ToString());
        }
    }

    private static void ValidationCallBack(object sender, System.Xml.Schema.ValidationEventArgs args)
    {
        if (args.Severity == XmlSeverityType.Warning)
        {
            _xmlSchemaValidationErrors.Add($"Warning: Matching schema not found. No validations occurred. {args.Message}");
        }
        else
        {
            _xmlSchemaValidationErrors.Add(args.Message);
        }
    }
}
