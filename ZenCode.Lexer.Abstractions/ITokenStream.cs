using ZenCode.Lexer.Model;

namespace ZenCode.Lexer.Abstractions;

public interface ITokenStream : IEnumerable<Token>
{
    Token Current { get; }
    Token Consume(TokenType tokenTypes);
    Token Consume();
    Token? Peek(byte numTokens);
    bool Match(TokenType tokenType);
}