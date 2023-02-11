using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;

namespace ZenCode.Lexer;

public class TokenMatcher : ITokenMatcher
{
    private readonly string _pattern;

    public TokenMatcher(TokenType type, string pattern)
    {
        TokenType = type;
        _pattern = pattern;
    }

    public TokenType TokenType { get; }

    public string? Match(string input, int startingIndex)
    {
        var index = input.IndexOf(_pattern, startingIndex, StringComparison.OrdinalIgnoreCase);

        return index != startingIndex ? null : input.Substring(startingIndex, _pattern.Length);
    }
}