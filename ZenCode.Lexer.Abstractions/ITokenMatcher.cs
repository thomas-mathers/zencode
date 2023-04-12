using ZenCode.Lexer.Model;

namespace ZenCode.Lexer.Abstractions;

public interface ITokenMatcher
{
    TokenType TokenType { get; }
    string? Match(string input, int startingIndex);
}
