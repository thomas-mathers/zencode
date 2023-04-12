using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
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

    [Theory]
    [ClassData(typeof(BinaryOperators))]
    public void Parse_ExpressionOpExpression_ReturnsBinaryExpression(TokenType operatorTokenType)
    {
        // Arrange
        var lExpression = _fixture.Create<ExpressionMock>();
        var rExpression = _fixture.Create<ExpressionMock>();

        var expected = new BinaryExpression(lExpression, new Token(operatorTokenType), rExpression);

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
}
