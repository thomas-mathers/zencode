using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class ReturnStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly ReturnStatementParsingStrategy _sut;
    private readonly Mock<ITokenStream> _tokenStreamMock = new();

    public ReturnStatementParsingStrategyTests()
    {
        _sut = new ReturnStatementParsingStrategy();

        _fixture.Customizations.Add(new TypeRelay(typeof(Expression), typeof(ExpressionMock)));
    }

    [Fact]
    public void Parse_ReturnNothing_ReturnsReturnStatement()
    {
        // Arrange
        var expected = new ReturnStatement();

        _tokenStreamMock
            .Setup(x => x.Match(TokenType.Semicolon))
            .Returns(true);

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);

        _tokenStreamMock.Verify(x => x.Consume(TokenType.Return));
    }

    [Fact]
    public void Parse_ReturnAnyExpression_ReturnsReturnStatement()
    {
        // Arrange
        var expression = _fixture.Create<Expression>();

        var expected = new ReturnStatement { Value = expression };

        _tokenStreamMock
            .Setup(x => x.Match(TokenType.Semicolon))
            .Returns(false);

        _parserMock
            .Setup(x => x.ParseExpression(_tokenStreamMock.Object, 0))
            .Returns(expression);

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);

        _tokenStreamMock.Verify(x => x.Consume(TokenType.Return));
    }
    
    [Fact]
    public void Parse_MissingReturn_ThrowsUnexpectedTokenException()
    {
        // Arrange
        _tokenStreamMock
            .Setup(x => x.Consume(TokenType.Return))
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
    public void Parse_ParseExpressionThrowsException_ThrowsException()
    {
        // Arrange
        _tokenStreamMock
            .Setup(x => x.Match(TokenType.Semicolon))
            .Returns(false);

        _parserMock
            .Setup(x => x.ParseExpression(_tokenStreamMock.Object, 0))
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
