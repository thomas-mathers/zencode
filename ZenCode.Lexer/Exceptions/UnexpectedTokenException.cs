using ZenCode.Lexer.Model;

namespace ZenCode.Lexer.Exceptions;

public class UnexpectedTokenException : Exception
{
    public UnexpectedTokenException(TokenType expectedTokenType, TokenType receivedTokenType) : base
    (
        $"Expected {expectedTokenType} but received {receivedTokenType}"
    )
    {
    }

    public UnexpectedTokenException(TokenType receivedTokenType) : base($"Unexpected token {receivedTokenType}")
    {
    }
}
