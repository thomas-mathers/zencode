using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions;

namespace ZenCode.Parser.Tests.Expressions;

public class ExpressionParserUnaryExpressionTests
{
    private readonly ExpressionParser _sut;

    public ExpressionParserUnaryExpressionTests()
    {
        _sut = new ExpressionParser();
    }

    [Theory]
    [InlineData(TokenType.Not, TokenType.Boolean)]
    [InlineData(TokenType.Not, TokenType.Integer)]
    [InlineData(TokenType.Not, TokenType.Float)]
    [InlineData(TokenType.Subtraction, TokenType.Boolean)]
    [InlineData(TokenType.Subtraction, TokenType.Integer)]
    [InlineData(TokenType.Subtraction, TokenType.Float)]
    public void Parse_UnaryExpression_ReturnsUnaryExpression(TokenType op, TokenType operand)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = op
            },
            new Token
            {
                Type = operand
            }
        });

        var expected = new UnaryExpression(
            new Token
            {
                Type = op
            },
            new ConstantExpression(new Token { Type = operand }));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}