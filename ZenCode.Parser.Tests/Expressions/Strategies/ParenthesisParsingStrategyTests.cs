using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class ParenthesisParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly ParenthesisParsingStrategy _sut;

    public ParenthesisParsingStrategyTests()
    {
        _sut = new ParenthesisParsingStrategy(_parserMock.Object);
    }

    [Fact]
    public void Parse_ExtraLeftParenthesis_ThrowsUnexpectedTokenException()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.None),
            new Token(TokenType.RightParenthesis)
        });

        var expression = _fixture.Create<Expression>();

        _parserMock
            .Setup(x => x.ParseExpression(tokenStream, 0))
            .Returns(expression)
            .ConsumesToken(tokenStream);

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream));
    }

    [Fact]
    public void Parse_NoExpression_ThrowsUnexpectedTokenException()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.RightParenthesis)
        });

        _parserMock.Setup(x => x.ParseExpression(tokenStream, 0)).Throws<UnexpectedTokenException>();

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream));
    }

    [Fact]
    public void Parse_MissingRightParenthesis_ThrowsUnexpectedTokenException()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.None)
        });

        var expression = _fixture.Create<Expression>();

        _parserMock
            .Setup(x => x.ParseExpression(tokenStream, 0))
            .Returns(expression)
            .ConsumesToken(tokenStream);

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream));
    }

    [Fact]
    public void Parse_ParenthesizedExpression_ReturnsConstantExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.None),
            new Token(TokenType.RightParenthesis)
        });

        var expected = _fixture.Create<Expression>();

        _parserMock
            .Setup(x => x.ParseExpression(tokenStream, 0))
            .Returns(expected)
            .ConsumesToken(tokenStream);

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}