using Xunit;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.Parser.Tests.Grammar.Expressions;

public class UnaryExpressionTests
{
    [Fact]
    public void ToString_NotExpression_ReturnsCorrectString()
    {
        // Arrange
        var unaryExpression = new UnaryExpression
        {
            Operator = UnaryOperatorType.Not,
            Expression = new ExpressionMock()
        };
        
        const string expected = "Not {Expression}";

        // Act
        var actual = unaryExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}
