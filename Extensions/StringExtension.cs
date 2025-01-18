using System.Text.RegularExpressions;

namespace MBKLibrary.Extensions;

public static partial class StringExtension
{
    [GeneratedRegex(@"\{.*?\}|\p{C}")] public static partial Regex MyRegex();

    public static string RemoveCurlyBraceContent(this string message) =>
       MyRegex().Replace(message, string.Empty);
}
