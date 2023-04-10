using AutoFixture;
using AutoFixture.Kernel;
using Xunit;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Tests.Mocks;

namespace ZenCode.Parser.Tests.Grammar.Statements;

public class IfStatementTests
{
    private readonly Fixture _fixture = new();

    public IfStatementTests()
    {
        _fixture.Customizations.Add(new TypeRelay(typeof(Expression), typeof(ExpressionMock)));
        _fixture.Customizations.Add(new TypeRelay(typeof(Statement), typeof(StatementMock)));
    }

    [Fact]
    public void ToString_NoElseIfNoElse_ReturnsCorrectString()
    {
        // Arrange
        var thenCondition = _fixture.Create<Expression>();
        var thenScope = new Scope { Statements = _fixture.CreateMany<Statement>(3).ToArray() };

        var ifStatement = new IfStatement(new ConditionScope(thenCondition, thenScope));

        const string expected = """
        if ({Expression})
        {
            {Statement}
            {Statement}
            {Statement}
        }
        """;

        // Act
        var actual = ifStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_OneElseIfNoElse_ReturnsCorrectString()
    {
        // Arrange
        var thenCondition = _fixture.Create<Expression>();
        var thenScope = new Scope { Statements = _fixture.CreateMany<Statement>(3).ToArray() };

        var elseIfCondition = _fixture.Create<Expression>();
        var elseIfScope = new Scope { Statements = _fixture.CreateMany<Statement>(3).ToArray() };

        var ifStatement = new IfStatement(new ConditionScope(thenCondition, thenScope))
        {
            ElseIfScopes = new[] { new ConditionScope(elseIfCondition, elseIfScope) }
        };

        const string expected = """
        if ({Expression})
        {
            {Statement}
            {Statement}
            {Statement}
        }
        else if ({Expression})
        {
            {Statement}
            {Statement}
            {Statement}
        }
        """;

        // Act
        var actual = ifStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_ThreeElseIfNoElse_ReturnsCorrectString()
    {
        // Arrange
        var thenCondition = _fixture.Create<Expression>();
        var thenScope = new Scope { Statements = _fixture.CreateMany<Statement>(3).ToArray() };

        var elseIfCondition = _fixture.Create<Expression>();
        var elseIfScope = new Scope { Statements = _fixture.CreateMany<Statement>(3).ToArray() };

        var ifStatement = new IfStatement(new ConditionScope(thenCondition, thenScope))
        {
            ElseIfScopes = new[]
            {
                new ConditionScope(elseIfCondition, elseIfScope),
                new ConditionScope(elseIfCondition, elseIfScope),
                new ConditionScope(elseIfCondition, elseIfScope)
            }
        };

        const string expected = """
        if ({Expression})
        {
            {Statement}
            {Statement}
            {Statement}
        }
        else if ({Expression})
        {
            {Statement}
            {Statement}
            {Statement}
        }
        else if ({Expression})
        {
            {Statement}
            {Statement}
            {Statement}
        }
        else if ({Expression})
        {
            {Statement}
            {Statement}
            {Statement}
        }
        """;

        // Act
        var actual = ifStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_NoElseIf_ReturnsCorrectString()
    {
        // Arrange
        var thenCondition = _fixture.Create<Expression>();
        var thenScope = new Scope { Statements = _fixture.CreateMany<Statement>(3).ToArray() };

        var elseScope = new Scope { Statements = _fixture.CreateMany<Statement>(3).ToArray() };

        var ifStatement = new IfStatement(new ConditionScope(thenCondition, thenScope)) { ElseScope = elseScope };

        const string expected = """
        if ({Expression})
        {
            {Statement}
            {Statement}
            {Statement}
        }
        else
        {
            {Statement}
            {Statement}
            {Statement}
        }
        """;

        // Act
        var actual = ifStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_OneElseIf_ReturnsCorrectString()
    {
        // Arrange
        var thenCondition = _fixture.Create<Expression>();
        var thenScope = new Scope { Statements = _fixture.CreateMany<Statement>(3).ToArray() };

        var elseIfCondition = _fixture.Create<Expression>();
        var elseIfScope = new Scope { Statements = _fixture.CreateMany<Statement>(3).ToArray() };

        var elseScope = new Scope { Statements = _fixture.CreateMany<Statement>(3).ToArray() };

        var ifStatement = new IfStatement(new ConditionScope(thenCondition, thenScope))
        {
            ElseIfScopes = new[] { new ConditionScope(elseIfCondition, elseIfScope) }, ElseScope = elseScope
        };

        const string expected = """
        if ({Expression})
        {
            {Statement}
            {Statement}
            {Statement}
        }
        else if ({Expression})
        {
            {Statement}
            {Statement}
            {Statement}
        }
        else
        {
            {Statement}
            {Statement}
            {Statement}
        }
        """;

        // Act
        var actual = ifStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_ThreeElseIf_ReturnsCorrectString()
    {
        // Arrange
        var thenCondition = _fixture.Create<Expression>();
        var thenScope = new Scope { Statements = _fixture.CreateMany<Statement>(3).ToArray() };

        var elseIfCondition = _fixture.Create<Expression>();
        var elseIfScope = new Scope { Statements = _fixture.CreateMany<Statement>(3).ToArray() };

        var elseScope = new Scope { Statements = _fixture.CreateMany<Statement>(3).ToArray() };

        var ifStatement = new IfStatement(new ConditionScope(thenCondition, thenScope))
        {
            ElseIfScopes = new[]
            {
                new ConditionScope(elseIfCondition, elseIfScope),
                new ConditionScope(elseIfCondition, elseIfScope),
                new ConditionScope(elseIfCondition, elseIfScope)
            },
            ElseScope = elseScope
        };

        const string expected = """
        if ({Expression})
        {
            {Statement}
            {Statement}
            {Statement}
        }
        else if ({Expression})
        {
            {Statement}
            {Statement}
            {Statement}
        }
        else if ({Expression})
        {
            {Statement}
            {Statement}
            {Statement}
        }
        else if ({Expression})
        {
            {Statement}
            {Statement}
            {Statement}
        }
        else
        {
            {Statement}
            {Statement}
            {Statement}
        }
        """;

        // Act
        var actual = ifStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}