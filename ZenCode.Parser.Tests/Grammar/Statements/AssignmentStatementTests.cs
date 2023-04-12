using AutoFixture;
using AutoFixture.Kernel;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Tests.Mocks;

namespace ZenCode.Parser.Tests.Grammar.Statements;

public class AssignmentStatementTests
{
    private readonly Fixture _fixture = new();

    public AssignmentStatementTests()
    {
        _fixture.Customizations.Add(new TypeRelay(typeof(Expression), typeof(ExpressionMock)));
        _fixture.Customizations.Add(new TypeRelay(typeof(Statement), typeof(StatementMock)));
    }

    [Fact]
    public void ToString_ArrayAssignment_ReturnsCorrectString()
    {
        // Arrange
        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier, "A"))
        {
            Indices = new ArrayIndexExpressionList
            {
                Expressions = _fixture.CreateMany<Expression>(3).ToArray()
            }
        };

        var assignmentStatement = new AssignmentStatement(variableReferenceExpression, _fixture.Create<Expression>());
        const string expected = "A[{Expression}][{Expression}][{Expression}] := {Expression}";

        // Act
        var actual = assignmentStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_VariableAssignment_ReturnsCorrectString()
    {
        // Arrange
        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier, "x"));
        var assignmentStatement = new AssignmentStatement(variableReferenceExpression, _fixture.Create<Expression>());
        const string expected = "x := {Expression}";

        // Act
        var actual = assignmentStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}
