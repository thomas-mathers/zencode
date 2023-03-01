using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class IfStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
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

        var conditions = _fixture.CreateMany<Expression>(1).ToArray();
        var scopes = _fixture.CreateMany<Scope>(1).ToArray();

        var expected = new IfStatement(new ConditionScope(conditions[0], scopes[0]));

        _expressionParserMock
            .Setup(x => x.Parse(tokenStream, 0))
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

        var conditions = _fixture.CreateMany<Expression>(2).ToArray();
        var scopes = _fixture.CreateMany<Scope>(2).ToArray();

        var expected = new IfStatement(new ConditionScope(conditions[0], scopes[0]))
        {
            ElseIfScopes = new []
            {
                new ConditionScope(conditions[1], scopes[1])
            }
        };

        _expressionParserMock
            .Setup(x => x.Parse(tokenStream, 0))
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

        var conditions = _fixture.CreateMany<Expression>(4).ToArray();
        var scopes = _fixture.CreateMany<Scope>(4).ToArray();

        var expected = new IfStatement(new ConditionScope(conditions[0], scopes[0]))
        {
            ElseIfScopes = new []
            {
                new ConditionScope(conditions[1], scopes[1]),
                new ConditionScope(conditions[2], scopes[2]),
                new ConditionScope(conditions[3], scopes[3])
            }
        };

        _expressionParserMock
            .Setup(x => x.Parse(tokenStream, 0))
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
            new Token(TokenType.None),
        });

        var conditions = _fixture.CreateMany<Expression>(1).ToArray();
        var scopes = _fixture.CreateMany<Scope>(2).ToArray();

        var expected = new IfStatement(new ConditionScope(conditions[0], scopes[0]))
        {
            ElseIfScopes = Array.Empty<ConditionScope>(),
            ElseScope = scopes[1]
        };

        _expressionParserMock
            .Setup(x => x.Parse(tokenStream, 0))
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
            new Token(TokenType.None),
        });

        var conditions = _fixture.CreateMany<Expression>(2).ToArray();
        var scopes = _fixture.CreateMany<Scope>(3).ToArray();

        var expected = new IfStatement(new ConditionScope(conditions[0], scopes[0]))
        {
            ElseIfScopes = new []
            {
                new ConditionScope(conditions[1], scopes[1]),
            },
            ElseScope = scopes[2]
        };

        _expressionParserMock
            .Setup(x => x.Parse(tokenStream, 0))
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

        var conditions = _fixture.CreateMany<Expression>(4).ToArray();
        var scopes = _fixture.CreateMany<Scope>(5).ToArray();

        var expected = new IfStatement(new ConditionScope(conditions[0], scopes[0]))
        {
            ElseIfScopes = new []
            {
                new ConditionScope(conditions[1], scopes[1]),
                new ConditionScope(conditions[2], scopes[2]),
                new ConditionScope(conditions[3], scopes[3]),
            },
            ElseScope = scopes[4]
        };

        _expressionParserMock
            .Setup(x => x.Parse(tokenStream, 0))
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
}