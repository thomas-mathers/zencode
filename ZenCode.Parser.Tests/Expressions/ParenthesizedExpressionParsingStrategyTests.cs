using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Expressions;

namespace ZenCode.Parser.Tests.Expressions;

public class ParenthesizedExpressionParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly ParenthesizedExpressionParsingStrategy _sut;

    public ParenthesizedExpressionParsingStrategyTests()
    {
        _sut = new ParenthesizedExpressionParsingStrategy(_expressionParserMock.Object);
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
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(expression)
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });        
        
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
            new Token(TokenType.RightParenthesis),
        });

        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0)).Throws<UnexpectedTokenException>();

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
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(expression)
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });

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
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(expected)
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}