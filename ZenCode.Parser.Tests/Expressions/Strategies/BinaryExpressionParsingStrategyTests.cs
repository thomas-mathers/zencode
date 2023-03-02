using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.Extensions;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class BinaryExpressionParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly BinaryExpressionParsingStrategy _sut;

    public BinaryExpressionParsingStrategyTests()
    {
        _sut = new BinaryExpressionParsingStrategy(_parserMock.Object, 0);
    }

    [Theory]
    [ClassData(typeof(BinaryOperators))]
    public void Parse_ExpressionOpExpression_ReturnsBinaryExpression(TokenType operatorTokenType)
    {
        // Arrange
        var lExpression = _fixture.Create<Expression>();
        var rExpression = _fixture.Create<Expression>();

        var tokenStream = new TokenStream(new[]
        {
            new Token(operatorTokenType),
            new Token(TokenType.None)
        });

        var expected = new BinaryExpression(
            lExpression,
            new Token(operatorTokenType),
            rExpression);

        _parserMock
            .Setup(x => x.ParseExpression(tokenStream, 0))
            .Returns(rExpression)
            .ConsumesToken(tokenStream);

        // Act
        var actual = _sut.Parse(tokenStream, lExpression);

        // Assert
        Assert.Equal(expected, actual);
    }
}