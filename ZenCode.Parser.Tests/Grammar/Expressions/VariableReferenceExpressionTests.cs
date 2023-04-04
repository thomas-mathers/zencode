using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.Mocks;

namespace ZenCode.Parser.Tests.Grammar.Expressions;

public class VariableReferenceExpressionTests
{
    [Fact]
    public void ToString_VariableReference_ReturnsCorrectString()
    {
        // Arrange
        const string identifierText = "x";
        var identifier = new Token(TokenType.Identifier) { Text = identifierText };
        var variableReferenceExpression = new VariableReferenceExpression(identifier);

        // Act
        var actual = variableReferenceExpression.ToString();

        // Assert
        Assert.Equal(identifierText, actual);
    }

    [Fact]
    public void ToString_ArrayReferenceSingleIndexExpression_ReturnsCorrectString()
    {
        // Arrange
        var identifier = new Token(TokenType.Identifier) { Text = "x" };
        var variableReferenceExpression = new VariableReferenceExpression(identifier)
        {
            Indices = new ArrayIndexExpressionList
            {
                Expressions = new[]
                {
                    new ExpressionMock()
                }
            }
        };
        const string expected = "x[{Expression}]";

        // Act
        var actual = variableReferenceExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_ArrayReferenceThreeIndexExpression_ReturnsCorrectString()
    {
        // Arrange
        var identifier = new Token(TokenType.Identifier) { Text = "x" };
        var variableReferenceExpression = new VariableReferenceExpression(identifier)
        {
            Indices = new ArrayIndexExpressionList
            {
                Expressions = new[]
                {
                    new ExpressionMock(),
                    new ExpressionMock(),
                    new ExpressionMock()
                }
            }
        };
        const string expected = "x[{Expression}][{Expression}][{Expression}]";

        // Act
        var actual = variableReferenceExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}