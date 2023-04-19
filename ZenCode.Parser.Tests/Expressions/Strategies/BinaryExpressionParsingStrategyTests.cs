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
using ZenCode.Parser.Tests.Mocks;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class BinaryExpressionParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly BinaryExpressionParsingStrategy _sut = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    
    public BinaryExpressionParsingStrategyTests()
    {
        _fixture.Customizations.Add(new TypeRelay(typeof(Expression), typeof(ExpressionMock)));
    }

    [Theory]
    [ClassData(typeof(BinaryOperators))]
    public void Parse_ExpressionOpExpression_ReturnsBinaryExpression(TokenType operatorTokenType)
    {
        // Arrange
        var lExpression = _fixture.Create<ExpressionMock>();
        var rExpression = _fixture.Create<ExpressionMock>();

        var expected = new BinaryExpression
        {
            Operator = new Token(operatorTokenType),
            Left = lExpression,
            Right = rExpression
        };

        _tokenStreamMock
            .Setup(x => x.Consume(operatorTokenType))
            .Returns(new Token(operatorTokenType));

        _parserMock
            .Setup(x => x.ParseExpression(_tokenStreamMock.Object, It.IsAny<int>()))
            .Returns(rExpression);

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object, lExpression, operatorTokenType, 0, false);

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

        var lOperand = _fixture.Create<Expression>();

        // Act
        var actual = Assert.Throws<UnexpectedTokenException>
        (
            () => _sut.Parse
                (_parserMock.Object, _tokenStreamMock.Object, lOperand, It.IsAny<TokenType>(), 0, false)
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_NullParser_ThrowsArgumentNullException()
    {
        // Arrange
        var lOperand = _fixture.Create<Expression>();

        // Act
        var actual = Assert.Throws<ArgumentNullException>
        (
            () => _sut.Parse
                (null!, _tokenStreamMock.Object, lOperand, It.IsAny<TokenType>(), 0, false)
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_NullTokenStream_ThrowsArgumentNullException()
    {
        // Arrange
        var lOperand = _fixture.Create<Expression>();

        // Act
        var actual = Assert.Throws<ArgumentNullException>
        (
            () => _sut.Parse
                (_parserMock.Object, null!, lOperand, It.IsAny<TokenType>(), 0, false)
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_NullLOperand_ThrowsArgumentNullException()
    {
        // Act
        var actual = Assert.Throws<ArgumentNullException>
        (
            () => _sut.Parse
                (_parserMock.Object, _tokenStreamMock.Object, null!, It.IsAny<TokenType>(), 0, false)
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_ParseExpressionThrowsException_ThrowsException()
    {
        // Arrange
        var lOperand = _fixture.Create<Expression>();

        _parserMock
            .Setup(x => x.ParseExpression(_tokenStreamMock.Object, It.IsAny<int>()))
            .Throws<Exception>();

        // Act
        var actual = Assert.Throws<Exception>
        (
            () => _sut.Parse
                (_parserMock.Object, _tokenStreamMock.Object, lOperand, It.IsAny<TokenType>(), 0, false)
        );

        // Assert
        Assert.NotNull(actual);
    }
}
