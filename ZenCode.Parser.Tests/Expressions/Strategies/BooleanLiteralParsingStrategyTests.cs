using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class BooleanLiteralParsingStrategyTests
{
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly BooleanLiteralParsingStrategy _sut;

    public BooleanLiteralParsingStrategyTests()
    {
        _sut = new BooleanLiteralParsingStrategy();
    }

    [Fact]
    public void Parse_BooleanLiteral_ReturnsConstantExpression()
    {
        // Arrange
        var expected = new LiteralExpression(new Token(TokenType.BooleanLiteral));

        _tokenStreamMock
            .Setup(x => x.Consume(TokenType.BooleanLiteral))
            .Returns(new Token(TokenType.BooleanLiteral));

        // Act
        var actual = _sut.Parse(_tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);
    }
}