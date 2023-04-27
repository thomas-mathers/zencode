using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Mappers;
using ZenCode.Parser.Tests.TestData;
using ZenCode.Tests.Common.Mocks;

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

        var binaryExpression = new BinaryExpression
        {
            Operator = TokenTypeToBinaryOperatorTypeMapper.Map(operatorToken),
            Left = lExpression,
            Right = rExpression
        };
        
        var expected = $"{{Expression}} {binaryExpression.Operator} {{Expression}}";

        // Act
        var actual = binaryExpression.ToString();

        // Expected
        Assert.Equal(expected, actual);
    }
}
