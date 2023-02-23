using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Expressions;

public class ExpressionListParserTests
{
    public static readonly IEnumerable<object[]> TokenTypesOtherThanComma =
        Enum.GetValues<TokenType>().Where(t => t != TokenType.Comma).Select(t => new object[] { t });
    
    private readonly Fixture _fixture = new();
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly ExpressionListParser _sut;
    
    public ExpressionListParserTests()
    {
        _sut = new ExpressionListParser(_expressionParserMock.Object);
    }

    [Fact]
    public void Parse_OneExpression_ReturnsSingleExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.None)
        });

        var expected = _fixture.CreateMany<Expression>(1).ToArray();

        _expressionParserMock.ReturnsExpressionSequence(expected);

        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_MultipleExpression_ReturnsMultipleExpressions()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.None),
            new Token(TokenType.Comma),
            new Token(TokenType.None),
            new Token(TokenType.Comma),
            new Token(TokenType.None),
        });

        var expected = _fixture.CreateMany<Expression>(3).ToArray();

        _expressionParserMock.ReturnsExpressionSequence(expected);

        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [MemberData(nameof(TokenTypesOtherThanComma))]
    public void Parse_MissingCommaBetweenTwoExpressions_ReturnsFirstExpression(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.None),
            new Token(tokenType),
            new Token(TokenType.None),
        });

        var expressions = _fixture.CreateMany<Expression>(2).ToArray();

        var expected = expressions[..1];

        _expressionParserMock.ReturnsExpressionSequence(expressions);

        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}