namespace PolarisLite;

public static class StringExtensions
{
    public static string MakeFirstLetterToLower(this string text)
    {
        return char.ToLower(text[0]) + text.Substring(1);
    }

    public static string RemoveSpaces(this string value)
    {
        var words = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < words.Length; i++)
        {
            words[i] = words[i][0] + words[i].Substring(1).ToLower();
        }
        return string.Join("", words);
    }

    public static string RemoveSpacesAndCapitalize(this string value)
    {
        var words = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < words.Length; i++)
        {
            words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
        }
        return string.Join("", words);
    }

    public static string Capitalize(this string value)
    {
        // Capitalizes the first letter and makes the rest lowercase
        return string.IsNullOrEmpty(value) ? value : char.ToUpper(value[0]) + value.Substring(1).ToLower();
    }
}
