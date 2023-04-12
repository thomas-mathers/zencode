using ZenCode.Lexer.Extensions;
using ZenCode.Lexer.Model;

namespace ZenCode.Lexer.Exceptions;

public class UnexpectedTokenException : Exception
{
    public UnexpectedTokenException(TokenType expectedTokenType, TokenType receivedTokenType) : base
    (
        $"Expected '{expectedTokenType.GetText()}', got '{receivedTokenType.GetText()}'"
    )
    {
    }

    public UnexpectedTokenException(TokenType receivedTokenType) : base
        ($"Unexpected token '{receivedTokenType.GetText()}'")
    {
    }
    
    public UnexpectedTokenException() : base
        ($"Unexpected token EOF")
    {
    }
}
