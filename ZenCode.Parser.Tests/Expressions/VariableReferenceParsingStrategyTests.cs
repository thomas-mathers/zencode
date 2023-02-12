using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions;

namespace ZenCode.Parser.Tests.Expressions;

public class VariableReferenceParsingStrategyTests
{
    private readonly VariableReferenceParsingStrategy _sut = new();

    [Theory]
    [InlineData(TokenType.Assignment)]
    [InlineData(TokenType.Addition)]
    [InlineData(TokenType.Subtraction)]
    [InlineData(TokenType.Multiplication)]
    [InlineData(TokenType.Division)]
    [InlineData(TokenType.Modulus)]
    [InlineData(TokenType.Exponentiation)]
    [InlineData(TokenType.LessThan)]
    [InlineData(TokenType.LessThanOrEqual)]
    [InlineData(TokenType.Equals)]
    [InlineData(TokenType.NotEquals)]
    [InlineData(TokenType.GreaterThan)]
    [InlineData(TokenType.GreaterThanOrEqual)]
    [InlineData(TokenType.And)]
    [InlineData(TokenType.Or)]
    [InlineData(TokenType.Not)]
    [InlineData(TokenType.Boolean)]
    [InlineData(TokenType.Integer)]
    [InlineData(TokenType.Float)]
    [InlineData(TokenType.LeftParenthesis)]
    [InlineData(TokenType.RightParenthesis)]
    [InlineData(TokenType.Comma)]
    [InlineData(TokenType.LeftBracket)]
    [InlineData(TokenType.RightBracket)]
    public void Parse_FirstTokenNotIdentifier_ThrowsSyntaxError(TokenType tokenType)
    {
    }
}