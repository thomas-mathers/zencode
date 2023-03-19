using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Tests.Grammar.Expressions;

public class LiteralExpressionTests
{
    [Theory]
    [InlineData("true", "true")]
    [InlineData("false", "false")]
    public void ToString_BooleanLiteral_ReturnsTokenText(string tokenText, string expected)
    {
        // Arrange
        var literalExpression = new LiteralExpression(new Token(TokenType.BooleanLiteral) { Text = tokenText });
        
        // Act
        var actual = literalExpression.ToString();
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData("0", "0")]
    [InlineData("1337", "1337")]
    public void ToString_IntegerLiteral_ReturnsTokenText(string tokenText, string expected)
    {
        // Arrange
        var literalExpression = new LiteralExpression(new Token(TokenType.IntegerLiteral) { Text = tokenText });
        
        // Act
        var actual = literalExpression.ToString();
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData("0", "0")]
    [InlineData("0.999", "0.999")]
    [InlineData("12345.25", "12345.25")]
    public void ToString_FloatLiteral_ReturnsTokenText(string tokenText, string expected)
    {
        // Arrange
        var literalExpression = new LiteralExpression(new Token(TokenType.FloatLiteral) { Text = tokenText });
        
        // Act
        var actual = literalExpression.ToString();
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData("\"\"", "\"\"")]
    [InlineData("\"Hello World\"", "\"Hello World\"")]
    public void ToString_StringLiteral_ReturnsTokenText(string tokenText, string expected)
    {
        // Arrange
        var literalExpression = new LiteralExpression(new Token(TokenType.StringLiteral) { Text = tokenText });
        
        // Act
        var actual = literalExpression.ToString();
        
        // Assert
        Assert.Equal(expected, actual);
    }
}