using AutoFixture;
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

public class BinaryExpressionParsingStrategyTests
{
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly Fixture _fixture = new();
    private readonly BinaryExpressionParsingStrategy _sut;

    public BinaryExpressionParsingStrategyTests()
    {
        _sut = new BinaryExpressionParsingStrategy(_expressionParserMock.Object, 0);
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

        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(rExpression)
            .Callback<ITokenStream, int>((_, _) => { tokenStream.Consume(); });

        var expected = new BinaryExpression(
            lExpression,
            new Token(operatorTokenType),
            rExpression);

        // Act
        var actual = _sut.Parse(tokenStream, lExpression);

        // Assert
        Assert.Equal(expected, actual);
    }
}