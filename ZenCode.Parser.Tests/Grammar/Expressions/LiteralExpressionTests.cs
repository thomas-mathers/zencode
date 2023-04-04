using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Tests.Grammar.Expressions;

public class LiteralExpressionTests
{
    [Theory]
    [InlineData(TokenType.BooleanLiteral, "true", "true")]
    [InlineData(TokenType.BooleanLiteral, "false", "false")]
    [InlineData(TokenType.IntegerLiteral, "0", "0")]
    [InlineData(TokenType.IntegerLiteral, "1337", "1337")]
    [InlineData(TokenType.FloatLiteral, "0", "0")]
    [InlineData(TokenType.FloatLiteral, "0.999", "0.999")]
    [InlineData(TokenType.FloatLiteral, "12345.25", "12345.25")]
    [InlineData(TokenType.StringLiteral, "\"\"", "\"\"")]
    [InlineData(TokenType.StringLiteral, "\"Hello World\"", "\"Hello World\"")]
    public void ToString_LiteralToken_ReturnsTokenText(TokenType tokenType, string tokenText, string expected)
    {
        // Arrange
        var literalExpression = new LiteralExpression(new Token(tokenType) { Text = tokenText });

        // Act
        var actual = literalExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}