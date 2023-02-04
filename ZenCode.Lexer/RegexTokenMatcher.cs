﻿using System.Text.RegularExpressions;

namespace ZenCode.Lexer;

public class RegexTokenMatcher : ITokenMatcher
{
    private readonly Regex _regex;

    public TokenType TokenType { get; }

    public RegexTokenMatcher(TokenType tokenType, string pattern)
    {
        TokenType = tokenType;
        _regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.NonBacktracking);
    }

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
