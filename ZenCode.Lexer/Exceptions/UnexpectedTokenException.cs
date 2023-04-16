using ZenCode.Lexer.Extensions;
using ZenCode.Lexer.Model;

namespace ZenCode.Lexer.Exceptions;

public class UnexpectedTokenException : LexerException
{
    public UnexpectedTokenException()
    {
        
    }
    
    public UnexpectedTokenException(Token receivedToken) : base
    (
        receivedToken.Line,
        receivedToken.StartingColumn,
        $"Unexpected token '{receivedToken.Type.GetText()}'"
    )
    {
    }
    
    public UnexpectedTokenException(Token receivedToken, TokenType expectedTokenType) : base
    (
        receivedToken.Line,
        receivedToken.StartingColumn,
        $"Expected '{expectedTokenType.GetText()}', got '{receivedToken.Type.GetText()}'"
    )
    {
    }
}
