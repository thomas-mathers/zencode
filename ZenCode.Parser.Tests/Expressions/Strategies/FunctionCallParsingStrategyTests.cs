using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class FunctionCallParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly FunctionCallParsingStrategy _sut = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly VariableReferenceExpression _variableReferenceExpression;

    public FunctionCallParsingStrategyTests()
    {
        _variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier));

        _fixture.Customizations.Add(new TypeRelay(typeof(Expression), typeof(ExpressionMock)));
    }

    [Fact]
    public void Parse_FunctionCallNoParameters_ReturnsFunctionCallExpression()
    {
        // Arrange
        var expected = new FunctionCallExpression { FunctionReference = _variableReferenceExpression };

        _tokenStreamMock
            .Setup(x => x.Match(TokenType.RightParenthesis))
            .Returns(true);

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object, _variableReferenceExpression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_FunctionCallOneOrMoreParameters_ReturnsFunctionCallExpression()
    {
        // Arrange
        var arguments = _fixture.Create<ExpressionList>();

        var expected = new FunctionCallExpression
            { FunctionReference = _variableReferenceExpression, Arguments = arguments };

        _tokenStreamMock
            .Setup(x => x.Match(TokenType.RightParenthesis))
            .Returns(false);

        _parserMock
            .Setup(x => x.ParseExpressionList(_tokenStreamMock.Object))
            .Returns(arguments);

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object, _variableReferenceExpression);

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
            () => _sut.Parse
                (_parserMock.Object, _tokenStreamMock.Object, _variableReferenceExpression)
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
            () => _sut.Parse
                (null!, _tokenStreamMock.Object, _variableReferenceExpression)
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
            () => _sut.Parse
                (_parserMock.Object, null!, _variableReferenceExpression)
        );

        // Assert
        Assert.NotNull(actual);
    }

    [Fact]
    public void Parse_NullVariableReferenceExpression_ThrowsArgumentNullException()
    {
        // Arrange + Act
        var actual = Assert.Throws<ArgumentNullException>
        (
            () => _sut.Parse
                (_parserMock.Object, _tokenStreamMock.Object, null!)
        );

        // Assert
        Assert.NotNull(actual);
    }

    [Fact]
    public void Parse_ParseExpressionListThrowsException_ThrowsException()
    {
        // Arrange
        _tokenStreamMock
            .Setup(x => x.Match(TokenType.RightParenthesis))
            .Returns(false);

        _parserMock
            .Setup(x => x.ParseExpressionList(_tokenStreamMock.Object))
            .Throws<Exception>();

        // Act
        var actual = Assert.Throws<Exception>
        (
            () => _sut.Parse
                (_parserMock.Object, _tokenStreamMock.Object, _variableReferenceExpression)
        );

        // Assert
        Assert.NotNull(actual);
    }
}
