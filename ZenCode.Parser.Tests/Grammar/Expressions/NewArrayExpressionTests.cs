using Xunit;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.Parser.Tests.Grammar.Expressions;

public class NewArrayExpressionTests
{
    [Fact]
    public void ToString_OneIndexExpressions_ReturnsCorrectString()
    {
        // Arrange
        var newExpression = new NewArrayExpression
        {
            Type = new TypeMock(),
            Size = new ExpressionMock()
        };
        
        const string expected = "new {Type}[{Expression}]";

        // Act
        var actual = newExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}
