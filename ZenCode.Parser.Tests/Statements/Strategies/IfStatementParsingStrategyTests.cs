using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;
using ZenCode.Parser.Tests.Extensions;
using ZenCode.Parser.Tests.Mocks;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class IfStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly IfStatementParsingStrategy _sut;
    private readonly Mock<ITokenStream> _tokenStreamMock = new();

    public IfStatementParsingStrategyTests()
    {
        _sut = new IfStatementParsingStrategy();

        _fixture.Customizations.Add(new TypeRelay(typeof(Expression), typeof(ExpressionMock)));
        _fixture.Customizations.Add(new TypeRelay(typeof(Statement), typeof(StatementMock)));
    }

    [Fact]
    public void Parse_HasThen_ReturnsIfStatementWithOnlyThen()
    {
        // Arrange
        var conditionScopes = _fixture.CreateMany<ConditionScope>(1).ToArray();

        var expected = new IfStatement(conditionScopes[0]);

        _parserMock
            .Setup(x => x.ParseConditionScope(_tokenStreamMock.Object))
            .ReturnsSequence(conditionScopes);

        // Arrange
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

        // Act
        Assert.Equal(expected, actual);

        _tokenStreamMock.Verify(x => x.Consume(TokenType.If));
    }

    [Fact]
    public void Parse_HasThenAndOneElseIf_ReturnsIfStatementWithThenAndOneElseIf()
    {
        // Arrange
        var conditionScopes = _fixture.CreateMany<ConditionScope>(2).ToArray();

        var expected = new IfStatement(conditionScopes[0])
        {
            ElseIfScopes = new[] { conditionScopes[1] }
        };

        _tokenStreamMock
            .Setup(x => x.Match(TokenType.ElseIf))
            .ReturnsSequence(true, false);

        _parserMock.Setup(x => x.ParseConditionScope(_tokenStreamMock.Object))
                   .ReturnsSequence(conditionScopes);

        // Arrange
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

        // Act
        Assert.Equal(expected, actual);

        _tokenStreamMock.Verify(x => x.Consume(TokenType.If));
    }

    [Fact]
    public void Parse_HasThenAndMultipleElseIfs_ReturnsIfStatementWithThenAndMultipleElseIfs()
    {
        // Arrange
        var conditionScopes = _fixture.CreateMany<ConditionScope>(4).ToArray();

        var expected = new IfStatement(conditionScopes[0])
        {
            ElseIfScopes = new[]
            {
                conditionScopes[1],
                conditionScopes[2],
                conditionScopes[3]
            }
        };

        _tokenStreamMock
            .Setup(x => x.Match(TokenType.ElseIf))
            .ReturnsSequence(true, true, true, false);

        _parserMock
            .Setup(x => x.ParseConditionScope(_tokenStreamMock.Object))
            .ReturnsSequence(conditionScopes);

        // Arrange
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

        // Act
        Assert.Equal(expected, actual);

        _tokenStreamMock.Verify(x => x.Consume(TokenType.If));
    }

    [Fact]
    public void Parse_HasThenAndElse_ReturnsIfStatementWithThenAndElse()
    {
        // Arrange
        var conditionScopes = _fixture.CreateMany<ConditionScope>(1).ToArray();
        var scope = _fixture.Create<Scope>();

        var expected = new IfStatement(conditionScopes[0])
        {
            ElseIfScopes = Array.Empty<ConditionScope>(),
            ElseScope = scope
        };

        _tokenStreamMock
            .Setup(x => x.Match(TokenType.Else))
            .Returns(true);

        _parserMock
            .Setup(x => x.ParseConditionScope(_tokenStreamMock.Object))
            .ReturnsSequence(conditionScopes);

        _parserMock
            .Setup(x => x.ParseScope(_tokenStreamMock.Object))
            .Returns(scope);

        // Arrange
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

        // Act
        Assert.Equal(expected, actual);

        _tokenStreamMock.Verify(x => x.Consume(TokenType.If));
    }

    [Fact]
    public void Parse_HasThenAndOneElseIfAndElse_ReturnsIfStatementWithThenAndOneElseIfAndElse()
    {
        // Arrange
        var conditionScopes = _fixture.CreateMany<ConditionScope>(2).ToArray();
        var scope = _fixture.Create<Scope>();

        var expected = new IfStatement(conditionScopes[0])
        {
            ElseIfScopes = new[]
            {
                conditionScopes[1]
            },
            ElseScope = scope
        };

        _tokenStreamMock
            .Setup(x => x.Match(TokenType.ElseIf))
            .ReturnsSequence(true, false);

        _tokenStreamMock
            .Setup(x => x.Match(TokenType.Else))
            .Returns(true);

        _parserMock
            .Setup(x => x.ParseConditionScope(_tokenStreamMock.Object))
            .ReturnsSequence(conditionScopes);

        _parserMock
            .Setup(x => x.ParseScope(_tokenStreamMock.Object))
            .Returns(scope);

        // Arrange
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

        // Act
        Assert.Equal(expected, actual);

        _tokenStreamMock.Verify(x => x.Consume(TokenType.If));
    }

    [Fact]
    public void Parse_HasThenAndMultipleElseIfAndElse_ReturnsIfStatementWithThenAndMultipleElseIfAndElse()
    {
        // Arrange
        var conditionScopes = _fixture.CreateMany<ConditionScope>(4).ToArray();
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

        _tokenStreamMock
            .Setup(x => x.Match(TokenType.ElseIf))
            .ReturnsSequence(true, true, true, false);

        _tokenStreamMock
            .Setup(x => x.Match(TokenType.Else))
            .Returns(true);

        _parserMock
            .Setup(x => x.ParseConditionScope(_tokenStreamMock.Object))
            .ReturnsSequence(conditionScopes);

        _parserMock
            .Setup(x => x.ParseScope(_tokenStreamMock.Object))
            .Returns(scope);

        // Arrange
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

        // Act
        Assert.Equal(expected, actual);

        _tokenStreamMock.Verify(x => x.Consume(TokenType.If));
    }
    
    [Fact]
    public void Parse_UnexpectedToken_ThrowsUnexpectedTokenException()
    {
        // Arrange
        _tokenStreamMock
            .Setup(x => x.Consume(It.IsAny<TokenType>()))
            .Throws<UnexpectedTokenException>();

        // Act
        var actual = Assert.Throws<UnexpectedTokenException>
        (
            () => _sut.Parse(_parserMock.Object, _tokenStreamMock.Object)
        );

        // Assert
        Assert.NotNull(actual);
    }
}
