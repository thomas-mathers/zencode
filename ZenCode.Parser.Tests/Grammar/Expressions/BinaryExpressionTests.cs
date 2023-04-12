using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.Mocks;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Grammar.Expressions;

public class BinaryExpressionTests
{
    [Theory]
    [ClassData(typeof(BinaryOperators))]
    public void ToString_ExpressionMockOpExpressionMock_ReturnsCorrectString(TokenType operatorToken)
    {
        // Arrange
        var lExpression = new ExpressionMock();
        var rExpression = new ExpressionMock();
        var op = new Token(operatorToken);
        var binaryExpression = new BinaryExpression(lExpression, op, rExpression);
        var expected = $"{{Expression}} {operatorToken} {{Expression}}";

        // Act
        var actual = binaryExpression.ToString();

        // Expected
        Assert.Equal(expected, actual);
    }
}
