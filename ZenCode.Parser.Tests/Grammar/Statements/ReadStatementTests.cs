using AutoFixture;
using AutoFixture.Kernel;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.Parser.Tests.Grammar.Statements;

public class ReadStatementTests
{
    private readonly Fixture _fixture = new();

    public ReadStatementTests()
    {
        _fixture.Customizations.Add(new TypeRelay(typeof(Expression), typeof(ExpressionMock)));
    }

    [Fact]
    public void ToString_ReadIntoVariable_ReturnsCorrectString()
    {
        // Arrange
        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier, "x"));
        var readStatement = new ReadStatement(variableReferenceExpression);

        const string expected = "read x";

        // Act
        var actual = readStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_ReadIntoArray_ReturnsCorrectString()
    {
        // Arrange
        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier, "x"))
        {
            Indices = new ArrayIndexExpressionList
            {
                Expressions = _fixture.CreateMany<Expression>(3).ToArray()
            }
        };

        var readStatement = new ReadStatement(variableReferenceExpression);

        const string expected = "read x[{Expression}][{Expression}][{Expression}]";

        // Act
        var actual = readStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}
