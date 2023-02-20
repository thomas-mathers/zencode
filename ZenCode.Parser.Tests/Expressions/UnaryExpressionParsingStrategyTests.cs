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
    public static readonly IEnumerable<object[]> ConstantTokenTypes =
        from c in TokenTypeGroups.GetConstants()
        select new object[] { c };
    
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly UnaryExpressionParsingStrategy _sut;

    public UnaryExpressionParsingStrategyTests()
    {
        _sut = new UnaryExpressionParsingStrategy(_expressionParserMock.Object);
    }

    [Theory]
    [MemberData(nameof(ConstantTokenTypes))]
    public void Parse_NotExpression_ReturnsUnaryExpression(TokenType constantType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Not),
            new Token(constantType)
        });
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new ConstantExpression(new Token(constantType)))
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });

        var expected = new UnaryExpression(
            new Token(TokenType.Not),
            new ConstantExpression(new Token(constantType)));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [MemberData(nameof(ConstantTokenTypes))]
    public void Parse_MinusExpression_ReturnsUnaryExpression(TokenType constantType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Subtraction),
            new Token(constantType)
        });
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new ConstantExpression(new Token(constantType)))
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });

        var expected = new UnaryExpression(
            new Token(TokenType.Subtraction),
            new ConstantExpression(new Token(constantType)));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}