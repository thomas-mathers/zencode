using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Tests.Extensions;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Expressions;

public class UnaryExpressionParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly UnaryExpressionParsingStrategy _sut;

    public UnaryExpressionParsingStrategyTests()
    {
        _sut = new UnaryExpressionParsingStrategy(_expressionParserMock.Object);
    }

    [Theory]
    [ClassData(typeof(UnaryOperators))]
    public void Parse_UnaryExpression_ReturnsUnaryExpression(TokenType operatorToken)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(operatorToken),
            new Token(TokenType.None)
        });

        var expression = _fixture.Create<Expression>();
        
        var expected = new UnaryExpression(
            new Token(operatorToken),
            expression);

        _expressionParserMock
            .Setup(x => x.Parse(tokenStream, 0))
            .Returns(expression)
            .ConsumesToken(tokenStream);

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}