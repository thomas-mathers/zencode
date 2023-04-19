using AutoFixture;
using AutoFixture.Kernel;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Tests.Mocks;

namespace ZenCode.Parser.Tests.Grammar.Statements;

public class ForStatementTests
{
    private readonly Fixture _fixture = new();

    public ForStatementTests()
    {
        _fixture.Customizations.Add(new TypeRelay(typeof(Statement), typeof(StatementMock)));
        _fixture.Customizations.Add(new TypeRelay(typeof(Expression), typeof(ExpressionMock)));
    }

    [Fact]
    public void ToString_ForStatementEmptyBlock_ReturnsCorrectString()
    {
        // Arrange
        var identifier = new Token(TokenType.Identifier, "i");

        var variableDeclarationStatement = new VariableDeclarationStatement
        {
            Name = identifier,
            Value = _fixture.Create<Expression>()
        };

        var iterator = _fixture.Create<Expression>();

        var assignmentStatement = new AssignmentStatement
        {
            Variable = new VariableReferenceExpression(identifier),
            Value = _fixture.Create<Expression>()
        };

        var forStatement = new ForStatement
        {
            Initializer = variableDeclarationStatement,
            Condition = iterator,
            Iterator = assignmentStatement
        };

        const string expected = """
        for (var i := {Expression}; {Expression}; i := {Expression})
        {
        }
        """;

        // Act
        var actual = forStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_ForStatement_ReturnsCorrectString()
    {
        // Arrange
        var identifier = new Token(TokenType.Identifier, "i");

        var variableDeclarationStatement = new VariableDeclarationStatement
        {
            Name = identifier,
            Value = _fixture.Create<Expression>()
        };

        var iterator = _fixture.Create<Expression>();

        var assignmentStatement = new AssignmentStatement
        {
            Variable = new VariableReferenceExpression(identifier),
            Value = _fixture.Create<Expression>()
        };

        var scope = new Scope
        {
            Statements = _fixture.CreateMany<Statement>(3).ToArray()
        };

        var forStatement = new ForStatement
        {
            Initializer = variableDeclarationStatement,
            Condition = iterator,
            Iterator = assignmentStatement,
            Body = scope
        };

        const string expected = """
        for (var i := {Expression}; {Expression}; i := {Expression})
        {
            {Statement}
            {Statement}
            {Statement}
        }
        """;

        // Act
        var actual = forStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}
