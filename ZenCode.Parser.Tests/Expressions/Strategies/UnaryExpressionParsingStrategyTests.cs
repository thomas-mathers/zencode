using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Mappers;
using ZenCode.Parser.Tests.TestData;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class UnaryExpressionParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly UnaryExpressionParsingStrategy _sut;
    private readonly Mock<ITokenStream> _tokenStreamMock = new();

    public UnaryExpressionParsingStrategyTests()
    {
        _sut = new UnaryExpressionParsingStrategy();

        _fixture.Customizations.Add(new TypeRelay(typeof(Expression), typeof(ExpressionMock)));
    }

    [Theory]
    [ClassData(typeof(UnaryOperators))]
    public void Parse_UnaryExpression_ReturnsUnaryExpression(TokenType operatorToken)
    {
        // Arrange
        var expression = _fixture.Create<Expression>();

        var expected = new UnaryExpression
        {
            Operator = TokenTypeToUnaryOperatorTypeMapper.Map(operatorToken),
            Expression = expression
        };

        _tokenStreamMock
            .Setup(x => x.Consume(operatorToken))
            .Returns(new Token(operatorToken));

        _parserMock
            .Setup(x => x.ParseExpression(_tokenStreamMock.Object, 0))
            .Returns(expression);

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object, operatorToken);

        // Assert
        Assert.Equal(expected, actual);
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
            () => _sut.Parse(_parserMock.Object, _tokenStreamMock.Object, It.IsAny<TokenType>())
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_NullParser_ThrowsArgumentNullException()
    {
        // Arrange
        // Act
        var actual = Assert.Throws<ArgumentNullException>
        (
            () => _sut.Parse(null!, _tokenStreamMock.Object, It.IsAny<TokenType>())
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_NullTokenStream_ThrowsArgumentNullException()
    {
        // Arrange
        // Act
        var actual = Assert.Throws<ArgumentNullException>
        (
            () => _sut.Parse(_parserMock.Object, null!, It.IsAny<TokenType>())
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_ParseExpressionThrowsException_ThrowsException()
    {
        // Arrange
        _parserMock
            .Setup(x => x.ParseExpression(_tokenStreamMock.Object, 0))
            .Throws<Exception>();

        // Act
        var actual = Assert.Throws<Exception>
        (
            () => _sut.Parse(_parserMock.Object, _tokenStreamMock.Object, It.IsAny<TokenType>())
        );

        // Assert
        Assert.NotNull(actual);
    }
}
