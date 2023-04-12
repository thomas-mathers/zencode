using System.Text.RegularExpressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;

namespace ZenCode.Lexer;

public class RegexTokenMatcher : ITokenMatcher
{
    private readonly Regex _regex;

    public RegexTokenMatcher(TokenType tokenType, string pattern)
    {
        TokenType = tokenType;
        _regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.NonBacktracking);
    }

    public TokenType TokenType { get; }

    public string? Match(string input, int startingIndex)
    {
        var match = _regex.Match(input, startingIndex);

        if (!match.Success || match.Index != startingIndex)
        {
            return null;
        }

        return match.Value;
    }
}
