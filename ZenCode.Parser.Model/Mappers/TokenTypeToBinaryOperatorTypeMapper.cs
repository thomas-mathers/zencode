using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;

namespace ZenCode.Parser.Model.Mappers;

public static class TokenTypeToBinaryOperatorTypeMapper
{
    public static BinaryOperatorType Map(TokenType tokenType)
    {
        return tokenType switch
        {
            TokenType.Plus => BinaryOperatorType.Addition,
            TokenType.Minus => BinaryOperatorType.Subtraction,
            TokenType.Multiplication => BinaryOperatorType.Multiplication,
            TokenType.Division => BinaryOperatorType.Division,
            TokenType.Modulus => BinaryOperatorType.Modulo,
            TokenType.Exponentiation => BinaryOperatorType.Power,
            TokenType.LessThan => BinaryOperatorType.LessThan,
            TokenType.LessThanOrEqual => BinaryOperatorType.LessThanOrEqual,
            TokenType.Equals => BinaryOperatorType.Equals,
            TokenType.NotEquals => BinaryOperatorType.NotEquals,
            TokenType.GreaterThan => BinaryOperatorType.GreaterThan,
            TokenType.GreaterThanOrEqual => BinaryOperatorType.GreaterThanOrEqual,
            TokenType.And => BinaryOperatorType.And,
            TokenType.Or => BinaryOperatorType.Or,
            _ => throw new ArgumentException()
        };
    }
}
