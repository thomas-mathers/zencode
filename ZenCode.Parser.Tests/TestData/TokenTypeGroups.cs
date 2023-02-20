using System.Collections;
using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Tests.TestData;

public static class TokenTypeGroups
{
    public static IEnumerable<TokenType> GetConstants()
    {
        yield return TokenType.Boolean;
        yield return TokenType.Integer;
        yield return TokenType.Float;
        yield return TokenType.String;
    }
    
    public static IEnumerable<TokenType> GetBinaryOperators()
    {
        yield return TokenType.Addition;
        yield return TokenType.Subtraction;
        yield return TokenType.Multiplication;
        yield return TokenType.Division;
        yield return TokenType.Modulus;
        yield return TokenType.Exponentiation;
        yield return TokenType.LessThan;
        yield return TokenType.LessThanOrEqual;
        yield return TokenType.Equals;
        yield return TokenType.NotEquals;
        yield return TokenType.GreaterThan;
        yield return TokenType.GreaterThanOrEqual;
        yield return TokenType.And;
        yield return TokenType.Or;
    }
    
    public static IEnumerable<TokenType> GetUnaryOperators()
    {
        yield return TokenType.Subtraction;
        yield return TokenType.Not;
    }
}