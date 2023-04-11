using Xunit;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.Mocks;

namespace ZenCode.Parser.Tests.Grammar.Expressions;

public class FunctionCallExpressionTests
{
    [Fact]
    public void ToString_NoParameters_ReturnsCorrectString()
    {
        // Arrange
        var functionCallExpression = new FunctionCallExpression(new ExpressionMock());
        const string expected = "{Expression}()";

        // Act
        var actual = functionCallExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_OneParameter_ReturnsCorrectString()
    {
        // Arrange
        var functionCallExpression = new FunctionCallExpression(new ExpressionMock())
        {
            Arguments = new ExpressionList
            {
                Expressions = new[]
                {
                    new ExpressionMock()
                }
            }
        };
        const string expected = "{Expression}({Expression})";

        // Act
        var actual = functionCallExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_ThreeParameters_ReturnsCorrectString()
    {
        // Arrange
        var functionCallExpression = new FunctionCallExpression(new ExpressionMock())
        {
            Arguments = new ExpressionList
            {
                Expressions = new[] { new ExpressionMock(), new ExpressionMock(), new ExpressionMock() }
            }
        };
        const string expected = "{Expression}({Expression}, {Expression}, {Expression})";

        // Act
        var actual = functionCallExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}