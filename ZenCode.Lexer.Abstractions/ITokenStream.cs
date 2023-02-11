using ZenCode.Lexer.Model;

namespace ZenCode.Lexer.Abstractions;

public interface ITokenStream : IEnumerable<Token>
{
    Token Consume(TokenType tokenType);
    Token Consume();
    Token? Peek(byte numTokens);
    bool Match(TokenType tokenType);
}