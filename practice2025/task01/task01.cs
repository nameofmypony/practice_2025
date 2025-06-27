namespace task01;
public static class StringExtensions
{
    public static bool IsPalindrome(this string input)
    {
        if (string.IsNullOrEmpty(input)) return false;

        string cleaned = new(input
            .Where(c => !char.IsPunctuation(c) && !char.IsWhiteSpace(c))
            .Select(char.ToLower)
            .ToArray());

        if (cleaned.Length == 0) return false;

        for (int i = 0; i < cleaned.Length; i++)
            if (cleaned[i] != cleaned[cleaned.Length - i - 1]) return false;
        
        return true;
    }
}