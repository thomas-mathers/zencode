using Moq;
using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Expressions;

public class UnaryExpressionParsingStrategyTests
{
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly UnaryExpressionParsingStrategy _sut;

    public UnaryExpressionParsingStrategyTests()
    {
        _sut = new UnaryExpressionParsingStrategy(_expressionParserMock.Object);
    }

    [Theory]
    [ClassData(typeof(ConstantTestData))]
    public void Parse_NotExpression_ReturnsUnaryExpression(TokenType constantType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Not
            },
            new Token
            {
                Type = constantType
            }
        });
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new ConstantExpression(new Token { Type = constantType }))
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });

        var expected = new UnaryExpression(
            new Token
            {
                Type = TokenType.Not
            },
            new ConstantExpression(new Token { Type = constantType }));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [ClassData(typeof(ConstantTestData))]
    public void Parse_MinusExpression_ReturnsUnaryExpression(TokenType constantType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Subtraction
            },
            new Token
            {
                Type = constantType
            }
        });
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new ConstantExpression(new Token { Type = constantType }))
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });

        var expected = new UnaryExpression(
            new Token
            {
                Type = TokenType.Subtraction
            },
            new ConstantExpression(new Token { Type = constantType }));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}