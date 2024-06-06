namespace PolarisLite;

public static class StringExtensions
{
    public static string MakeFirstLetterToLower(this string text)
    {
        return char.ToLower(text[0]) + text.Substring(1);
    }
}
