using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Tests.Integration.Expressions;

public class UnaryExpressionParsingTests
{
    private readonly IParser _parser;

    public UnaryExpressionParsingTests()
    {
        _parser = new ParserFactory().Create();
    }

    [Fact]
    public void ParseExpression_UnaryExpression_ReturnsUnaryExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Minus),
                new Token(TokenType.IntegerLiteral)
            }
        );

        var expected = new UnaryExpression
            (new Token(TokenType.Minus), new LiteralExpression(new Token(TokenType.IntegerLiteral)));

        // Act
        var actual = _parser.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void ParseExpression_MissingExpression_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Minus)
            }
        );

        // Act
        var exception = Assert.Throws<EndOfTokenStreamException>(() => _parser.ParseExpression(tokenStream));

        // Assert
        Assert.NotNull(exception);
    }
}
