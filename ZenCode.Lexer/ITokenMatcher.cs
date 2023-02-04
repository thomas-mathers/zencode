namespace ZenCode.Lexer;

public interface ITokenMatcher
{
    TokenType TokenType { get; }
    string? Match(string input, int startingIndex);
}