using AutoFixture;
using AutoFixture.Kernel;
using Xunit;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Tests.Mocks;

namespace ZenCode.Parser.Tests.Grammar.Statements;

public class WhileStatementTests
{
    private readonly Fixture _fixture = new();

    public WhileStatementTests()
    {
        _fixture.Customizations.Add(new TypeRelay(typeof(Expression), typeof(ExpressionMock)));
        _fixture.Customizations.Add(new TypeRelay(typeof(Statement), typeof(StatementMock)));
    }

    [Fact]
    public void ToString_WhileStatementEmptyScope_ReturnsCorrectString()
    {
        // Arrange
        var conditionScope = new ConditionScope(_fixture.Create<Expression>(), new Scope());
        var whileStatement = new WhileStatement(conditionScope);

        const string expected = """
        while ({Expression})
        {
        }
        """;

        // Act
        var actual = whileStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_WhileStatement_ReturnsCorrectString()
    {
        // Arrange
        var conditionScope = new ConditionScope(
            _fixture.Create<Expression>(),
            new Scope
            {
                Statements = _fixture.CreateMany<Statement>(3).ToArray()
            });

        var whileStatement = new WhileStatement(conditionScope);

        const string expected = """
        while ({Expression})
        {
            {Statement}
            {Statement}
            {Statement}
        }
        """;

        // Act
        var actual = whileStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}