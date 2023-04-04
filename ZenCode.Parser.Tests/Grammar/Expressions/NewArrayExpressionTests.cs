using Xunit;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.Mocks;

namespace ZenCode.Parser.Tests.Grammar.Expressions;

public class NewArrayExpressionTests
{
    [Fact]
    public void ToString_OneIndexExpressions_ReturnsCorrectString()
    {
        // Arrange
        var type = new TypeMock();
        var size = new ExpressionMock();
        var newExpression = new NewArrayExpression(type, size);
        const string expected = "new {Type}[{Expression}]";

        // Act
        var actual = newExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}