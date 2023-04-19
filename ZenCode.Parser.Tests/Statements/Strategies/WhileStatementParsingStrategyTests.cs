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
using ZenCode.Parser.Tests.Mocks;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class WhileStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly WhileStatementParsingStrategy _sut;
    private readonly Mock<ITokenStream> _tokenStreamMock = new();

    public WhileStatementParsingStrategyTests()
    {
        _sut = new WhileStatementParsingStrategy();

        _fixture.Customizations.Add(new TypeRelay(typeof(Expression), typeof(ExpressionMock)));
        _fixture.Customizations.Add(new TypeRelay(typeof(Statement), typeof(StatementMock)));
    }

    [Fact]
    public void Parse_ValidInput_ReturnsWhileStatement()
    {
        // Arrange
        var expected = new WhileStatement
        {
            ConditionScope = _fixture.Create<ConditionScope>()
        };

        _parserMock
            .Setup(x => x.ParseConditionScope(_tokenStreamMock.Object))
            .Returns(expected.ConditionScope);

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);

        _tokenStreamMock.Verify(x => x.Consume(TokenType.While));
    }
    
    [Fact]
    public void Parse_MissingWhile_ThrowsUnexpectedTokenException()
    {
        // Arrange
        _tokenStreamMock
            .Setup(x => x.Consume(TokenType.While))
            .Throws<UnexpectedTokenException>();

        // Act
        var actual = Assert.Throws<UnexpectedTokenException>
        (
            () => _sut.Parse(_parserMock.Object, _tokenStreamMock.Object)
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_NullParser_ThrowsArgumentNullException()
    {
        // Arrange + Act
        var actual = Assert.Throws<ArgumentNullException>
        (
            () => _sut.Parse(null!, _tokenStreamMock.Object)
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_NullTokenStream_ThrowsArgumentNullException()
    {
        // Arrange + Act
        var actual = Assert.Throws<ArgumentNullException>
        (
            () => _sut.Parse(_parserMock.Object, null!)
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_ParseConditionScopeThrowsException_ThrowsException()
    {
        // Arrange
        _parserMock
            .Setup(x => x.ParseConditionScope(_tokenStreamMock.Object))
            .Throws<Exception>();

        // Act
        var actual = Assert.Throws<Exception>
        (
            () => _sut.Parse(_parserMock.Object, _tokenStreamMock.Object)
        );

        // Assert
        Assert.NotNull(actual);
    }
}
