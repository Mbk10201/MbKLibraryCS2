using System.Text.RegularExpressions;

namespace MBKLibrary;

public class Stocks
{
    public static string GetColoredText(string message)
    {
        Dictionary<string, int> colorMap = new()
        {
            { "{default}", 1 },
            { "{white}", 1 },
            { "{darkred}", 2 },
            { "{lightpurple}", 3},
            { "{green}", 4 },
            { "{lightgreen}", 5 },
            { "{slimegreen}", 6 },
            { "{red}", 7 },
            { "{grey}", 8 },
            { "{yellow}", 9 },
            { "{invisible}", 10 },
            { "{lightblue}", 11 },
            { "{blue}", 12 },
            { "{purple}", 13 },
            { "{pink}", 14 },
            { "{fadedred}", 15 },
            { "{gold}", 16 },
            // No more colors are mapped to CS2
        };

        // Use a regular expression to find and replace color codes
        string pattern = "{(\\w+)}"; // Matches {word}
        string replaced = Regex.Replace(message, pattern, match =>
        {
            string colorCode = match.Groups[1].Value;
            if (colorMap.TryGetValue("{" + colorCode + "}", out int replacement))
            {
                // A little hack to get the color code to work
                return Convert.ToChar(replacement).ToString();
            }
            // If the color code is not found, leave it unchanged
            return match.Value;
        });
        // Non-breaking space - a little hack to get all colors to show
        return $"\xA0{replaced}";
    }

    /// <summary>
    /// Removes leading and trailing quotes from a given string.
    /// </summary>
    /// <param name="input">The string to strip quotes from.</param>
    /// <returns>The input string without leading or trailing quotes.</returns>
    public static string StripQuotes(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        // Check for leading and trailing quotes
        if (input.StartsWith("\"") && input.EndsWith("\""))
        {
            return input.Substring(1, input.Length - 2);
        }

        return input;
    }
}
