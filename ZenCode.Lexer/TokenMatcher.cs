namespace ZenCode.Lexer;

public class TokenMatcher : ITokenMatcher
{
    private readonly string _pattern;

    public TokenType TokenType { get; }

    public TokenMatcher(TokenType type, string pattern)
    {
        TokenType = type;
        _pattern = pattern;
    }

    public string? Match(string input, int startingIndex)
    {
        var index = input.IndexOf(_pattern, startingIndex, StringComparison.OrdinalIgnoreCase);

        if (index != startingIndex)
        {
            return null;
        }

        return input.Substring(startingIndex, _pattern.Length);
    }
}
