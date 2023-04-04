using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.Mocks;

namespace ZenCode.Parser.Tests.Grammar.Expressions;

public class UnaryExpressionTests
{
    [Fact]
    public void ToString_NotExpression_ReturnsCorrectString()
    {
        // Arrange
        var unaryExpression = new UnaryExpression(new Token(TokenType.Not), new ExpressionMock());
        const string expected = "Not {Expression}";

        // Act
        var actual = unaryExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}