using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class IfStatementParsingStrategyTests
{
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly Fixture _fixture = new();
    private readonly Mock<IStatementParser> _statementParserMock = new();
    private readonly IfStatementParsingStrategy _sut;

    public IfStatementParsingStrategyTests()
    {
        _sut = new IfStatementParsingStrategy(_expressionParserMock.Object, _statementParserMock.Object);
    }

    [Fact]
    public void Parse_HasThen_ReturnsIfStatementWithOnlyThen()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.None),
            new Token(TokenType.None)
        });

        var conditionScopes = _fixture.CreateMany<ConditionScope>(1).ToArray();
        var conditions = conditionScopes.Select(x => x.Condition).ToArray();
        var scopes = conditionScopes.Select(x => x.Scope).ToArray();

        var expected = new IfStatement(conditionScopes[0]);

        _expressionParserMock
            .Setup(x => x.ParseExpression(tokenStream, 0))
            .ReturnsSequence(conditions)
            .ConsumesToken(tokenStream);

        _statementParserMock
            .Setup(x => x.ParseScope(tokenStream))
            .ReturnsSequence(scopes)
            .ConsumesToken(tokenStream);

        // Arrange
        var actual = _sut.Parse(tokenStream);

        // Act
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_HasThenAndOneElseIf_ReturnsIfStatementWithThenAndOneElseIf()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.ElseIf),
            new Token(TokenType.None),
            new Token(TokenType.None)
        });

        var conditionScopes = _fixture.CreateMany<ConditionScope>(2).ToArray();
        var conditions = conditionScopes.Select(x => x.Condition).ToArray();
        var scopes = conditionScopes.Select(x => x.Scope).ToArray();

        var expected = new IfStatement(conditionScopes[0])
        {
            ElseIfScopes = new[]
            {
                conditionScopes[1]
            }
        };

        _expressionParserMock
            .Setup(x => x.ParseExpression(tokenStream, 0))
            .ReturnsSequence(conditions)
            .ConsumesToken(tokenStream);

        _statementParserMock
            .Setup(x => x.ParseScope(tokenStream))
            .ReturnsSequence(scopes)
            .ConsumesToken(tokenStream);

        // Arrange
        var actual = _sut.Parse(tokenStream);

        // Act
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_HasThenAndMultipleElseIfs_ReturnsIfStatementWithThenAndMultipleElseIfs()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.ElseIf),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.ElseIf),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.ElseIf),
            new Token(TokenType.None),
            new Token(TokenType.None)
        });

        var conditionScopes = _fixture.CreateMany<ConditionScope>(4).ToArray();
        var conditions = conditionScopes.Select(x => x.Condition).ToArray();
        var scopes = conditionScopes.Select(x => x.Scope).ToArray();

        var expected = new IfStatement(conditionScopes[0])
        {
            ElseIfScopes = new[]
            {
                conditionScopes[1],
                conditionScopes[2],
                conditionScopes[3]
            }
        };

        _expressionParserMock
            .Setup(x => x.ParseExpression(tokenStream, 0))
            .ReturnsSequence(conditions)
            .ConsumesToken(tokenStream);

        _statementParserMock
            .Setup(x => x.ParseScope(tokenStream))
            .ReturnsSequence(scopes)
            .ConsumesToken(tokenStream);

        // Arrange
        var actual = _sut.Parse(tokenStream);

        // Act
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_HasThenAndElse_ReturnsIfStatementWithThenAndElse()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.Else),
            new Token(TokenType.None)
        });

        var conditionScopes = _fixture.CreateMany<ConditionScope>(1).ToArray();
        var conditions = conditionScopes.Select(x => x.Condition).ToArray();
        var scopes = conditionScopes.Select(x => x.Scope).ToArray();
        var scope = _fixture.Create<Scope>();

        var expected = new IfStatement(conditionScopes[0])
        {
            ElseIfScopes = Array.Empty<ConditionScope>(),
            ElseScope = scope
        };

        _expressionParserMock
            .Setup(x => x.ParseExpression(tokenStream, 0))
            .ReturnsSequence(conditions)
            .ConsumesToken(tokenStream);

        _statementParserMock
            .Setup(x => x.ParseScope(tokenStream))
            .ReturnsSequence(scopes.Concat(new[] { scope }).ToArray())
            .ConsumesToken(tokenStream);

        // Arrange
        var actual = _sut.Parse(tokenStream);

        // Act
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_HasThenAndOneElseIfAndElse_ReturnsIfStatementWithThenAndOneElseIfAndElse()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.ElseIf),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.Else),
            new Token(TokenType.None)
        });

        var conditionScopes = _fixture.CreateMany<ConditionScope>(2).ToArray();
        var conditions = conditionScopes.Select(x => x.Condition).ToArray();
        var scopes = conditionScopes.Select(x => x.Scope).ToArray();
        var scope = _fixture.Create<Scope>();

        var expected = new IfStatement(conditionScopes[0])
        {
            ElseIfScopes = new[]
            {
                conditionScopes[1]
            },
            ElseScope = scope
        };

        _expressionParserMock
            .Setup(x => x.ParseExpression(tokenStream, 0))
            .ReturnsSequence(conditions)
            .ConsumesToken(tokenStream);

        _statementParserMock
            .Setup(x => x.ParseScope(tokenStream))
            .ReturnsSequence(scopes.Concat(new[] { scope }).ToArray())
            .ConsumesToken(tokenStream);

        // Arrange
        var actual = _sut.Parse(tokenStream);

        // Act
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_HasThenAndMultipleElseIfAndElse_ReturnsIfStatementWithThenAndMultipleElseIfAndElse()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.ElseIf),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.ElseIf),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.ElseIf),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.Else),
            new Token(TokenType.None)
        });

        var conditionScopes = _fixture.CreateMany<ConditionScope>(4).ToArray();
        var conditions = conditionScopes.Select(x => x.Condition).ToArray();
        var scopes = conditionScopes.Select(x => x.Scope).ToArray();
        var scope = _fixture.Create<Scope>();

        var expected = new IfStatement(conditionScopes[0])
        {
            ElseIfScopes = new[]
            {
                conditionScopes[1],
                conditionScopes[2],
                conditionScopes[3]
            },
            ElseScope = scope
        };

        _expressionParserMock
            .Setup(x => x.ParseExpression(tokenStream, 0))
            .ReturnsSequence(conditions)
            .ConsumesToken(tokenStream);

        _statementParserMock
            .Setup(x => x.ParseScope(tokenStream))
            .ReturnsSequence(scopes.Concat(new[] { scope }).ToArray())
            .ConsumesToken(tokenStream);

        // Arrange
        var actual = _sut.Parse(tokenStream);

        // Act
        Assert.Equal(expected, actual);
    }
}