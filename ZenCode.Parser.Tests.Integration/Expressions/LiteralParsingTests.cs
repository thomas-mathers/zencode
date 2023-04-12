using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Tests.Integration.Expressions;

public class LiteralParsingTests
{
    private readonly IParser _parser;

    public LiteralParsingTests()
    {
        _parser = new ParserFactory().Create();
    }
    
    [Theory]
    [InlineData(TokenType.BooleanLiteral)]
    [InlineData(TokenType.IntegerLiteral)]
    [InlineData(TokenType.FloatLiteral)]
    [InlineData(TokenType.StringLiteral)]
    public void ParseExpression_Literal_ReturnsLiteralExpression(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(tokenType)
            }
        );

        var expected = new LiteralExpression(new Token(tokenType));

        // Act
        var actual = _parser.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}
