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
        var variableDeclarationStatement = new VariableDeclarationStatement(identifier, _fixture.Create<Expression>());
        var iterator = _fixture.Create<Expression>();

        var assignmentStatement = new AssignmentStatement
        (
            new VariableReferenceExpression(identifier),
            _fixture.Create<Expression>()
        );

        var scope = new Scope();

        var forStatement = new ForStatement
        (
            variableDeclarationStatement,
            iterator,
            assignmentStatement,
            scope
        );

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
        var variableDeclarationStatement = new VariableDeclarationStatement(identifier, _fixture.Create<Expression>());
        var iterator = _fixture.Create<Expression>();

        var assignmentStatement = new AssignmentStatement
        (
            new VariableReferenceExpression(identifier),
            _fixture.Create<Expression>()
        );

        var scope = new Scope
        {
            Statements = _fixture.CreateMany<Statement>(3).ToArray()
        };

        var forStatement = new ForStatement
        (
            variableDeclarationStatement,
            iterator,
            assignmentStatement,
            scope
        );

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
