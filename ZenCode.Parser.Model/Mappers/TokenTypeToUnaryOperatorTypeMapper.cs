using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;

namespace ZenCode.Parser.Model.Mappers;

public static class TokenTypeToUnaryOperatorTypeMapper
{
    public static UnaryOperatorType Map(TokenType tokenType)
    {
        return tokenType switch
        {
            TokenType.Minus => UnaryOperatorType.Negate,
            TokenType.Not => UnaryOperatorType.Not,
            _ => throw new ArgumentException()
        };
    }
}
