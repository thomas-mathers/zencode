using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions;

namespace ZenCode.Parser.Tests.Expressions;

public class ExpressionParserConstantExpressionTests
{
    private readonly ExpressionParser _sut;

    public ExpressionParserConstantExpressionTests()
    {
        _sut = new ExpressionParser();
    }

    [Fact]
    public void Parse_Boolean_ReturnsConstantExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Boolean
            }
        });

        var expected = new ConstantExpression(new Token
        {
            Type = TokenType.Boolean
        });

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_Integer_ReturnsConstantExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Integer
            }
        });

        var expected = new ConstantExpression(new Token
        {
            Type = TokenType.Integer
        });

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_Float_ReturnsConstantExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Float
            }
        });

        var expected = new ConstantExpression(new Token
        {
            Type = TokenType.Float
        });

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}