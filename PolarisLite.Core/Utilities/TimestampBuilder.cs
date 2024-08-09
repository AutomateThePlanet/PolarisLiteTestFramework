namespace PolarisLite.Utilities;
public class TimestampBuilder
{
    public static string BuildUniqueText(string text)
    {
        var newTimestamp = GenerateUniqueText();
        var result = string.Concat(text, newTimestamp);
        return result;
    }

    public static string GenerateUniqueText()
    {
        var newTimestamp = DateTime.Now.ToString("MM-dd-yyyy-hh-mm-ss-ffff");
        return newTimestamp;
    }

    public static string GenerateUniqueUrl(string url)
    {
        var newTimestamp = $"{url}\\{DateTime.Now.ToString("MMMMddyyyyhhmmss")}";
        return newTimestamp;
    }
    public static string GenerateUniqueTextMonthNameOneWord()
    {
        var newTimestamp = DateTime.Now.ToString("MMMMddyyyyhhmmss");
        return newTimestamp;
    }

    public static string BuildUniqueEmail(string prefix, string sufix)
    {
        var result = string.Concat(prefix, "_", GenerateUniqueText(), "@", sufix, ".com");
        return result;
    }

    public static string BuildUniqueEmailTimestamp()
    {
        var result = $"test-{GenerateUniqueText()}@test.com";
        return result;
    }

    public static string BuildUniqueEmailGuid()
    {
        var result = $"test-{Guid.NewGuid()}@test.com";
        return result;
    }

    public static string BuildUniqueEmail(string prefix)
    {
        var result = $"{prefix}{GenerateUniqueText()}@test.com";
        return result;
    }

    public static string BuildUniqueEmail(char specialSymbol)
    {
        var result = $"test-{GenerateUniqueText()}{specialSymbol}@test.com";
        return result;
    }
}
